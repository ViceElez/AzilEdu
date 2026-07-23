namespace AzilEdu.Shared.DTOs.Employees
{
    public class SaveEmployeeDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int EmployeePositionId { get; set; }
        public int EmployeeStatusId { get; set; }
    }
}