using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IFileService
    {
        IResult ImageUpload(IFormFile file, int carId, List<string> FileExtensions);        
        IResult ImageDelete(string imagePath);
    }
}
