namespace ProductManagement.Services.BusinessRules
{
    using ProductManagement.Services.BusinessRules.Interfaces;
    using ProductManagement.Services.UoW;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a generic business rule base. It allows to have generic parameters in the 
    /// <see cref="PreSave(IList{object},IList{object}, IList{object})"/> method.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BusinessRuleBase<TEntity> : IBusinessRule
    {
        /// <inheritdoc />
        public virtual void PostSave(IUnitOfWork unitOfWork)
        {
        }

        /// <summary>
        /// Defines the <see cref="PreSave(IList{object}, IList{object}, IList{object})"/> method with generic parameters.
        /// </summary>
        /// <param name="added">The added items.</param>
        /// <param name="updated">The updated items.</param>
        /// <param name="removed">The removed items.</param>
        public virtual void PreSave(IList<TEntity> added, IList<TEntity> updated, IList<TEntity> removed)
        {
        }

        /// <inheritdoc />
        public void PreSave(IList<object> added, IList<object> updated, IList<object> removed) =>
            this.PreSave(added.Cast<TEntity>().ToList(), updated.Cast<TEntity>().ToList(), removed.Cast<TEntity>().ToList());
    }
}
