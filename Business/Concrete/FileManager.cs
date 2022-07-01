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
            
            string resimUzantisi = Path.GetExtension(file.FileName);
            var checkFilter = VerifyFileExtensions(filters, resimUzantisi);

            if (checkFilter)
            {
                
                string yeniResimAdi = string.Format("{0:D}{1}", Guid.NewGuid(), resimUzantisi);
                string imageKlasoru = Path.Combine(_wwwRoot, carId.ToString());
                string tamResimYolu = Path.Combine(imageKlasoru, yeniResimAdi);
                string webResimYolu = string.Format("/" + carId.ToString() + "/{0}", yeniResimAdi);
                if (!Directory.Exists(imageKlasoru))
                    Directory.CreateDirectory(imageKlasoru);

                using (var fileStream = File.Create(tamResimYolu))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }
                
                return new SuccessResult(webResimYolu);
            }

            var errorMessage = string.Join(" ", filters);
            return new ErrorResult($"Sadece {errorMessage} uzantılı dosyalar yükleyiniz");

        }

        private bool VerifyFileExtensions(List<string> filters, string fileExtention)
        {          

            return filters.Contains(fileExtention);
        }
    }
}
