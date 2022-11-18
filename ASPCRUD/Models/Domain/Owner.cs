namespace ASPCRUD.Models.Domain
{
    public class Owner
    {
        public Guid OwnerId { get; set; }
        public string FirstName { get; set; }  
        
        public string LastName { get; set; }

        public string DriverLicense { get; set; }

    }
}
