using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class FileManager : IFileService
    {
        private static string _wwwRoot = "wwwroot";

        public IResult ImageUpload(IFormFile file, int carId,List<string> filters)
        {
            
            string fileExtension = Path.GetExtension(file.FileName);
            var checkFilter = VerifyFileExtensions(filters, fileExtension);

            if (checkFilter)
            {
                
                string newFileName = string.Format("{0:D}{1}", Guid.NewGuid(), fileExtension);
                string imageFolder = Path.Combine(_wwwRoot, carId.ToString());
                string fullImagePath = Path.Combine(imageFolder, newFileName);
                string İmagePath = string.Format("/" + carId.ToString() + "/{0}", newFileName);
                if (!Directory.Exists(imageFolder))
                    Directory.CreateDirectory(imageFolder);

                using (var fileStream = File.Create(fullImagePath))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                
                return new SuccessResult(İmagePath);
            }

            var errorMessage = string.Join(" ", filters);
            return new ErrorResult($"Sadece {errorMessage} uzantılı dosyalar yükleyiniz");

        }
        public IResult ImageDelete(string imagePath)
        {
            string path = _wwwRoot+imagePath.Replace("/", @"\\");
            
           
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                return new SuccessResult();
            }
            return new ErrorResult();
        }
       

        private bool VerifyFileExtensions(List<string> filters, string fileExtention)
        {          

            return filters.Contains(fileExtention);
        }
    }
}
