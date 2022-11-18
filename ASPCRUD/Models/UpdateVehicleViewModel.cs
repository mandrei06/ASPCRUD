namespace ASPCRUD.Models
{
    public class UpdateVehicleViewModel
    {
        public Guid VehicleId { get; set; }
        public string Brand { get; set; }

        public string Vin { get; set; }

        public string Color { get; set; }

        public string Year { get; set; }

        public string Owner_Id { get; set; }
    }
}
