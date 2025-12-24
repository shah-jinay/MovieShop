using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
    public class MoviesController: Controller
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        //GET: MoviesController
        public IActionResult Index(int? genreId, int page = 1)
        {
            const int pageSize = 8;

            var model = new GenreMoviesViewModel
            {
                Genres = _movieService.GetAllGenres(),
                SelectedGenreId = genreId,
                PageSize = pageSize
            };

            var allMovies = genreId.HasValue
                ? _movieService.GetMoviesByGenre(genreId.Value)
                : _movieService.GetTop20GrossingMovies();

            model.TotalCount = allMovies.Count;
            model.TotalPages = model.TotalCount == 0 ? 1 : (int)Math.Ceiling(model.TotalCount / (double)pageSize);
            model.CurrentPage = Math.Clamp(page, 1, model.TotalPages);
            var skip = (model.CurrentPage - 1) * pageSize;
            model.Movies = allMovies.Skip(skip).Take(pageSize).ToList();

            if (model.TotalPages > 1)
            {
                const int maxVisiblePages = 10;
                var links = new List<PaginationLink>();
                int startPage = Math.Max(1, model.CurrentPage - maxVisiblePages / 2);
                int endPage = startPage + maxVisiblePages - 1;
                if (endPage > model.TotalPages)
                {
                    endPage = model.TotalPages;
                    startPage = Math.Max(1, endPage - maxVisiblePages + 1);
                }

                if (startPage > 1)
                {
                    links.Add(new PaginationLink
                    {
                        PageNumber = 1,
                        Url = Url.Action("Index", new { genreId, page = 1 }) ?? string.Empty,
                        IsCurrent = model.CurrentPage == 1
                    });

                    if (startPage > 2)
                    {
                        links.Add(new PaginationLink { IsEllipsis = true });
                    }
                }

                for (var p = startPage; p <= endPage; p++)
                {
                    links.Add(new PaginationLink
                    {
                        PageNumber = p,
                        Url = Url.Action("Index", new { genreId, page = p }) ?? string.Empty,
                        IsCurrent = p == model.CurrentPage
                    });
                }

                if (endPage < model.TotalPages)
                {
                    if (endPage < model.TotalPages - 1)
                    {
                        links.Add(new PaginationLink { IsEllipsis = true });
                    }

                    links.Add(new PaginationLink
                    {
                        PageNumber = model.TotalPages,
                        Url = Url.Action("Index", new { genreId, page = model.TotalPages }) ?? string.Empty,
                        IsCurrent = model.CurrentPage == model.TotalPages
                    });
                }

                var pagination = new PaginationViewModel
                {
                    CurrentPage = model.CurrentPage,
                    TotalPages = model.TotalPages,
                    PreviousPageUrl = model.CurrentPage > 1 ? Url.Action("Index", new { genreId, page = model.CurrentPage - 1 }) : null,
                    NextPageUrl = model.CurrentPage < model.TotalPages ? Url.Action("Index", new { genreId, page = model.CurrentPage + 1 }) : null,
                    Links = links
                };
                ViewBag.Pagination = pagination;
            }
            else
            {
                ViewBag.Pagination = null;
            }

            return View(model);
        }
        
        [HttpGet]
        public IActionResult MovieDetails(int id)
        {
            var movie = _movieService.GetMovieDetails(id);
            return View(movie);
        }
        [HttpPost]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _movieService.DeleteMovie(id);
            if (movie == false)
            {
                return NotFound();
            }
            _movieService.DeleteMovie(id);
            return RedirectToAction("Index", "Home");
        }
    }
}  
