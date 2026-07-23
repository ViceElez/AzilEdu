namespace AzilEdu.Shared.Models.Donors
{
    public class Donors
    {
        public int Id { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int DonorTypeId { get; set; }
        public DonorType DonorType { get; set; } = null!;
        public int DonorStatusId { get; set; }
        public DonorStatus DonorStatus { get; set; } = null!;
    }
}
