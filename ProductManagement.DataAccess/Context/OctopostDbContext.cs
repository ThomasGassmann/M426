namespace ProductManagement.DataAccess.Context
{
    using Microsoft.EntityFrameworkCore;
    using ProductManagement.DataRepresentation.Model;

    /// <summary>
    /// Contains the <see cref="DbContext"/> to access the data stored in the database.
    /// </summary>
    public class ProductManagementDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductManagementDbContext"/> class. Creates the connection to the database.
        /// </summary>
        /// <param name="options">The options to connect to the database.</param>
        public ProductManagementDbContext(DbContextOptions<ProductManagementDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The entity 'Product' in the database.
        /// </summary>
        public DbSet<Product> Products { get; set; }
    }
}
