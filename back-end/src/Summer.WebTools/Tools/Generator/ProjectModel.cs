using System;
using System.IO;

namespace Summer.WebTools.Tools.Generator
{
    public class ProjectModel
    {
        public string ProjectName { get; set; } = "MyProject";

        public string TemplateName { get; set; } = "Summer_1.0.0";

        public string TemplateFilePath => Path.Combine(AppContext.BaseDirectory, "Templates", TemplateName + ".zip");
    }
}