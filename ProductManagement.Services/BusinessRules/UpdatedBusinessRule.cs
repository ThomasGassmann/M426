namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.DataRepresentation;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// This business rule will set the update date for all added items
    /// before saving the entity to the database.
    /// </summary>
    public class UpdatedBusinessRule : BusinessRuleBase<IUpdated>
    {
        /// <summary>
        /// Sets the update date on all entities before saving them to the database.
        /// </summary>
        /// <param name="added">All added items.</param>
        /// <param name="updated">All updated items.</param>
        /// <param name="removed">ALl removed items.</param>
        public override void PreSave(IList<IUpdated> added, IList<IUpdated> updated, IList<IUpdated> removed)
        {
            foreach (var updatedItem in updated)
            {
                updatedItem.Updated = DateTime.Now;
            }
        }
    }
}
