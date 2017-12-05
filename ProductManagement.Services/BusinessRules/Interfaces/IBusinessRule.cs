namespace ProductManagement.Services.BusinessRules.Interfaces
{
    using ProductManagement.Services.UoW;
    using System.Collections.Generic;

    /// <summary>
    /// Defines a business rule to be executed when adding, updating or deleting entities from the database.
    /// </summary>
    public interface IBusinessRule
    {
        /// <summary>
        /// This method wiil be called before saving the changes to the database.
        /// Changes to the entities can be done in this method implementation.
        /// For example all added entities could be edited to have a creation date.
        /// </summary>
        /// <param name="added">All added entities. The ID of these entities will be unset, since they weren't indexed yet by the database.</param>
        /// <param name="updated">All updated entities.</param>
        /// <param name="removed">All deleted entities.</param>
        void PreSave(IList<object> added, IList<object> updated, IList<object> removed);

        /// <summary>
        /// This method will be executed after all changes were saved on the database.
        /// </summary>
        /// <param name="unitOfWork">The <see cref="IUnitOfWork"/> on which the changes were saved.</param>
        void PostSave(IUnitOfWork unitOfWork);
    }
}