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
    public class StorageKeeperController : Controller
    {
        private readonly IStorageKeeperRepository _storageKeeperRepository;
        private readonly IStorageRepository _storageRepository;
        IMapper _mapper;

        public StorageKeeperController(IStorageKeeperRepository storageKeeperRepository,
            IStorageRepository storageRepository, IMapper mapper)
        {
            _storageKeeperRepository = storageKeeperRepository;
            _storageRepository = storageRepository;
            _mapper = mapper;
        }

        // GET: api/StorageKeeper
        [HttpGet]
        public async Task<IActionResult> GetAllStorageKeepers()
        {
            var storageKeepers = _mapper.Map<List<StorageKeeperDetailsDTO>>(await _storageKeeperRepository.GetAllKeepersAsync());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(storageKeepers);
        }

        // GET: api/StorageKeeper/{storageKeeperId}
        [HttpGet("{storageKeeperId}")]
        public async Task<IActionResult> GetStorageKeeperById(int storageKeeperId)
        {
            if (!await _storageKeeperRepository.KeeperExistsAsync(storageKeeperId))
                return NotFound();

            var storageKeeper = _mapper.Map<StorageKeeperDetailsDTO>(await _storageKeeperRepository.GetKeeperByIdAsync(storageKeeperId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(storageKeeper);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStorage([FromBody] StorageKeeperDTO keeperCreate)
        {
            if (keeperCreate == null)
                return BadRequest(ModelState);

            var phonePattern = @"^\+?[1-9]\d{1,14}$";
            if (!Regex.IsMatch(keeperCreate.Phone, phonePattern))
            {
                ModelState.AddModelError("", "Invalid phone number format");
                return StatusCode(422, ModelState);
            }

            var storage = await _storageRepository.GetStorageByIdAsync(keeperCreate.StorageId);
            if (storage is null)
            {
                ModelState.AddModelError("", $"There are no storages with id: {keeperCreate.StorageId}");
                return StatusCode(422, ModelState);
            }

            var keeper = (await _storageKeeperRepository.GetAllKeepersAsync())
                .Where(sk => sk.Phone.Trim().ToUpper() == keeperCreate.Phone.Trim().ToUpper()).FirstOrDefault();

            if (keeper != null)
            {
                ModelState.AddModelError("", "Storage keeper with the same phone number already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var keeperMap = _mapper.Map<StorageKeeper>(keeperCreate);

            keeperMap.Storage = storage;

            if (!await _storageKeeperRepository.CreateKeeperAsync(keeperMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created!");
        }

        // PUT: api/StorageKeeper/{storageKeeperId}
        [HttpPut("{storageKeeeperId}")]
        public async Task<IActionResult> UpdateStorageKeeper(int storageKeeeperId, StorageKeeperDTO storageKeeperUpdate)
        {
            try
            {
                if (storageKeeperUpdate is null)
                    return BadRequest(ModelState);

                if (storageKeeeperId != storageKeeperUpdate.StorageId)
                    return BadRequest(ModelState);

                if (!await _storageKeeperRepository.KeeperExistsAsync(storageKeeeperId))
                    return NotFound();

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var phonePattern = @"^\+?[1-9]\d{1,14}$";
                if (!Regex.IsMatch(storageKeeperUpdate.Phone, phonePattern))
                {
                    ModelState.AddModelError("", "Invalid phone number format");
                    return StatusCode(422, ModelState);
                }

                var storageKeeperMap = _mapper.Map<StorageKeeper>(storageKeeperUpdate);

                if (!await _storageKeeperRepository.UpdateKeeperAsync(storageKeeperMap))
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

        // DELETE: api/StorageKeeper/{storageKeeperId}
        [HttpDelete("{storageKeeperId}")]
        public async Task<IActionResult> DeleteStorageKeeper(int storageKeeperId)
        {
            try
            {
                if (!await _storageKeeperRepository.KeeperExistsAsync(storageKeeperId))
                    return NotFound();

                var storageProductToDelete = await _storageKeeperRepository.GetKeeperByIdAsync(storageKeeperId);

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (!await _storageKeeperRepository.DeleteKeeperAsync(storageProductToDelete))
                {
                    ModelState.AddModelError("", "Something went wrong deleting storageProduct");
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
