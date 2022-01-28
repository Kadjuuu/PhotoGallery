using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using AuthSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private AuthDbContext _DbContext;
        public APIController(AuthDbContext DbContext)
        {
            _DbContext = DbContext;

        }

        [HttpGet("GetUsers")]
        public IActionResult Get()
        {
            try
            {
                var users = _DbContext.Users.ToList();
                if (users.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(users);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }

        }


    }
}
