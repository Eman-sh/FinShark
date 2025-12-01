using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models.Domain;
using api.Models.DTO;
using api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(ApplicationDBContext context, IRegionRepository regionRepository, IMapper mapper)
        {
            _context = context;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            //Get the Data from Database - Domain models 
            var regionsDomain = await regionRepository.GetAllAsync();

            var regionDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionsDomain = await regionRepository.GetByIdAsync(id);


            if (regionsDomain == null)
            {
                return NotFound();
            }

            //Map Domain model to DTO
            var regionDto = mapper.Map<RegionDto>(regionsDomain);
            return Ok(regionDto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDto addRegionDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionDto);

            //Add Region to Database
            regionDomainModel = await regionRepository.CreateRegionAsync(regionDomainModel);

            //Map Domain Model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }



        //Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionDomainModel = mapper.Map<Region>(updateRegionDto);
            //Find the region in the database
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            //Find the region in the database
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);
        }



        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //Find the region in the database
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }

    }
}