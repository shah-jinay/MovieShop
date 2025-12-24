using System.Diagnostics;
using System.Collections.Generic;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers;

public class HomeController: Controller
{
    private readonly IMovieService movieService;
    
    public HomeController(IMovieService _movieService)
    {
        movieService = _movieService;
    }
    public IActionResult Index(int page = 1)
    {
        const int pageSize = 8;
        var movies = movieService.GetTop20GrossingMovies();
        var totalCount = movies.Count;
        var totalPages = totalCount == 0 ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize);
        var currentPage = Math.Clamp(page, 1, totalPages);
        var pagedMovies = movies.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

        ViewBag.Pagination = BuildPagination(totalPages, currentPage, null);
        return View(pagedMovies);
    }

    public IActionResult Privacy()
    {
        //ViewData["key"] = value;
        // ViewData["Message"] = "Hello From ViewData Privacy Policy";
        //IDictionary <string, object> by default it take the string type as a object for any other type casting is must
        ViewBag.Message = "Hello From ViewData Privacy Policy";
        return View();
    }

    public IActionResult TopMovies()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private PaginationViewModel? BuildPagination(int totalPages, int currentPage, int? genreId)
    {
        if (totalPages <= 1)
        {
            return null;
        }

        const int maxVisiblePages = 10;
        var links = new List<PaginationLink>();
        int startPage = Math.Max(1, currentPage - maxVisiblePages / 2);
        int endPage = startPage + maxVisiblePages - 1;
        if (endPage > totalPages)
        {
            endPage = totalPages;
            startPage = Math.Max(1, endPage - maxVisiblePages + 1);
        }

        string BuildUrl(int pageNumber)
        {
            return genreId.HasValue
                ? Url.Action("Index", "Movies", new { genreId, page = pageNumber }) ?? string.Empty
                : Url.Action("Index", new { page = pageNumber }) ?? string.Empty;
        }

        if (startPage > 1)
        {
            links.Add(new PaginationLink { PageNumber = 1, Url = BuildUrl(1), IsCurrent = currentPage == 1 });
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
                Url = BuildUrl(p),
                IsCurrent = p == currentPage
            });
        }

        if (endPage < totalPages)
        {
            if (endPage < totalPages - 1)
            {
                links.Add(new PaginationLink { IsEllipsis = true });
            }
            links.Add(new PaginationLink
            {
                PageNumber = totalPages,
                Url = BuildUrl(totalPages),
                IsCurrent = currentPage == totalPages
            });
        }

        return new PaginationViewModel
        {
            CurrentPage = currentPage,
            TotalPages = totalPages,
            PreviousPageUrl = currentPage > 1 ? BuildUrl(currentPage - 1) : null,
            NextPageUrl = currentPage < totalPages ? BuildUrl(currentPage + 1) : null,
            Links = links
        };
    }
}
