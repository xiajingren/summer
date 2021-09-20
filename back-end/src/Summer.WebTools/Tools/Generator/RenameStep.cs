namespace Summer.WebTools.Tools.Generator
{
    public class RenameStep : IMakeProjectStep
    {
        public void Execute(ProjectModel projectModel, ref EntryModel entryModel)
        {
            entryModel.Name = entryModel.Name?.Replace("Summer", projectModel.ProjectName);
            entryModel.Content = entryModel.Content?.Replace("Summer", projectModel.ProjectName);
        }
    }
}