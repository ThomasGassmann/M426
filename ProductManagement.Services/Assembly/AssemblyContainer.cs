namespace ProductManagement.Services.Assembly
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The implementation of the assembly conatiner.
    /// </summary>
    public class AssemblyContainer : IAssemblyContainer
    {
        /// <summary>
        /// Stores all assemblies lazily.
        /// </summary>
        private readonly Lazy<Assembly[]> assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyContainer"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies to store.</param>
        public AssemblyContainer(IEnumerable<Assembly> assemblies)
        {
            this.assemblies = new Lazy<Assembly[]>(() => assemblies.ToArray());
        }

        /// <inheritdoc />
        public Assembly[] GetAssemblies() =>
            this.assemblies.Value;
    }
}
