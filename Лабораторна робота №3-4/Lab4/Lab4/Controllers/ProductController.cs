using ApplicationLayer.DTO;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.RepositoryInterfaces;
using InfrastructureLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IStorageProductRepository _storageProductRepository;

        public ProductController(IProductRepository productRepository,
            IStorageProductRepository storageProductRepository,
            IStorageKeeperRepository storageKeeperRepository,
            IMapper mapper)
        {
            _storageProductRepository = storageProductRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = _mapper.Map<List<ProductDetailsDTO>>(await _productRepository.GetAllProductsAsync());
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(products);
        }

        // GET: api/Product/{productId}
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            if (!await _productRepository.ProductExistsAsync(productId))
                return NotFound();

            var product = _mapper.Map<ProductDetailsDTO>(await _productRepository.GetProductByIdAsync(productId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(product);
        }



        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDTO productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            if (productCreate.Price <= 0)
            {
                ModelState.AddModelError("", "Price must be more than zero.");
                return StatusCode(422, ModelState);
            }

            if (await _productRepository.ProductExistsAsync(productCreate.ProductId) ||
                await _productRepository.ProductExistsAsync(productCreate.Name))
            {
                ModelState.AddModelError("", "Product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<Product>(productCreate);

            if (!await _productRepository.CreateProductAsync(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        // PUT: api/Product/{productId}
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, ProductDTO productUpdate)
        {
            try
            {
                if (productUpdate is null)
                    return BadRequest(ModelState);

                if (productId != productUpdate.ProductId)
                    return BadRequest(ModelState);

                if (!await _productRepository.ProductExistsAsync(productId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (productUpdate.Price <= 0)
                {
                    ModelState.AddModelError("", "Price must be more than zero.");
                    return StatusCode(422, ModelState);
                }

                var productMap = _mapper.Map<Product>(productUpdate);

                if (!await _productRepository.UpdateProductAsync(productMap))
                {
                    ModelState.AddModelError("", "Something went wrong while updating");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully updated!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding the product: {ex.Message}");
            }
        }

        // DELETE: api/Product/{productId}
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            try { 
            if (! await _productRepository.ProductExistsAsync(productId))
                return NotFound();

            var productToDelete = await _productRepository.GetProductByIdAsync(productId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storageProducts = await _storageProductRepository.GetStorageProductsByProductIdAsync(productId);

            if (storageProducts.Any())
            {
                ModelState.AddModelError("", "This product has storage products referencing to him.");
                return BadRequest(ModelState);
            }

            if (!await _productRepository.DeleteProductAsync(productToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting product");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the product: {ex.Message}");
            }
        }
    }
}
