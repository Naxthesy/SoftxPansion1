
using System.Collections.Generic;
namespace SoftxPansion1.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Employee> Employees = new List<Employee>();
    }
}
