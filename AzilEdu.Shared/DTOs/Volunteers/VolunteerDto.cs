namespace AzilEdu.Shared.DTOs.Volunteers
{
    public class VolunteerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string AvailableFrom { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int VolunteerStatusId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
