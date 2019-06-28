using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using B3.Models;

namespace Karaoke.Controllers.Admin
{
    public class ConfigsController : Controller
    {
        private readonly DatabaseContext _context;

        public ConfigsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Configs
        public IActionResult Index()
        {
            return View();
        }

        // GET: Configs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configModel = await _context.Configs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configModel == null)
            {
                return NotFound();
            }

            return View(configModel);
        }

        // GET: Configs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Configs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Address,HotLine,OpenTime,Status,CreatedBy,EditedBy,CreatedTime,UpdatedTime")] ConfigModel configModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configModel);
        }

        // GET: Configs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configModel = await _context.Configs.FindAsync(id);
            if (configModel == null)
            {
                return NotFound();
            }
            return View(configModel);
        }

        // POST: Configs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Address,HotLine,OpenTime,Status,CreatedBy,EditedBy,CreatedTime,UpdatedTime")] ConfigModel configModel)
        {
            if (id != configModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigModelExists(configModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(configModel);
        }

        // GET: Configs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configModel = await _context.Configs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (configModel == null)
            {
                return NotFound();
            }

            return View(configModel);
        }

        // POST: Configs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configModel = await _context.Configs.FindAsync(id);
            _context.Configs.Remove(configModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigModelExists(int id)
        {
            return _context.Configs.Any(e => e.Id == id);
        }
    }
}
