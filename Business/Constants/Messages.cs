using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarAdded = "Car Added";
        public static string CarUpdated = "Car Updated";
        public static string CarDeleted = "Car Deleted";

        public static string ColorAdded = "Color Added";
        public static string ColorUpdated = "Color Updated";
        public static string ColorDeleted = "Color Deleted";

        public static string BrandAdded = "Brand Added";
        public static string BrandUpdated = "Brand Updated";
        public static string BrandDeleted = "Brand Deleted";

        public static string CarImageAdded = "CarImage Added";
        public static string CarImageUpdated = "CarImage Updated";
        public static string CarImageDeleted = "CarImage Deleted";

        public static string CarImageCountError = "can have up to 5 images";

        public static string AuthorizationDenied="Yetkiniz Yok!";
    }
}
