using System;
using System.ComponentModel.DataAnnotations;

namespace Summer.App.Contracts.Business.Dtos
{
    public class UploadFileOutputDto
    {
        public Guid? Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string FileUrl { get; set; }
    }
}