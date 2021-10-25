using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Zip;

namespace Summer.WebCodeGenerator.Tools.Generator
{
    public class ProjectGenerator : IProjectGenerator
    {
        private readonly List<IMakeProjectStep> _makeProjectSteps;

        public ProjectGenerator()
        {
            _makeProjectSteps = new List<IMakeProjectStep>
            {
                new RenameStep()
            };
        }

        public async Task<byte[]> MakeProject(ProjectModel projectModel)
        {
            var tempFilePath = Path.Combine(AppContext.BaseDirectory, "Temp", Guid.NewGuid() + ".zip");

            await using (var zipInputStream = new ZipInputStream(File.OpenRead(projectModel.TemplateFilePath)))
            {
                await using (var fileStream = File.Create(tempFilePath))
                {
                    await using (var zipOutputStream = new ZipOutputStream(fileStream))
                    {
                        ZipEntry sourceEntry;
                        while ((sourceEntry = zipInputStream.GetNextEntry()) != null)
                        {
                            var streamReader = new StreamReader(zipInputStream);
                            var content = await streamReader.ReadToEndAsync();

                            var entryModel = new EntryModel
                                {Name = sourceEntry.Name, Content = content, IsFile = sourceEntry.IsFile};

                            //todo:处理文件内容
                            _makeProjectSteps.ForEach(p => { p.Execute(projectModel, ref entryModel); });

                            if (entryModel.IsRemove) continue;

                            var targetEntry =
                                new ZipEntry(entryModel.Name)
                                {
                                    DateTime = DateTime.Now
                                };
                            zipOutputStream.PutNextEntry(targetEntry);

                            if (string.IsNullOrEmpty(entryModel.Content)) continue;

                            var streamWriter = new StreamWriter(zipOutputStream);
                            await streamWriter.WriteAsync(entryModel.Content);
                            await streamWriter.FlushAsync();
                        }
                    }
                }
            }

            var data = await File.ReadAllBytesAsync(tempFilePath);
            File.Delete(tempFilePath);
            return data;
        }
    }
}