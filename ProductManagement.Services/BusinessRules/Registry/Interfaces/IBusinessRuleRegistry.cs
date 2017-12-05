namespace ProductManagement.Services.BusinessRules.Registry.Interfaces
{
    using ProductManagement.Services.BusinessRules.Interfaces;
    using ProductManagement.Services.UoW;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a type to store and manage all business rules.
    /// </summary>
    public interface IBusinessRuleRegistry
    {
        /// <summary>
        /// Gets the types of all registered business rules.
        /// The key of the dictionary is the entity type on the database,
        /// while the values are lists of business rules associated with 
        /// that given type.
        /// </summary>
        IDictionary<Type, IList<Type>> RegisteredEntries { get; }

        /// <summary>
        /// Gets all business rules for a given entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity to get the business rules for.</typeparam>
        /// <returns>Returns all business rules for the given type.</returns>
        IEnumerable<Type> GetBusinessRulesFor<TEntity>();

        /// <summary>
        /// Gets all business rules for a given entity.
        /// </summary>
        /// <param name="type">The entity to get the business rules for.</param>
        /// <returns>Returns all business rules for the given type.</returns>
        IEnumerable<Type> GetBusinessRulesFor(Type type);

        /// <summary>
        /// Triggers all <see cref="BusinessRuleBase{TEntity}.PreSave(IList{TEntity}, IList{TEntity}, IList{TEntity})"/>
        /// methods on the given <see cref="IUnitOfWork"/> for all business rules found
        /// for the changed entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity all business rules should be executed for.</typeparam>
        /// <param name="unitOfWork">The unit of work to execute the business rules on.</param>
        /// <param name="added">The added items on the unit of work.</param>
        /// <param name="changed">The updated items on the unit of work.</param>
        /// <param name="removed">The deleted items on the unit of work.</param>
        void TriggerPreSaveBusinessRulesFor<TEntity>(IUnitOfWork unitOfWork, IList<TEntity> added, IList<TEntity> changed, IList<TEntity> removed);

        /// <summary>
        /// Triggers all <see cref="BusinessRuleBase{TEntity}.PostSave(IUnitOfWork)"/>
        /// methods on the given <see cref="IUnitOfWork"/> for all business rules found
        /// for the changed entities. 
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity all business rules should be executed for.</typeparam>
        /// <param name="unitOfWork">The unit of work to execute the business rules on.</param>
        void TriggerPostSaveBusinessRulesFor<TEntity>(IUnitOfWork unitOfWork);

        /// <summary>
        /// Instantiates a business rules with the dependency injection container.
        /// </summary>
        /// <typeparam name="TBusinessRule">The type of the business rule to instantiate.</typeparam>
        /// <param name="unitOfWork">The unit of work used for the given business rule type..</param>
        /// <returns>Returns the instantiated business rule.</returns>
        TBusinessRule InstantiateBusinessRule<TBusinessRule>(IUnitOfWork unitOfWork) where TBusinessRule : IBusinessRule;

        /// <summary>
        /// Instantieates a business rule with the dependency injection container.
        /// </summary>
        /// <param name="type">The type of the business rule to instantiate.</param>
        /// <param name="unitOfWork">The unit of work used for the given business rule type.</param>
        /// <returns>Returns the instantiated business rule.</returns>
        IBusinessRule InstantiateBusinessRule(Type type, IUnitOfWork unitOfWork);

        /// <summary>
        /// Registers all business rules in the registry.
        /// </summary>
        void RegisterEntries();
    }
}
