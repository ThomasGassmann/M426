namespace ProductManagement.DataRepresentation
{
    using System;

    /// <summary>
    /// Defines an entity, that stores its creation date.
    /// </summary>
    public interface ICreated
    {
        /// <summary>
        /// Gets or sets the creation date of the entity.
        /// </summary>
        DateTime Created { get; set; }
    }
}
