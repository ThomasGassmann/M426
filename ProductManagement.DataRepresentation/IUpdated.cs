namespace ProductManagement.DataRepresentation
{
    using System;

    /// <summary>
    /// Defines an entity, that stores its update date.
    /// </summary>
    public interface IUpdated
    {
        /// <summary>
        /// Gets or sets the update date of the entity.
        /// </summary>
        DateTime Updated { get; set; }
    }
}
