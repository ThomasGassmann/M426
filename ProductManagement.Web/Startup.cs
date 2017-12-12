namespace ProductManagement.Web
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using ProductManagement.DataAccess.Context;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.DataRepresentation.Settings;
    using ProductManagement.Services.Assembly;
    using ProductManagement.Services.BusinessRules.Interfaces;
    using ProductManagement.Services.BusinessRules.Registry;
    using ProductManagement.Services.BusinessRules.Registry.Interfaces;
    using ProductManagement.Services.ChangeNotification;
    using ProductManagement.Services.Core.Product.Creation;
    using ProductManagement.Services.Core.Product.Edit;
    using ProductManagement.Services.Core.Product.Filtering;
    using ProductManagement.Services.Query;
    using ProductManagement.Services.UoW;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Defines the startup class to initalize the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configration of the application.</param>
        public Startup(IConfiguration configuration) =>
            this.Configuration = configuration;

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures all services for the dependency injection container.
        /// </summary>
        /// <param name="services">The collection to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var mailConfiguration = JsonConvert.DeserializeObject<SmtpSettings>(
                this.Configuration.GetSection("Smtp").Value);
            services.AddSingleton<ISmtpSettings, SmtpSettings>();
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            // Register all auto mapper types in the assembly of the given type.
            Mapper.Initialize(x => x.AddProfiles(new[] { typeof(Product) }));
            services.AddMvc();
            // Registers the database context.
            services.AddDbContext<ProductManagementDbContext>(x => 
                x.UseSqlServer(connectionString));
            services.AddSingleton<IBusinessRuleRegistry, BaseBusinessRuleRegistry>();
            // Iterate all assemblies and register it in the DI container.
            var assemblies = Startup.IterateAssemblies();
            services.AddSingleton<IAssemblyContainer>(x => new AssemblyContainer(assemblies));
            // Registera services
            services.AddSingleton<IQueryService, QueryService>();
            services.AddScoped<IProductEditService, ProductEditService>();
            services.AddScoped<IProductCreationService, ProductCreationService>();
            services.AddScoped<IProductFilterService, ProductFilterService>();
            services.AddSingleton<IUnitOfWorkFactory>(x =>
                new UnitOfWorkFactory(
                    connectionString,
                    x.GetService<IBusinessRuleRegistry>()));
            services.AddSingleton<IObserverManager, ObserverManager>();
            this.ConfigureBusinessRules(services);

            var context = services.BuildServiceProvider().GetService<ProductManagementDbContext>();
            context.Database.EnsureCreated();
        }

        /// <summary>
        /// Configures the pipeline for a request.
        /// </summary>
        /// <param name="app">The app builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// Iterates through all assemblies in the application.
        /// </summary>
        /// <returns>Returns an enumerable of the assemblies.</returns>
        private static IEnumerable<Assembly> IterateAssemblies()
        {
            yield return Assembly.GetExecutingAssembly();
            var assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach (var assembly in assemblies)
            {
                if (assembly.Name.Contains(nameof(ProductManagement)))
                {
                    var loaded = Assembly.Load(assembly);
                    yield return loaded;
                }
            }
        }

        /// <summary>
        /// Configures all business rules for the depdency injection container.
        /// </summary>
        /// <param name="services">The service collection to add the business rules to.</param>
        private void ConfigureBusinessRules(IServiceCollection services)
        {
            foreach (var productManagementAssembly in Startup.IterateAssemblies())
            {
                var types = productManagementAssembly.GetTypes();
                var businessRules = types.Where(x => x.GetInterfaces().Contains(typeof(IBusinessRule)) && !x.IsInterface);
                foreach (var businessRule in businessRules)
                {
                    if (!businessRule.IsAbstract && !businessRule.IsGenericType)
                    {
                        services.AddTransient(businessRule);
                    }
                }
            }
        }
    }
}
