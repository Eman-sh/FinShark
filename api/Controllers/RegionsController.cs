using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models.Domain;
using api.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public RegionsController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {

            //Get the Data from Database - Domain models 
            var regions = await _context.Region.ToListAsync();

            // Map Domain models to DTOs 
            var regionDto = new List<AddRegionDto>();
            regions.ForEach(region =>
            {
                regionDto.Add(new AddRegionDto
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl
                });
            });
            //return DTOs
            return Ok(regionDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
        {
            var regionDomain = await _context.Region.FindAsync(id);


            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain model to DTO
            var regionDto = new AddRegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] AddRegionDto addRegionDto)
        {
            //Map DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionDto.Code,
                Name = addRegionDto.Name,
                RegionImageUrl = addRegionDto.RegionImageUrl
            };

            //Add Region to Database
            await _context.Region.AddAsync(regionDomainModel);
            await _context.SaveChangesAsync();

            //Map Domain Model back to DTO
            var regionDto = new AddRegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }



        //Update Region
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            //Find the region in the database
            var existingRegionDomainModel = await _context.Region.FindAsync(id);
            if (existingRegionDomainModel == null)
            {
                return NotFound();
            }
            // Map DTO to Domain Model
            //Update the region properties
            existingRegionDomainModel.Name = updateRegionDto.Name;
            existingRegionDomainModel.Code = updateRegionDto.Code;
            existingRegionDomainModel.RegionImageUrl = updateRegionDto.RegionImageUrl;
            await _context.SaveChangesAsync();

            //Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = existingRegionDomainModel.Id,
                Code = existingRegionDomainModel.Code,
                Name = existingRegionDomainModel.Name,
                RegionImageUrl = existingRegionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }



        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            //Find the region in the database
            var existingRegionDomainModel = await _context.Region.FindAsync(id);
            if (existingRegionDomainModel == null)
            {
                return NotFound();
            }
            _context.Region.Remove(existingRegionDomainModel);
            await _context.SaveChangesAsync();

            //Map Domain model to DTO
            var regionDto = new AddRegionDto
            {
                Id = existingRegionDomainModel.Id,
                Name = existingRegionDomainModel.Name,
                Code = existingRegionDomainModel.Code,
                RegionImageUrl = existingRegionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

    }
}