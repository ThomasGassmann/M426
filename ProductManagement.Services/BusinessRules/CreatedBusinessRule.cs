namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.DataRepresentation;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This business rule will set the created date for all added items
    /// before saving the entity to the database.
    /// </summary>
    public class CreatedBusinessRule : BusinessRuleBase<ICreated>
    {
        /// <summary>
        /// Sets the creation date on all entities before saving them to the database.
        /// </summary>
        /// <param name="added">All added items.</param>
        /// <param name="updated">All updated items.</param>
        /// <param name="removed">ALl removed items.</param>
        public override void PreSave(IList<ICreated> added, IList<ICreated> updated, IList<ICreated> removed)
        {
            foreach (var addedItem in added)
            {
                addedItem.Created = DateTime.Now;
            }
        }
    }
}
