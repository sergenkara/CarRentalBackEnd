using Core.Entities;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos
{
    public class CarAddDto:IDto
    {
        public CarImage carImage { get; set; }
        public IFormFile image { get; set; }
    }
}
