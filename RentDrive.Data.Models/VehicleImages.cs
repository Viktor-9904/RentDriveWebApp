namespace RentDrive.Data.Models
{
    public class VehicleImages
    {
        public Guid Id { get; set; }
        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;
        public string ImageURL { get; set; } = null!;
    }
}
