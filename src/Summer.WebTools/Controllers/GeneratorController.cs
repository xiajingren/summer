using Microsoft.AspNetCore.Mvc;
using Summer.WebTools.Tools.Generator;

namespace Summer.WebTools.Controllers
{
    public class GeneratorController : Controller
    {
        private readonly IProjectGenerator _projectGenerator;

        public GeneratorController(IProjectGenerator projectGenerator)
        {
            _projectGenerator = projectGenerator;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Make(MakeProjectOptions value)
        {
            return File(_projectGenerator.MakeProject(value), "application/octet-stream", value.ProjectName + ".zip");
        }
    }
}