using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllRegions()
        {

            //Get the Data from Database - Domain models 
            var regions = _context.Region.ToList();

            // Map Domain models to DTOs 
            var regionDto = new List<RegionDto>();
            regions.ForEach(region =>
            {
                regionDto.Add(new RegionDto
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
        public IActionResult GetRegionById([FromRoute] Guid id)
        {
            var regionDomain = _context.Region.Find(id);


            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map Domain model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }


        [HttpPost]
        public IActionResult CreateRegion([FromBody] RegionDto region)
        {
            //Map DTO to Domain Model
            var regionDomain = new Models.Domain.Region
            {
                Id = Guid.NewGuid(),
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            };

            //Add Region to Database
            _context.Region.Add(regionDomain);
            _context.SaveChanges();

            //Map Domain Model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
        }
            }
}