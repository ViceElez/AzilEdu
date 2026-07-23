namespace AzilEdu.Shared.DTOs.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int EmployeePositionId { get; set; }
        public string PositionName { get; set; } = string.Empty;
        public int EmployeeStatusId { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }
}