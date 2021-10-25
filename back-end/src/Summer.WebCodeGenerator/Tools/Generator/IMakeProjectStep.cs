namespace Summer.WebCodeGenerator.Tools.Generator
{
    public interface IMakeProjectStep
    {
        void Execute(ProjectModel projectModel, ref EntryModel entryModel);
    }
}