using System;
using System.Collections.Generic;
using System.Text;

namespace AzilEdu.Shared.Models.Volunteers
{
    public class Volunteer
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
        public VolunteerStatus? VolunteerStatus { get; set; }
    }
}
