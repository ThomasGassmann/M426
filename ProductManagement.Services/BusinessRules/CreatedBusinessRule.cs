namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.DataRepresentation;
    using System;
    using System.Collections.Generic;

    public class CreatedBusinessRule : BusinessRuleBase<ICreated>
    {
        public override void PreSave(IList<ICreated> added, IList<ICreated> updated, IList<ICreated> removed)
        {
            foreach (var addedItem in added)
            {
                addedItem.Created = DateTime.Now;
            }
        }
    }
}
