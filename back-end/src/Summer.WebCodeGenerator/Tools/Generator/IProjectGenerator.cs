using System.Threading.Tasks;

namespace Summer.WebCodeGenerator.Tools.Generator
{
    public interface IProjectGenerator
    {
        public Task<byte[]> MakeProject(ProjectModel projectModel);
    }
}