using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B3.Models;

namespace Karaoke.Controllers.Admin.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ConfigsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Configs
        [HttpGet]
        public IEnumerable<ConfigModel> GetConfigs()
        {
            return _context.Configs;
        }
        // POST: api/Configs
        [HttpPost("GetList")]
        public IActionResult GetList()
        {
            var request = Request.Form;
            var listdata = _context.Configs.ToList();
            int length = Convert.ToInt32(request["length"].ToString());
            int start = Convert.ToInt32(request["start"].ToString());
            var listConfigs = listdata.Skip(start).Take(length).ToList();
            var result = new
            {
                data = listConfigs,
                recordsTotal = listdata.Count,
                recordsFiltered = listdata.Count,
                draw= request["draw"],
                Test =request
              
            };
            return Ok(result);
        }
        // GET: api/Configs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetConfigModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configModel = await _context.Configs.FindAsync(id);

            if (configModel == null)
            {
                return NotFound();
            }

            return Ok(configModel);
        }

        // PUT: api/Configs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfigModel([FromRoute] int id, [FromBody] ConfigModel configModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != configModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(configModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Configs
        [HttpPost]
        public async Task<IActionResult> PostConfigModel([FromBody] ConfigModel configModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Configs.Add(configModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfigModel", new { id = configModel.Id }, configModel);
        }

        // DELETE: api/Configs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfigModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var configModel = await _context.Configs.FindAsync(id);
            if (configModel == null)
            {
                return NotFound();
            }

            _context.Configs.Remove(configModel);
            await _context.SaveChangesAsync();

            return Ok(configModel);
        }

        private bool ConfigModelExists(int id)
        {
            return _context.Configs.Any(e => e.Id == id);
        }
    }
}