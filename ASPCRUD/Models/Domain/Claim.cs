namespace ASPCRUD.Models.Domain
{
    public class Claim
    {
        public Guid ClaimId { get; set; }
        public string Description { get; set; }

        public string Status { get; set; }

        public string Date { get; set; }
        
        public string Vehicle_Id { get; set; }
    }

}
