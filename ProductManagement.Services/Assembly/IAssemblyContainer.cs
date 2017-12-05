namespace ProductManagement.Services.Assembly
{
    using System.Reflection;

    /// <summary>
    /// Defines a container used to store all assemblies of the running application.
    /// </summary>
    public interface IAssemblyContainer
    {
        /// <summary>
        /// Gets all assemblies of the current application.
        /// </summary>
        /// <returns>An array of all found assemblies.</returns>
        Assembly[] GetAssemblies();
    }
}
