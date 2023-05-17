using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using BooksStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BooksStore.Areas.Identity.Data;
using BooksStore.Data;
using BooksStore.Models;

namespace BooksStore.Controllers
{
    public class UserBooksController : Controller
    {
        private readonly BooksStoreContext _context;
        private readonly UserManager<BooksStoreUser> _userManager;

        public UserBooksController(BooksStoreContext context, UserManager<BooksStoreUser> usermanager)
        {
            _context = context;
            _userManager = usermanager;
        }

        private Task<BooksStoreUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddToMyBooks(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var MVCUserBookContext = _context.UserBook.Where(m => m.BookId == id);
            var user = await GetCurrentUserAsync();
            if (ModelState.IsValid)
            {
                UserBook userBook = new UserBook();
                userBook.BookId = (int)id;
                userBook.AppUser = user.UserName;
                _context.UserBook.Add(userBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MoiKnigi));
            }
            if (MVCUserBookContext != null)
            {
                return View(await MVCUserBookContext.ToListAsync());
            }
            else
            {
                return Problem("Entity set 'BooksStoreContext.UserBook' is null!");
            }
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MoiKnigi()
        {
            var user = await GetCurrentUserAsync();
            var MVCUserBookContext = _context.UserBook.AsQueryable().Include(m => m.Book).ThenInclude(m => m.Author).Where(m => m.AppUser == user.UserName);
            var MyBooksList = _context.Book.AsQueryable();
            MyBooksList = MVCUserBookContext.Select(m => m.Book);
            if (MVCUserBookContext != null)
            {
                return View("~/Views/UserBooks/MyBooks.cshtml", await MyBooksList.ToListAsync());
            }
            else
            {
                return Problem("Entity set 'BooksStoreContext.UserBook' is null!");
            }
        }

        // GET: UserBooks
        public async Task<IActionResult> Index()
        {
            var mVCBookContext = _context.UserBook.Include(u => u.Book);
            return View(await mVCBookContext.ToListAsync());
        }

        // GET: UserBooks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserBook == null)
            {
                return NotFound();
            }

            var userBook = await _context.UserBook
                .Include(u => u.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBook == null)
            {
                return NotFound();
            }

            return View(userBook);
        }

        // GET: UserBooks/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title");
            return View();
        }

        // POST: UserBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AppUser,BookId")] UserBook userBook)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userBook);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", userBook.BookId);
            return View(userBook);
        }

        // GET: UserBooks/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserBook == null)
            {
                return NotFound();
            }

            var userBook = await _context.UserBook.FindAsync(id);
            if (userBook == null)
            {
                return NotFound();
            }
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", userBook.BookId);
            return View(userBook);
        }

        // POST: UserBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AppUser,BookId")] UserBook userBook)
        {
            if (id != userBook.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userBook);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserBookExists(userBook.Id))
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
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Title", userBook.BookId);
            return View(userBook);
        }

        // GET: UserBooks/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserBook == null)
            {
                return NotFound();
            }

            var userBook = await _context.UserBook
                .Include(u => u.Book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userBook == null)
            {
                return NotFound();
            }

            return View(userBook);
        }

        // POST: UserBooks/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserBook == null)
            {
                return Problem("Entity set 'BooksStoreContext.UserBook'  is null.");
            }
            var userBook = await _context.UserBook.FindAsync(id);
            if (userBook != null)
            {
                _context.UserBook.Remove(userBook);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserBookExists(int id)
        {
            return (_context.UserBook?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
