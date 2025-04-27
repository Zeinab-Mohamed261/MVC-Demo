using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> allowedExtensions = new List<string> { ".jpg", ".png", ".jpeg" };
        const int maxSize = 2_097_152; //2MB   // const => don't Change 
        public string? Upload(IFormFile file, string folderName)
        {
            //1.Check Extension
            var extension = Path.GetExtension(file.FileName);//.png
            if(!allowedExtensions.Contains(extension)) return null;

            //2.Check Size
            if(file.Length ==0 || file.Length > maxSize) return null;

            //3.Get Located Folder Path
            //var folderPath = "C:\\Users\\PC\\Downloads\\DemoV01\\Demo.PL\\wwwroot\\Files\\images\\";  //static [local] invalid
            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\Files\\{folderName}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory() , "wwwroot" , "Files" , folderName);

            //4.Make Attachment Name Unique-- GUID
            var fileName = $"{Guid.NewGuid()}_{file.FileName}"; //image1.hamadaaa.png => 1f5b6a3b-0f8c-4e9a-ae7d-4f6b3c7c0a9c.png

            //5.Get File Path
            var filePath = Path.Combine(folderPath, fileName);

            //6.Create File Stream To Copy File[Unmanaged]
            using FileStream fs = new FileStream(filePath, FileMode.Create);

            //7.Use Stream To Copy File
            file.CopyTo(fs);

            //8.Return FileName To Store In Database
            return fileName;
        }
        public bool Delete(string filePath)
        {
            if (!File.Exists(filePath)) return false;
            else
            {
                File.Delete(filePath);
                return true;
            }
        }
    }
}
