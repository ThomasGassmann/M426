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

    /// <summary>
    /// Defines the default implementation of the <see cref="IUnitOfWork"/>. See the interface
    /// for more documentation.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Contains the database context to make changes to the database.
        /// </summary>
        private readonly ProductManagementDbContext productManagementDbContext;

        /// <summary>
        /// Defines the change tracker lazily. It is used to get all changed entities.
        /// </summary>
        private readonly Lazy<ChangeTracker> changeTracker;

        /// <summary>
        /// The business rule registry, which contains all registered business rules.
        /// </summary>
        private readonly IBusinessRuleRegistry businessRuleRegistry;

        /// <summary>
        /// Contains the connection string for the database.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Contains all created repositories. The key is the entity type and the value is the
        /// created repository for the entity type.
        /// </summary>
        private readonly IDictionary<Type, Type> repositories = new Dictionary<Type, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork"/> class. It will
        /// initialize the change tracker lazily.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        /// <param name="businessRuleRegistry">The registry for all business rules.</param>
        public UnitOfWork(string connectionString, IBusinessRuleRegistry businessRuleRegistry)
        {
            this.RegisterRepositories();
            this.connectionString = connectionString;
            this.productManagementDbContext = this.CreateContext();
            this.businessRuleRegistry = businessRuleRegistry;
            this.changeTracker = new Lazy<ChangeTracker>(() => new ChangeTracker(this.productManagementDbContext));
        }

        /// <inheritdoc />
        public IRepository<T> CreateEntityRepository<T>()
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                throw new ArgumentException($"No repository for type {typeof(T).FullName} registered");
            }

            var type = this.repositories[typeof(T)];
            return (IRepository<T>)Activator.CreateInstance(type, this.productManagementDbContext);
        }

        /// <inheritdoc />
        public void Dispose() =>
            this.productManagementDbContext.Dispose();

        /// <inheritdoc />
        public void Save()
        {
            var changeTrackerValue = this.changeTracker.Value;
            var businessRules = this.ExecutePreSaveBusinessRules(changeTrackerValue);
            this.productManagementDbContext.SaveChanges();
            this.ExecutePostSaveBusinessRules(businessRules);
        }

        /// <summary>
        /// Creates a new database context
        /// </summary>
        /// <returns>Returns the newly created database context.</returns>
        private ProductManagementDbContext CreateContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProductManagementDbContext>();
            optionsBuilder.UseSqlServer(this.connectionString);
            return new ProductManagementDbContext(optionsBuilder.Options);
        }

        /// <summary>
        /// This method will get all changed entities from the database context, find
        /// all business rules for those entities, and finally executes those business rules
        /// with the pre save method on the entity. It will return all business rules
        /// that were executed.
        /// </summary>
        /// <param name="changeTracker">The change tracker to use to get all changed entities.</param>
        /// <returns>Returns all executed business rules (pre save).</returns>
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

        /// <summary>
        /// It will execute all post save methods on the given business rules.
        /// </summary>
        /// <param name="businessRulesToExecute">The business rules to execute the post save methods on.</param>
        private void ExecutePostSaveBusinessRules(IEnumerable<IBusinessRule> businessRulesToExecute)
        {
            foreach (var businessRule in businessRulesToExecute)
            {
                businessRule.PostSave(this);
            }
        }

        /// <summary>
        /// Registers all repositories.
        /// </summary>
        private void RegisterRepositories()
        {
            this.repositories.Add(typeof(Product), typeof(Repository<Product>));
        }
    }
}
