using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.ContextDb;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ApplicationContext>, ICarDal
    {
        public CarDetailDto GetCarDetail(int carId)
        {
            using (ApplicationContext applicationContext = new ApplicationContext())
            {
                var result = from car in applicationContext.Cars
                             join color in applicationContext.Colors
                             on car.ColorId equals color.Id
                             join brand in applicationContext.Brands
                             on car.BrandId equals brand.Id

                             select (new CarDetailDto
                             {
                                 Id = car.Id,
                                 BrandName = brand.Name,
                                 ColorName = color.Name,
                                 ModelYear = car.ModelYear,
                                 DailyPrice = car.DailyPrice,
                                 Description = car.Description,
                                 Images = applicationContext.CarImages.Where(i => i.CarId == car.Id).ToList()

                             });
                           

                return result.FirstOrDefault(c=>c.Id == carId);


            }
        }
    }
}
