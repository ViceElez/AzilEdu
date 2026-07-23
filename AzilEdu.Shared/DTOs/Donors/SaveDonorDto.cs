namespace AzilEdu.Shared.DTOs.Donors
{
    public class SaveDonorDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string OrganizationName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public int DonorTypeId { get; set; }
        public int DonorStatusId { get; set; }
    }
}