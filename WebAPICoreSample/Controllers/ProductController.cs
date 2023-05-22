using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPICoreSample.Interface;
using WebAPICoreSample.Models;

namespace WebAPICoreSample.Controllers
{
    [Route("api/product")]
    [ApiController, Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _IProduct;
        public ProductController(IProduct IProduct)
        {
            _IProduct = IProduct;
        }
        [HttpGet]
        [Route("ProductsList")]
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await Task.FromResult(_IProduct.GetProductDetails());
        }
        [HttpGet]
        [Route("ProductDetail")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await Task.FromResult(_IProduct.GetProductDetails(id));
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }
        [HttpPost]
        [Route("CreateProduct")]
        public async Task<ActionResult<Product>> Post(Product products)
        {
            _IProduct.AddProduct(products);
            return await Task.FromResult(products);
        }

        [HttpPost]
        [Route("DeleteProduct")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = _IProduct.DeleteProduct(id);
            return await Task.FromResult(product);
        }

        [HttpPost]
        [Route("UpdateProduct")]
        public async Task<ActionResult<Product>> Put(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            try
            {
                _IProduct.UpdateProduct(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(product);
        }

        private bool ProductExists(int id)
        {
            return _IProduct.CheckEmployee(id);
        }
    }
}
