using System.Collections.Generic;
using System.Threading.Tasks;

namespace Summer.WebTools.Tools.Generator
{
    public interface IProjectGenerator
    {
        public Task<byte[]> MakeProject(ProjectModel projectModel);
    }
}