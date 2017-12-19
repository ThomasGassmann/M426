namespace ProductManagement.Web.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using ProductManagement.DataRepresentation.Dto;
    using ProductManagement.DataRepresentation.ViewModel;
    using ProductManagement.Services.ChangeNotification;
    using ProductManagement.Services.Core.Product.Creation;
    using ProductManagement.Services.Core.Product.Creation.Strategy;
    using ProductManagement.Services.Core.Product.Edit;
    using ProductManagement.Services.Core.Product.Filtering;
    using System.Net.Mail;

    /// <summary>
    /// Defines the controller for the route 'Product/...'.
    /// </summary>
    public class ProductController : Controller
    {
        /// <summary>
        /// Contains the service to edit products.
        /// </summary>
        private readonly IProductEditService productEditService;

        /// <summary>
        /// Contains the service to create products.
        /// </summary>
        private readonly IProductCreationService productCreationService;

        /// <summary>
        /// Contains the service to filter products.
        /// </summary>
        private readonly IProductFilterService filterService;

        /// <summary>
        /// Contains the service to manage observers.
        /// </summary>
        private readonly IObserverManager observerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController"/> class.
        /// </summary>
        /// <param name="productCreationService">The <see cref="IProductCreationService"/> dependency.</param>
        /// <param name="productEditService">The <see cref="IProductEditService"/> dependency.</param>
        /// <param name="productFilterService">The <see cref="IProductFilterService"/> dependency.</param>
        /// <param name="observerManager">The <see cref="IObserverManager"/> dependency.</param>
        public ProductController(
            IProductCreationService productCreationService,
            IProductEditService productEditService,
            IProductFilterService productFilterService,
            IObserverManager observerManager)
        {
            this.productEditService = productEditService;
            this.productCreationService = productCreationService;
            this.filterService = productFilterService;
            this.observerManager = observerManager;
        }

        /// <summary>
        /// Returns the page for the route '/Products/Index'. It will have two query
        /// parameters for the page number and page size. It will filter the products
        /// from the <see cref="IProductFilterService"/> and display them in the view.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>Returns the rendered view.</returns>
        [HttpGet("[controller]")]
        public IActionResult Index(int? page = 0, int? pageSize = 10)
        {
            var queried = this.filterService.GetByTitle(page.Value, pageSize.Value);
            var mapped = Mapper.Map<ViewProductViewModel[]>(queried);
            return this.View(mapped);
        }

        /// <summary>
        /// Returns the page for the route '/Products/Edit' or '/Product/Edit/productId'.
        /// It will return the page with the product already loaded or nothing, if a new
        /// one should be created.
        /// </summary>
        /// <param name="id">The id of the product to edit. Null, if the product should be created.</param>
        /// <returns>Returns the rednered view.</returns>
        [HttpGet("[controller]/Edit/{id?}")]
        public IActionResult Edit(long? id)
        {
            var dto = this.productEditService.GetProductEditDto(id);
            var viewModel = Mapper.Map<EditProductViewModel>(dto);
            return this.View(viewModel);
        }

        /// <summary>
        /// Registers an observer, if the user observes a product. This method will be called
        /// by an AJAX request from the client. It will create an observer for the given 
        /// product id and for the given email address. It will be used via HTTP post on the
        /// following url: '/api/Product/Observer'.
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <param name="email">The email.</param>
        /// <returns>Returns the HTTP response.</returns>
        [HttpPost("api/[controller]/Observer")]
        public IActionResult RegisterObserver([FromQuery]long? productId, [FromQuery]string email)
        {
            if (productId.HasValue && this.IsValidMail(email))
            {
                this.observerManager.Create(productId.Value, email);
                return this.Ok();
            }

            return this.BadRequest();
        }

        /// <summary>
        /// Saves the edits made on the form to the database. If the changes aren't valid,
        /// error messages will be displayed. The action will be accessed using HTTP post
        /// on '/Product/Edit/{productId}', where productId is null, if the product should
        /// be created. This action will be called by the form.
        /// </summary>
        /// <param name="viewModel">The view model sent by the form.</param>
        /// <returns>Returns a redirect or the error messages in the view.</returns>
        [HttpPost("[controller]/Edit/{id?}")]
        public IActionResult SaveEdit(EditProductViewModel viewModel)
        {
            this.TryUpdateModelAsync(viewModel).Wait();
            if (this.ModelState.IsValid)
            {
                var mapped = Mapper.Map<ProductCreationDto>(viewModel);
                if (!viewModel.Id.HasValue)
                {
                    var strategy = CreationStrategyFactory.CreateStrategy(viewModel.SelectedStrategy);
                    if (strategy == null)
                    {
                        // If the user has selected no valid creation strategy, display an error message.
                        this.ModelState.AddModelError("INVALID_STRATEGY", "Please select a valid strategy");
                        return this.View("Edit", viewModel);
                    }

                    this.productCreationService.CreateProduct(mapped, strategy);
                }
                else
                {
                    this.productEditService.SaveEdit(viewModel.Id.Value, mapped);
                }

                return this.RedirectToAction("Index", "Product");
            }

            return this.View("Edit", viewModel);
        }

        /// <summary>
        /// Checks whether the given mail address is valid.
        /// </summary>
        /// <param name="mail">The email to check.</param>
        /// <returns>Returns a value determning whether the mail is valid or not.</returns>
        private bool IsValidMail(string mail)
        {
            try
            {
                new MailAddress(mail);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}