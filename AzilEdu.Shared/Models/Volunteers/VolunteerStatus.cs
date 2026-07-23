using System;
using System.Collections.Generic;
using System.Text;

namespace AzilEdu.Shared.Models.Volunteers
{
    public class VolunteerStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Volunteers> Volunteers { get; set; } = new List<Volunteers>();
    }
}
