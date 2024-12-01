using ApplicationLayer.DTO;
using AutoMapper;
using DomainLayer.Models;
using DomainLayer.RepositoryInterfaces;
using InfrastructureLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace Lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageProductController : Controller
    {
        private readonly IStorageProductRepository _storageProductRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public StorageProductController(IStorageProductRepository storageProductRepository,
            IStorageRepository storageRepository, IProductRepository productRepository, IMapper mapper)
        {
            _storageProductRepository = storageProductRepository;
            _storageRepository = storageRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        // GET: api/StorageProduct
        [HttpGet]
        public async Task<IActionResult> GetAllStorageProducts()
        {
            var storageProducts = _mapper.
                Map<List<StorageProductDetailsDTO>>(await _storageProductRepository.GetAllStorageProductsAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(storageProducts);
        }

        // GET: api/StorageProduct/{productId}/{storageProductId}
        [HttpGet("{productId}/{storageId}")]
        public async Task<IActionResult> GetStorageProductById(int productId, int storageId)
        {
            if (!await _storageProductRepository.StorageProductExistsAsync(productId, storageId))
                return NotFound();

            var storageProduct = _mapper.
                Map<StorageProductDetailsDTO>(await _storageProductRepository.GetStorageProductByIdAsync(productId, storageId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(storageProduct);
        }

        // POST: api/StorageProduct
        [HttpPost]
        public async Task<IActionResult> CreateStorageProduct([FromBody] StorageProductDTO productCreate)
        {
            if (productCreate == null)
                return BadRequest(ModelState);

            if (productCreate.Quantity <= 0)
            {
                ModelState.AddModelError("", "Quantity must be more than zero.");
                return StatusCode(422, ModelState);
            }

            var storage = await _storageRepository.GetStorageByIdAsync(productCreate.StorageId);
            if (storage is null)
            {
                ModelState.AddModelError("", $"There are no storages with id: {productCreate.StorageId}");
                return StatusCode(422, ModelState);
            }

            var product = await _productRepository.GetProductByIdAsync(productCreate.ProductId);
            if (product is null)
            {
                ModelState.AddModelError("", $"There are no products with id: {productCreate.ProductId}");
                return StatusCode(422, ModelState);
            }

            var storageProduct = (await _storageProductRepository.GetAllStorageProductsAsync())
                .Where(sp => sp.ProductId == productCreate.ProductId && sp.StorageId == productCreate.StorageId).FirstOrDefault();

            if (storageProduct != null)
            {
                ModelState.AddModelError("", "Storage product already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productMap = _mapper.Map<StorageProduct>(productCreate);

            productMap.Storage = storage;
            productMap.Product = product;

            if (!await _storageProductRepository.CreateStorageProductAsync(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        // PUT: api/StorageProduct/{productId}/{storageId}
        [HttpPut("{productId}/{storageId}")]
        public async Task<IActionResult> UpdateStorageProduct(int productId, int storageId, StorageProductDTO storageProductUpdate)
        {
            try
            {
                if (storageProductUpdate is null)
                    return BadRequest(ModelState);

                if (storageId != storageProductUpdate.StorageId || productId != storageProductUpdate.ProductId)
                    return BadRequest(ModelState);

                if (!await _storageProductRepository.StorageProductExistsAsync(productId, storageId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (storageProductUpdate.Quantity <= 0)
                {
                    ModelState.AddModelError("", "Quantity must be more than zero.");
                    return StatusCode(422, ModelState);
                }

                var storageProductMap = _mapper.Map<StorageProduct>(storageProductUpdate);

                if (!await _storageProductRepository.UpdateStorageProductAsync(storageProductMap))
                {
                    ModelState.AddModelError("", "Something went wrong while updating");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully updated!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the product: {ex.Message}");
            }
        }

        // DELETE: api/StorageProduct/{productId}/{storageProductId}
        [HttpDelete("{productId}/{storageId}")]
        public async Task<IActionResult> DeleteStorageProduct(int productId, int storageId)
        {
            try
            {
                if (!await _storageProductRepository.StorageProductExistsAsync(productId, storageId))
                    return NotFound();

                var storageProductToDelete = await _storageProductRepository.GetStorageProductByIdAsync(productId, storageId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!await _storageProductRepository.DeleteStorageProductAsync(storageProductToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting storageProduct");
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
