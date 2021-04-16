namespace Summer.WebTools.Tools.Generator
{
    public interface IProjectGenerator
    {
        public byte[] MakeProject(MakeProjectOptions options);
    }
}