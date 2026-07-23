using System;
using System.Collections.Generic;
using System.Text;

namespace AzilEdu.Shared.DTOs.Volunteers
{
    public class SaveVolunteerDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string Email { get; set; }= string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Skills { get; set; } = string.Empty;
        public string AvailableFrom { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int VolunteerStatusId { get; set; }
    }
}
