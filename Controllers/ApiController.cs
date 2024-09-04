using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.Services;
using MovieApi.Services.ViewModels;
using System.Diagnostics;

namespace MovieApi.Controllers
{
    [ApiController]
    [Route("api")]
    [AllowAnonymous]
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [Route("search/movie")]
        public async Task<IActionResult> Index([FromServices] ITMDBService tMDBService,string title)
        {
            var movie = await tMDBService.SearchMovie(title);
            if (movie != null)
            {
                return Ok(movie);
            }
            else
            {
                return NotFound();
            }



            return View();
        }

    }
}
