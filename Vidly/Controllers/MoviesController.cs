using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Linq;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var movies = _context.Movies.Include(m => m.GenreType).ToList(); ;

            return View(movies);
        }

        public ViewResult Details (int id)
        {
            var movie = _context.Movies.Include(m => m.GenreType).SingleOrDefault(c => c.Id == id);
            return View(movie);
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Title = "Shrek" },
                new Movie { Id = 2, Title = "Wall-E" }
            };
        }

        // GET: Movies
        public ActionResult Random()
        {
            var movie = new Movie() {Id = 1,  Title = "Shrek!" };

            var customers = new List<Customer>
            {
                new Customer {Name = "Customer 1" },
                new Customer {Name = "Customer 2" }
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            //return Content("Hello world!");
            //return HttpNotFound();
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "Title" });
        }


        public ActionResult New()
        {
            var genreTypes = _context.GenreTypes.ToList();
            var viewModel = new MovieFormViewModel
            {
                GenreTypes = genreTypes
            };
            return View("MovieForm", viewModel);

            //var membershipTypes = _context.MembershipTypes.ToList();
            //var viewModel = new CustomerFormViewModel
            //{
            //    MembershipTypes = membershipTypes
            //};
            //return View("CustomerForm", viewModel);
        }

        public ActionResult Edit(int id)
        {

            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                GenreTypes = _context.GenreTypes.ToList()
            };
            return View("MovieForm", viewModel);
        }


        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);

                movieInDb.Title = movie.Title;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.GenreTypeId = movie.GenreTypeId;
                movieInDb.InStock = movie.InStock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;

        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "Name";

        //    return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        //}
        //[Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        //public ActionResult ByReleaseDate(int year, int month)
        //{
        //    return Content(year + "/" + month);
        //}






    }
}