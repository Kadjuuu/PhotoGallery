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
        private ImageDbContext _DbContext;
        public APIController(ImageDbContext DbContext)
        {
            _DbContext = DbContext;

        }

        [HttpGet("Images")]
        public IActionResult Get()
        {
            try
            {
                var Images = _DbContext.Images.ToList();
                if (Images.Count == 0)
                {
                    return StatusCode(404, "No user found");
                }
                return Ok(Images);
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
        }
            

        [HttpPost("CreateUser")]
        public IActionResult Create([FromBody] ImageModel Photos)
        {
            ImageModel image = new ImageModel();
            image.Title = Photos.Title;
            image.Author = Photos.Author;
            image.ImageFile = Photos.ImageFile;
            image.ImageName = Photos.ImageName;

            try
            {
                _DbContext.Images.Add(image);
                _DbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occurred");
            }
            var images = _DbContext.Images.ToList();
            return Ok(images);
        }

        [HttpPut("UpdateUser")]
        public IActionResult Update([FromBody] ImageModel Photos)
        {
            

            try
            {
                var image = _DbContext.Images.FirstOrDefault(x => x.ImageId == Photos.ImageId);
                if (image == null)
                {
                    return StatusCode(404, "User not found");
                }
                image.Title = Photos.Title;
                image.Author = Photos.Author;
                image.ImageFile = Photos.ImageFile;
                image.ImageName = Photos.ImageName;

                _DbContext.Entry(image).State = EntityState.Modified;
                _DbContext.SaveChanges();

            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error has occurred");
            }
            var images = _DbContext.Images.ToList();
            return Ok(images);
        }

        [HttpDelete("DeleteUser/{ImageId}")]
        public IActionResult Delete([FromRoute] ImageModel Photos)
        {
            try
            {
                var image = _DbContext.Images.FirstOrDefault(x => x.ImageId == Photos.ImageId);
                if (image == null)
                {
                    return StatusCode(404, "User not found");
                }
                _DbContext.Entry(image).State = EntityState.Deleted;
                _DbContext.SaveChanges();
            }
            catch (Exception)
            {

                return StatusCode(500, "An error has occurred");
            }
            var images = _DbContext.Images.ToList();
            return Ok(images);
        }

    }
}
