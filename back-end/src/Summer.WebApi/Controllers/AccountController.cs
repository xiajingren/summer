﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Summer.Application.Dtos;
using Summer.Application.Services;

namespace Summer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterInputDto input)
        {
            var output = await _identityService.Register(input);
            if (!output.Success)
            {
                return BadRequest(output.Errors);
            }

            return Ok(output.Data);
        }
    }
}