using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Summer.WebCodeGenerator.Tools.Generator;

namespace Summer.WebCodeGenerator.Controllers
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

        //[HttpPost]
        public async Task<IActionResult> Make(ProjectModel value)
        {
            return File(await _projectGenerator.MakeProject(value), "application/octet-stream",
                value.ProjectName + ".zip");
        }
    }
}