using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimonProj.Data;
using SimonProj.DTO;
using SimonProj.Models;

namespace SimonProj.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ProjContext _context;

        public TeachersController(ProjContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Teachers != null
                ? View(await _context.Teachers.ToListAsync())
                : Problem("Entity set 'ProjContext.Teachers'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("LastName,FirstName,JoinedDate")]
            TeacherDTO teacherDto
        )
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Teacher
                {
                    LastName = teacherDto.LastName,
                    FirstName = teacherDto.FirstName,
                    JoinedDate = teacherDto.JoinedDate
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(teacherDto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("ID,LastName,FirstName,JoinedDate")]
            TeacherDTO teacherDto
        )
        {
            if (id != teacherDto.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Teacher
                    {
                        ID = teacherDto.ID,
                        LastName = teacherDto.LastName,
                        FirstName = teacherDto.FirstName,
                        JoinedDate = teacherDto.JoinedDate
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacherDto.ID))
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

            return View(teacherDto);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Teachers == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Teachers == null)
            {
                return Problem("Entity set 'ProjContext.Teachers'  is null.");
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherExists(int id)
        {
            return (_context.Teachers?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}