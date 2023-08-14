namespace WebApplication2.Models
{
    public class Employee
    {
        public int? Id { get; set; }
        public string? lastName { get; set; }
        public string? firstName { get; set; }
        public string? title { get; set; }
        public string? titleOfCourtesy { get; set; }
        public string? birthDate { get; set; }
        public string? hireDate { get; set; }
        public string? notes { get; set; }
        public string? reportsTo { get; set; }


        public Employee()
        {


        }

    }
}
