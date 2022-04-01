#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MvcMovie.Models;

namespace MVCMovie.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public async Task<IActionResult> Index([FromServices] MvcMovieContext context, string movieGenre, string searchString)
        {
            IQueryable<string> genreQuery = from m in context.Movie
                orderby m.Genre
                select m.Genre;
            var movies = from m in context.Movie
                select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(movieGenre))
            {
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = await movies.ToListAsync()
            };

            return View(movieGenreVM);
        }

        // GET: Movie/Details/5
        public async Task<IActionResult> Details([FromServices] MvcMovieContext context, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] MvcMovieContext context, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Models.Movie movie)
        {
            if (ModelState.IsValid)
            {
                context.Add(movie);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movie/Edit/5
        public async Task<IActionResult> Edit([FromServices] MvcMovieContext context, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movie/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromServices] MvcMovieContext context, int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Models.Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(movie);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(context, movie.Id))
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
            return View(movie);
        }

        // GET: Movie/Delete/5
        public async Task<IActionResult> Delete([FromServices] MvcMovieContext context, int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await context.Movie
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed([FromServices] MvcMovieContext context, int id)
        {
            var movie = await context.Movie.FindAsync(id);
            context.Movie.Remove(movie);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists([FromServices] MvcMovieContext context, int id)
        {
            return context.Movie.Any(e => e.Id == id);
        }
    }
}
