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
    public class StorageController : Controller
    {
        private readonly IStorageRepository _storageRepository;
        private readonly IStorageProductRepository _storageProductRepository;
        private readonly IStorageKeeperRepository _storageKeeperRepository;
        private readonly IMapper _mapper;

        public StorageController(IStorageRepository storageRepository,
            IStorageKeeperRepository storageKeeperRepository,
            IStorageProductRepository storageProductRepository,
            IMapper mapper)
        {
            _storageProductRepository = storageProductRepository;
            _storageKeeperRepository = storageKeeperRepository;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        // GET: api/Storage
        [HttpGet]
        public async Task<IActionResult> GetAllStorages()
        {
            var storages = _mapper.Map<List<StorageDetailsDTO>>(await _storageRepository.GetAllStoragesAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(storages);
        }

        // GET: api/Storage/{storageId}
        [HttpGet("{storageId}")]
        public async Task<IActionResult> GetStorageById(int storageId)
        {
            if (!await _storageRepository.StorageExistsAsync(storageId))
                return NotFound();

            var storage = _mapper.Map<StorageDetailsDTO>(await _storageRepository.GetStorageByIdAsync(storageId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(storage);
        }

        // POST: api/Storage
        [HttpPost]
        public async Task<IActionResult> CreateStorage([FromBody] StorageDTO storageCreate)
        {
            if (storageCreate == null)
                return BadRequest(ModelState);

            var storage = (await _storageRepository.GetAllStoragesAsync())
                .Where(s => s.Name.Trim().ToUpper() == storageCreate.Name.Trim().ToUpper()).FirstOrDefault();

            if (storage != null)
            {
                ModelState.AddModelError("", "Storage with the same name already exists");
                return StatusCode(422, ModelState);
            }

            storage = (await _storageRepository.GetAllStoragesAsync())
                .Where(s => s.City.Trim().ToUpper() == storageCreate.City.Trim().ToUpper() &&
                s.Region.Trim().ToUpper() == storageCreate.Region.Trim().ToUpper() &&
                s.Address.Trim().ToUpper() == storageCreate.Address.Trim().ToUpper()).FirstOrDefault();

            if (storage != null)
            {
                ModelState.AddModelError("", "Storage with the same location already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storageMap = _mapper.Map<Storage>(storageCreate);

            if (!await _storageRepository.CreateStorageAsync(storageMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        // PUT: api/Storage/{storageId}
        [HttpPut("{storageId}")]
        public async Task<IActionResult> UpdateStorage(int storageId, StorageDTO storageUpdate)
        {
            try
            {
                if (storageUpdate is null)
                    return BadRequest(ModelState);

                if (storageId != storageUpdate.StorageId)
                    return BadRequest(ModelState);

                if (!await _storageRepository.StorageExistsAsync(storageId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var storageMap = _mapper.Map<Storage>(storageUpdate);

                if (!await _storageRepository.UpdateStorageAsync(storageMap))
                {
                    ModelState.AddModelError("", "Something went wrong while updating");
                    return StatusCode(500, ModelState);
                }

                return Ok("Successfully updated!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating. {ex.Message}");
            }
        }

        // DELETE: api/Storage/{storageId}
        [HttpDelete("{storageId}")]
        public async Task<IActionResult> DeleteStorage(int storageId)
        {
            try
            {
                if (!await _storageRepository.StorageExistsAsync(storageId))
                return NotFound();

            var storageToDelete = await _storageRepository.GetStorageByIdAsync(storageId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var storageProducts = await _storageProductRepository.GetStorageProductsByStorageIdAsync(storageId);

            if (storageProducts.Any())
            {
                ModelState.AddModelError("", "This product has storage products referencing to him.");
                return BadRequest(ModelState);
            }

            var storageKeepers = await _storageKeeperRepository.GetKeepersByStorageIdAsync(storageId);

            if (storageKeepers.Any())
            {
                ModelState.AddModelError("", "This product has storage keepers referencing to him.");
                return BadRequest(ModelState);
            }

            if (storageToDelete.StorageProducts.Count > 0 || storageToDelete.StorageKeepers.Count > 0)
                ModelState.AddModelError("", "This storage has storage products or storage keepers referencing to him.");



            if (!await _storageRepository.DeleteStorageAsync(storageToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting storage");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully deleted!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting {ex.Message}");
            }
        }
    }
}
