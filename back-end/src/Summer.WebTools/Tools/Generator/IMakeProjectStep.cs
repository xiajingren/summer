namespace Summer.WebTools.Tools.Generator
{
    public interface IMakeProjectStep
    {
        void Execute(ProjectModel projectModel, ref EntryModel entryModel);
    }
}