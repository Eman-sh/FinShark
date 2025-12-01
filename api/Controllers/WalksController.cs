using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Domain;
using api.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        public WalksController(IMapper mapper)
        {
            this.mapper = mapper;

        }

        [HttpPost]
        public async Task<IActionResult> CreateWalks([FromBody] AddWalksDto addWalksDto)
        {
            var walkdDomainModel = mapper.Map<Walk>(addWalksDto);
            return Ok("Create Walks");
        }

    }
}