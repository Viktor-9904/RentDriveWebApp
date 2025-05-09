namespace RentDrive.Common
{
    public static class EntityValidationConstants
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
    }
}
