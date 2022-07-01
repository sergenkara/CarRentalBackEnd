using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    internal class CarImageManager : ICarImageService
    {
        readonly private ICarImageDal _carImageDal;
        readonly private IFileService _fileService;
        public CarImageManager(ICarImageDal carImageDal, IFileService fileService)
        {
            _carImageDal = carImageDal;
            _fileService = fileService;
        }

        public IResult Add(int carId, IFormFile file)
        {
            IResult result = BusinessRules.Run(CheckImageCount(carId));
            if (result != null)
            {
                return result;
            }
            var imagePath = _fileService.ImageUpload(file, carId, new List<string> { ".jpeg", ".png" });
            if (imagePath.Success)
            {
                CarImage carImage = new CarImage();
                carImage.CarId = carId;
                carImage.ImagePath = imagePath.Message;
                carImage.Date = DateTime.Now;
                _carImageDal.Add(carImage);
            }
            return new SuccessResult(Messages.CarImageAdded);
        }
        

        public IResult Delete(CarImage carImage)
        {
            var result = _fileService.ImageDelete(carImage.ImagePath);
            if (result.Success)
            {
                _carImageDal.Delete(carImage);
                return new SuccessResult(Messages.CarImageDeleted);
            }
            return new ErrorResult("Silinemedi");
        }

        public IDataResult<CarImage> Get(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(p => p.Id == id));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }
       

        public IResult CheckImageCount(int carId)
        {
            var result = _carImageDal.GetAll(p => p.CarId == carId).Count;
            if (result >= 5)
            {
                return new ErrorResult(Messages.CarImageCountError);
            }
            return new SuccessResult();
        }
    }
}
