using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class JobsController : Controller
    {
        private static List<Job> jobs = new List<Job>
        {
            new Job { Id = 1, Title = "Software Engineer", Description = "Develop web applications", 
                      Company = "FPT", Location = "Hanoi", PostedDate = DateTime.Now },
            new Job { Id = 2, Title = "Data Analyst", Description = "Analyze business data", 
                      Company = "FPT", Location = "Da Nang", PostedDate = DateTime.Now }
        };
        public IActionResult Index()
        {
            return View(jobs);
        }
        public IActionResult Details(int id)
        {
            var job = jobs.Find(j => j.Id == id);
            return View(job);
        }
        
        private static List<JobApplication> applications = new List<JobApplication>();
        public IActionResult Apply(int jobId){
            var job = jobs.Find(j => j.Id == jobId);
            ViewBag.JobTitle = job?.Title;
            return View(new JobApplication { JobId = jobId });
        }
        [HttpPost]
        public IActionResult Apply(JobApplication application){
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
    }
}
