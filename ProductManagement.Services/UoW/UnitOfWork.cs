namespace ProductManagement.Services.UoW
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using ProductManagement.DataAccess.Context;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.BusinessRules.Interfaces;
    using ProductManagement.Services.BusinessRules.Registry.Interfaces;
    using ProductManagement.Services.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductManagementDbContext productManagementDbContext;

        private readonly Lazy<ChangeTracker> changeTracker;

        private readonly IBusinessRuleRegistry businessRuleRegistry;

        private readonly string connectionString;

        private readonly IDictionary<Type, Type> repositories = new Dictionary<Type, Type>();

        public UnitOfWork(string connectionString, IBusinessRuleRegistry businessRuleRegistry)
        {
            this.RegisterRepositories();
            this.connectionString = connectionString;
            this.productManagementDbContext = this.CreateContext();
            this.businessRuleRegistry = businessRuleRegistry;
            this.changeTracker = new Lazy<ChangeTracker>(() => new ChangeTracker(this.productManagementDbContext));
        }

        public IRepository<T> CreateEntityRepository<T>()
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"No repository for type {typeof(T).FullName} registered");
            }

            var type = this.repositories[typeof(T)];
            return (IRepository<T>)Activator.CreateInstance(type, this.productManagementDbContext);
        }

        public void Dispose() =>
            this.productManagementDbContext.Dispose();

        public void Save()
        {
            var changeTrackerValue = this.changeTracker.Value;
            var businessRules = this.ExecutePreSaveBusinessRules(changeTrackerValue);
            this.productManagementDbContext.SaveChanges();
            this.ExecutePostSaveBusinessRules(businessRules);
        }

        private ProductManagementDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductManagementDbContext>();
            optionsBuilder.UseSqlServer(this.connectionString);
            return new ProductManagementDbContext(optionsBuilder.Options);
        }

        private IEnumerable<IBusinessRule> ExecutePreSaveBusinessRules(ChangeTracker changeTracker)
        {
            var list = new List<IBusinessRule>();
            foreach (var changedEntityGroup in changeTracker.Entries().GroupBy(x => x.Entity.GetType()))
            {
                var addedIds = changedEntityGroup.Where(x => x.State == EntityState.Added).Select(x => x.Entity);
                var changedIds = changedEntityGroup.Where(x => x.State == EntityState.Modified).Select(x => x.Entity);
                var removedIds = changedEntityGroup.Where(x => x.State == EntityState.Deleted).Select(x => x.Entity);
                var businessRulesToExecute = this.businessRuleRegistry.GetBusinessRulesFor(changedEntityGroup.Key);
                foreach (var businessRule in businessRulesToExecute)
                {
                    var instantiatedBusinessRule = this.businessRuleRegistry.InstantiateBusinessRule(businessRule, this);
                    instantiatedBusinessRule.PreSave(
                        addedIds.ToList(),
                        changedIds.ToList(),
                        removedIds.ToList());
                    list.Add(instantiatedBusinessRule);
                }
            }

            return list;
        }

        private void ExecutePostSaveBusinessRules(IEnumerable<IBusinessRule> businessRulesToExecute)
        {
            foreach (var businessRule in businessRulesToExecute)
            {
                businessRule.PostSave(this);
            }
        }

        private void RegisterRepositories()
        {
            this.repositories.Add(typeof(Product), typeof(Repository<Product>));
        }
    }
}
