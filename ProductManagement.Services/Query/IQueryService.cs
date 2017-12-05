namespace ProductManagement.Services.Query
{
    using ProductManagement.DataRepresentation;
    using System.Linq;

    public interface IQueryService
    {
        IQueryable<T> Query<T>() where T : class, IIdentifiable;
    }
}
