using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NXeShop.Models;

namespace NXeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly EWEBBANHANGDBDBMARKETSMDFContext _context;

        // Add notyF
        public INotyfService _notyfService { get; set; }

        public RolesController(EWEBBANHANGDBDBMARKETSMDFContext context, INotyfService notyfService)
        {
            _context = context;
            _notyfService = notyfService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles.ToListAsync());
        }

        // GET: Admin/Roles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) {
                return NotFound();
            }
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // GET: Admin/Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleId,RoleName,Description")] Role role)
        {
            if (ModelState.IsValid) { 
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                _notyfService.Success("Tạo mới thành công!");
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Admin/Roles/Edit?id =
        public async Task<IActionResult> Edit(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Admin/Roles/Edit?id=
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleId,RoleName,Description")] Role role)
        {
            if (id != role.RoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                    _notyfService.Success("Cập nhật thành công!");
                }
                catch(Exception ex)
                {
                    _notyfService.Success("Có lỗi xảy ra!");
                    Console.WriteLine(ex.ToString());
                }

                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Admin/Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FirstOrDefaultAsync(m => m.RoleId == id);

            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Admin/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            _notyfService.Success("Xóa quyền truy cập thành công!");
            return RedirectToAction("Index");
        }

        }
}
