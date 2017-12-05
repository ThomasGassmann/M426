namespace ProductManagement.DataRepresentation
{
    /// <summary>
    /// Defines all entities on the database, that have an ID (BITINT) as primary key.
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Gets or sets the ID of the entity.
        /// </summary>
        long Id { get; set; }
    }
}
