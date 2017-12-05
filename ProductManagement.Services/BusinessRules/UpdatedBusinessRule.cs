namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.DataRepresentation;
    using System;
    using System.Collections.Generic;

    public class UpdatedBusinessRule : BusinessRuleBase<IUpdated>
    {
        public override void PreSave(IList<IUpdated> added, IList<IUpdated> updated, IList<IUpdated> removed)
        {
            foreach (var updatedItem in updated)
            {
                updatedItem.Updated = DateTime.Now;
            }
        }
    }
}
