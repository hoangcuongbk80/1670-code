using Microsoft.AspNetCore.Mvc;
using FPTJobMatch.Models;
using System.Collections.Generic;

namespace FPTJobMatch.Controllers
{
    public class JobsController : Controller
    {
        private static List<Job> jobs = new List<Job>
        {
            new Job { Id = 1, Title = "Software Engineer", Description = "Develop web applications", Company = "FPT", Location = "Hanoi", PostedDate = DateTime.Now },
            new Job { Id = 2, Title = "Data Analyst", Description = "Analyze business data", Company = "FPT", Location = "Da Nang", PostedDate = DateTime.Now }
        };

        private static List<JobApplication> applications = new List<JobApplication>();

        public IActionResult Details(int id)
        {
            var job = jobs.Find(j => j.Id == id);
            return View(job);
        }

        [HttpGet]
        public IActionResult Apply(int jobId)
        {
            var job = jobs.Find(j => j.Id == jobId);
            ViewBag.JobTitle = job?.Title;
            return View(new JobApplication { JobId = jobId });
        }

        [HttpPost]
        public IActionResult Apply(JobApplication application)
        {
            applications.Add(application);
            return RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            return View();
        }

        public IActionResult Applications()
        {
            return View(applications);
        }
        public IActionResult Index(string searchTerm, int page = 1)
        {
            const int PageSize = 5; // Number of jobs per page
            var filteredJobs = jobs
                .Where(j => string.IsNullOrEmpty(searchTerm) ||
                            j.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            j.Location.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();

            var paginatedJobs = filteredJobs
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            ViewBag.TotalPages = Math.Ceiling((double)filteredJobs.Count / PageSize);
            ViewBag.CurrentPage = page;
            ViewBag.SearchTerm = searchTerm;

            return View(paginatedJobs);
        }
        public IActionResult Dashboard()
        {
            var jobStats = jobs
                .GroupBy(j => j.Location)
                .Select(g => new { Location = g.Key, Count = g.Count() })
                .ToList();

            ViewBag.JobStats = jobStats;

            return View(jobs);
        }

        // Display the form to create a new job
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Handle form submission for creating a new job
        [HttpPost]
        public IActionResult Create(Job newJob)
        {
            if (ModelState.IsValid)
            {
                newJob.Id = jobs.Max(j => j.Id) + 1; // Generate a unique ID for the new job
                newJob.PostedDate = DateTime.Now;    // Set the posted date to the current date
                jobs.Add(newJob);
                return RedirectToAction("Index");   // Redirect to the job listing page
            }
            return View(newJob); // Return the form with validation errors if invalid
        }
    }
}
