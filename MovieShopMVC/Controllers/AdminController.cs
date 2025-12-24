using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly IMovieService _movieService;

    public AdminController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public IActionResult CreateMovie()
    {
        return View();
    }

    [HttpGet]
    public IActionResult TopMovies()
    {
        var movies = _movieService.GetTop20GrossingMovies();
        return View(movies);
    }

    [HttpGet]
    public IActionResult TopPurchasedMovies()
    {
        var movies = _movieService.GetTopPurchasedMovies(20);
        return View(movies);
    }
}
