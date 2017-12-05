namespace ProductManagement.Web
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using ProductManagement.DataAccess.Context;
    using ProductManagement.DataRepresentation.Mappings;
    using ProductManagement.DataRepresentation.Model;
    using ProductManagement.Services.Assembly;
    using ProductManagement.Services.BusinessRules.Interfaces;
    using ProductManagement.Services.BusinessRules.Registry;
    using ProductManagement.Services.BusinessRules.Registry.Interfaces;
    using ProductManagement.Services.Core.Product.Creation;
    using ProductManagement.Services.Core.Product.Edit;
    using ProductManagement.Services.Core.Product.Filtering;
    using ProductManagement.Services.Query;
    using ProductManagement.Services.UoW;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");
            Mapper.Initialize(x => x.AddProfiles(new[] { typeof(Product) }));
            services.AddMvc();
            services.AddDbContext<ProductManagementDbContext>(x => 
                x.UseSqlServer(connectionString));
            services.AddSingleton<IBusinessRuleRegistry, BaseBusinessRuleRegistry>();
            var assemblies = Startup.IterateAssemblies();
            services.AddSingleton<IAssemblyContainer>(x => new AssemblyContainer(assemblies));
            services.AddSingleton<IQueryService, QueryService>();
            services.AddScoped<IProductEditService, ProductEditService>();
            services.AddScoped<IProductCreationService, ProductCreationService>();
            services.AddScoped<IProductFilterService, ProductFilterService>();
            services.AddSingleton<IUnitOfWorkFactory>(x =>
                new UnitOfWorkFactory(
                    connectionString,
                    x.GetService<IBusinessRuleRegistry>()));
            this.ConfigureBusinessRules(services);

            var context = services.BuildServiceProvider().GetService<ProductManagementDbContext>();
            context.Database.EnsureCreated();
        }

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
