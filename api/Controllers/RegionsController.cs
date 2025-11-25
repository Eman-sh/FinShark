using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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

            //return DTOs
            return Ok(regions);
        }//
        [HttpGet("{id}")]
        public IActionResult GetRegionById([FromRoute] int id)
        {
            var region = _context.Region.Find(id);
            if (region == null)
            {
                return NotFound();
            }
            return Ok(region);
        }
            }
}