namespace AzilEdu.Shared.Models.Donors
{
    public class DonorStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Donors> Donors { get; set; } = new List<Donors>();
    }
}
