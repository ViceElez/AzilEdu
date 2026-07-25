namespace AzilEdu.Shared.Models.Employees
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }=string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int EmployeePositionId { get; set; }
        public EmployeePosition EmployeePosition { get; set; } = null!;
        public int EmployeeStatusId { get; set; }
        public EmployeeStatus EmployeeStatus { get; set; } = null!;
    }
}
