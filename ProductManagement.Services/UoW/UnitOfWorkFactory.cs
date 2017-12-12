namespace ProductManagement.Services.UoW
{
    using ProductManagement.Services.BusinessRules.Registry.Interfaces;

    /// <summary>
    /// The default sinlgeton (in the DI container) of the <see cref="IUnitOfWorkFactory"/>.
    /// </summary>
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        /// <summary>
        /// The business rule registry, which registers all business rules and is able to execute them.
        /// </summary>
        private readonly IBusinessRuleRegistry businessRuleRegistry;

        /// <summary>
        /// Contains the connection string.
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWorkFactory"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string dependency.</param>
        /// <param name="businessRuleRegistry">The <see cref="IBusinessRuleRegistry"/> dependency.</param>
        public UnitOfWorkFactory(string connectionString, IBusinessRuleRegistry businessRuleRegistry)
        {
            this.connectionString = connectionString;
            this.businessRuleRegistry = businessRuleRegistry;
        }

        /// <inheritdoc />
        public IUnitOfWork CreateUnitOfWork()
        {
            var unitOfWork = new UnitOfWork(this.connectionString, this.businessRuleRegistry);
            return unitOfWork;
        }
    }
}
