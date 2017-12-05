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

    public class ProductController : Controller
    {
        private readonly IProductEditService productEditService;

        private readonly IProductCreationService productCreationService;

        private readonly IProductFilterService filterService;

        public ProductController(
            IProductCreationService productCreationService,
            IProductEditService productEditService,
            IProductFilterService productFilterService)
        {
            this.productEditService = productEditService;
            this.productCreationService = productCreationService;
            this.filterService = productFilterService;
        }

        [HttpGet("[controller]")]
        public IActionResult Index(int? page = 0, int? pageSize = 10)
        {
            var queried = this.filterService.GetByTitle(page.Value, pageSize.Value);
            var mapped = Mapper.Map<ViewProductViewModel[]>(queried);
            return this.View(mapped);
        }

        [HttpGet("[controller]/Edit/{id?}")]
        public IActionResult Edit(long? id)
        {
            var viewModel = this.productEditService.GetViewModel(id);
            return this.View(viewModel);
        }

        [HttpPost("api/[controller]/Observer")]
        public IActionResult RegisterObserver([FromQuery]long? productId, [FromQuery]string email)
        {
            if (productId.HasValue && this.IsValidMail(email))
            {
                ObserverManager.Instance.Create(productId.Value, email);
                return this.Ok();
            }

            return this.BadRequest();
        }

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