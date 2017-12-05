namespace ProductManagement.Services.BusinessRules.Registry
{
    using ProductManagement.Services.Assembly;
    using ProductManagement.Services.BusinessRules.Interfaces;
    using ProductManagement.Services.BusinessRules.Registry.Interfaces;
    using ProductManagement.Services.UoW;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the default registry for business rules. It can be extended with additional business rules
    /// by inheriting from the class and overriding the <see cref="RegisteredEntries"/> method.
    /// </summary>
    public class BaseBusinessRuleRegistry : IBusinessRuleRegistry
    {
        /// <summary>
        /// Contains the service provider used to get services via the dependency injection container.
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Contains the assembly container to loop through all running assemblies.
        /// </summary>
        private readonly IAssemblyContainer assemblyContainer;

        /// <summary>
        /// Contains all registered business rules. Every entity type (key) has multiple business rules associated with it (value).
        /// </summary>
        private IDictionary<Type, IList<Type>> registeredEntries = 
            new Dictionary<Type, IList<Type>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseBusinessRuleRegistry"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider used to get dependencies.</param>
        /// <param name="assemblyContainer">The container of all executing assemblies.</param>
        public BaseBusinessRuleRegistry(IServiceProvider serviceProvider, IAssemblyContainer assemblyContainer)
        {
            this.serviceProvider = serviceProvider;
            this.assemblyContainer = assemblyContainer;
            this.RegisterEntries();
        }

        /// <inheritdoc />
        public IDictionary<Type, IList<Type>> RegisteredEntries => 
            this.registeredEntries;

        /// <inheritdoc />
        public virtual void RegisterEntries()
        {
            var productManagementAssemblies = this.assemblyContainer.GetAssemblies();
            foreach (var productManagementAssembly in productManagementAssemblies)
            {
                var types = productManagementAssembly.GetTypes();
                var businessRules = types.Where(x => x.GetInterfaces().Contains(typeof(IBusinessRule)) && !x.IsInterface);
                foreach (var businessRule in businessRules)
                {
                    if (!businessRule.IsAbstract && !businessRule.IsGenericType)
                    {
                        var type = businessRule.BaseType.GetGenericArguments().SingleOrDefault();
                        if (businessRule.BaseType == typeof(object))
                        {
                            type = typeof(object);
                        }

                        this.RegisterEntry(type, businessRule);
                    }
                }
            }
        }

        /// <inheritdoc />
        public IEnumerable<Type> GetBusinessRulesFor<TEntity>() =>
            this.GetBusinessRulesFor(typeof(TEntity));

        /// <inheritdoc />
        public IEnumerable<Type> GetBusinessRulesFor(Type type)
        {
            var list = new List<Type>();
            foreach (var businessRuleGroup in this.RegisteredEntries)
            {
                if (businessRuleGroup.Key.GetTypeInfo().IsAssignableFrom(type))
                {
                    foreach (var value in businessRuleGroup.Value)
                    {
                        list.Add(value);
                    }
                }
            }
    
            // Execute least specific business rule first
            return list.Distinct().OrderBy(x => x.GetTypeInfo().IsInterface);
        }

        /// <inheritdoc />
        public TBusinessRule InstantiateBusinessRule<TBusinessRule>(IUnitOfWork unitOfWork) where TBusinessRule : IBusinessRule =>
            (TBusinessRule)this.InstantiateBusinessRule(typeof(TBusinessRule), unitOfWork);

        /// <inheritdoc />
        public IBusinessRule InstantiateBusinessRule(Type type, IUnitOfWork unitOfWork)
        {
            this.ThrowIfInvalidBusinessRule(type);
            var instantiated = this.serviceProvider.GetService(type);
            return (IBusinessRule)instantiated;
        }

        /// <inheritdoc />
        public void TriggerPreSaveBusinessRulesFor<TEntity>(IUnitOfWork unitOfWork, IList<TEntity> added, IList<TEntity> changed, IList<TEntity> removed)
        {
            var rules = this.GetBusinessRulesFor<TEntity>();
            foreach (var rule in rules)
            {
                var createdRule = this.InstantiateBusinessRule(rule, unitOfWork);
                createdRule.PreSave(
                    added.Cast<object>().ToList(), 
                    changed.Cast<object>().ToList(), 
                    removed.Cast<object>().ToList());
            }
        }

        /// <inheritdoc />
        public void TriggerPostSaveBusinessRulesFor<TEntity>(IUnitOfWork unitOfWork)
        {
            var rules = this.GetBusinessRulesFor<TEntity>();
            foreach (var rule in rules)
            {
                var createdRule = this.InstantiateBusinessRule(rule, unitOfWork);
                createdRule.PostSave(unitOfWork);
            }
        }

        /// <summary>
        /// Throws an exception, if the given business rule is invalid.
        /// </summary>
        /// <param name="t">The business rule type to validate.</param>
        private void ThrowIfInvalidBusinessRule(Type t)
        {
            if (!t.GetInterfaces().Contains(typeof(IBusinessRule)))
            {
                throw new ArgumentException($"The business rule '{t.FullName}' must implement '{typeof(IBusinessRule).FullName}'.");
            }
        }

        /// <summary>
        /// Registers a single business rule entry.
        /// </summary>
        /// <param name="entityType">The entity type to register business rules for.</param>
        /// <param name="businessRuleType">The business rule to register for the given entity type.</param>
        private void RegisterEntry(Type entityType, Type businessRuleType)
        {
            this.ThrowIfInvalidBusinessRule(businessRuleType);
            if (!this.registeredEntries.ContainsKey(entityType))
            {
                this.registeredEntries.Add(entityType, new List<Type>());
                this.registeredEntries[entityType].Add(businessRuleType);

                return;
            }

            this.registeredEntries[entityType].Add(businessRuleType);
        }
    }
}
