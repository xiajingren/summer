using System;
using Summer.App.Contracts.Core;

namespace Summer.App.Contracts.Business.Dtos
{
    public class UploadFileOutputDto
    {
        public Guid? Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public string FileUrl
        {
            get
            {
                if (Id == null)
                    return null;

                return $"{AppGlobal.HttpContext?.Request.Scheme}://{AppGlobal.HttpContext?.Request.Host}" +
                       $"{AppGlobal.HttpContext?.Request.PathBase}/api/Sys/UploadFile/Download/{Id}";
            }
        }

    }
}