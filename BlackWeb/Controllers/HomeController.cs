using BlackEngine.Models;

using BlackWeb.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics;

namespace BlackWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly string _connectionString;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _connectionString = Startup.BlackBoxConnectionString;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel(_connectionString);

            return View(viewModel.GetProfiles);
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShowSearchResults(string SearchPhrase)
        {
            SearchViewModel viewModel = new SearchViewModel(_connectionString);
            return View("Index", viewModel.GetProfiles.FindAll(p => p.FirstName.ToLower().Contains(SearchPhrase.ToLower())));
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            DetailsViewModel viewModel = new DetailsViewModel(_connectionString);

            if (id == null)
            {
                return NotFound();
            }

            var profile = viewModel.GetProfiles.Find(p => p.ID == id);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("FirstName", "LastName", "Alias", "Location", "Age", "DOB", 
            "Mobile", "Twitter", "Instagram", "Facebook", "Snapchat", "AltSocialMedia")] IProfile profile) //Can Also Use [FromForm]
        {
            CreateViewModel viewModel = new CreateViewModel(_connectionString);

            if (profile.FirstName != null)
            {
                viewModel.CreateProfile(profile);
                return RedirectToAction(nameof(Index));
            }
            return View(profile);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            EditViewModel viewModel = new EditViewModel(_connectionString);
            if (id == null)
            {
                return NotFound();
            }

            IProfile profile = viewModel.GetProfiles.Find(p => p.ID == id);
            
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("FirstName", "LastName", "Alias", "Location", "Age", "DOB",
            "Mobile", "Twitter", "Instagram", "Facebook", "Snapchat", "AltSocialMedia")] Profile profile)
        {
            EditViewModel viewModel = new EditViewModel(_connectionString);

            profile.ID = id;

            if (id != profile.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.EditProfile(profile);
                }
                catch (Exception)
                {
                    if (!ProfileExists(profile.ID))
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
            return View(profile);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            DeleteViewModel viewModel = new DeleteViewModel(_connectionString);

            if (id == null)
            {
                return NotFound();
            }

            IProfile profile = viewModel.GetProfiles.Find(p => p.ID == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            DeleteViewModel viewModel = new DeleteViewModel(_connectionString);
            IProfile profile = viewModel.GetProfiles.Find(p => p.ID == id);
            viewModel.DeleteProfile(profile);
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return true;//return _sqlData.GetProfiles.Any(e => e.Id == id);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
