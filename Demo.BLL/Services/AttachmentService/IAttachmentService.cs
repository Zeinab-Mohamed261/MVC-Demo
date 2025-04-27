using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        //Upload
        public string? /*fileName*/ Upload(IFormFile file, string folderName);
        //Delete
        public bool Delete(string fileName , string folderName);
    }
}
