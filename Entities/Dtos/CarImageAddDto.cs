using Core.Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CarImageAddDto :IDto
    {
        public CarImage car { get; set; }
        public List<IFormFile>  files { get; set; }
    }
}
