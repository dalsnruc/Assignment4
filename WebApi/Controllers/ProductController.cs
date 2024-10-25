using DataLayer;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Mapster;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]

public class ProductController : BaseController
{
    IDataService _dataService;
    private readonly LinkGenerator _linkGenerator;

    public ProductController(
        IDataService dataService,
        LinkGenerator linkGenerator)
        : base(linkGenerator)
    {
        _dataService = dataService;
        _linkGenerator = linkGenerator;
    }
    
    [HttpGet("{id}", Name = nameof(GetProduct))]
    public IActionResult GetProduct(int id)
    {
        var product = _dataService.GetProduct(id);

        if (product == null)
        {
            return NotFound();
        }
        var model = CreateProductModel(product);
        return Ok(model);
    }

    [HttpGet("category/{id}", Name = nameof(GetProductByCategories))]
    public IActionResult GetProductByCategories(int id)
    {
        var products = _dataService
            .GetProductByCategory(id)
            .Select(CreateProductModel);

        if (products == null || !products.Any())
        {
            return NotFound(products ?? new List<ProductModel>());

        }

        return Ok(products);
    }

    [HttpGet]
    public IActionResult GetProductByName(string name)
    {
        var products = _dataService
            .GetProductByName(name)
            .Select(CreateProductModel);

        if (products == null || !products.Any())
        {
            return NotFound(products ?? new List<ProductModel>());
        }

        return Ok(products);

    }


    private ProductModel? CreateProductModel(Product? product)
    {
        if (product == null) {
            return null;
        }
        var model = product.Adapt<ProductModel>();
        model.Url = GetUrl(product.Id);

        return model;
        
    }

    private string? GetUrl(int id)
    {
        return _linkGenerator.GetUriByName(
            HttpContext,
            nameof(GetProduct), new { id });
    }

    private string? GetLink(string linkName, int page, int pageSize)
    {
        return _linkGenerator.GetUriByName(
                    HttpContext,
                    linkName,
                    new { page, pageSize }
                    );
    }
    

}
