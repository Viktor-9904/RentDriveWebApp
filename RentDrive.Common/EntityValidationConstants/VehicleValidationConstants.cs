namespace RentDrive.Common.Vehicle
{
    public static class VehicleValidationConstants
    {
        public static class Vehicle
        {
            public const int MakeMaxLength = 100;
            public const int ModelMaxLength = 100;
            public const int ColorMaxLength = 50;
            public const int DescriptionMaxLength = 1500;
        }
        public static class VehicleTypeClass
        {
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength = 1500;
        }
        public static class VehicleType
        {
            public const int NameMaxLength = 50;
        }
        public static class VehicleImages
        {
            public const int ImageURLMaxLength = 2083;
            public const string DefaultImageURL = "images/default-image.jpg";
        }
        public static class VehicleTypeProperty
        {
            public const int NameMaxLength = 50;
        }
        public static class VehicleTypePropertyValue
        {
            public const int PropertyValueMaxLength = 100;
        }
    }
}
