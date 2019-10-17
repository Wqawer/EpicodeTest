using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = fillEmployes();
            var structList = FillEmployeesStructures(list);
            foreach (var element in structList)
            {
                Console.WriteLine("|{0}|{1}|{2}|{3}|",element.id,element.EmployeeId,element.level,element.SeperiorId);
            }
            var level = GetSupeieriorRowOfEmployee(7,1 , structList);
        }
        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ? Superiorid { get; set; }
            public virtual Employee Superior { get; set; }
        }
        public class EmployeesStructure
        {
            public int id { get; }
            public int level { get; set; }
            public int? SeperiorId { get; set; }
            public int EmployeeId { get; set; }
            private static int PrimaryKey = 0;
            public EmployeesStructure(){
                id = ++PrimaryKey;
            }
        }
        public static List<EmployeesStructure> FillEmployeesStructures(List<Employee> employees)
        {
            List<Employee> employeesList = new List<Employee>(employees); 
            List<EmployeesStructure> employeesStructures = new List<EmployeesStructure>();
            for(int i = (employees.Count - 1);i >= 0;i--)
            {
                if (!employees[i].Superiorid.HasValue)
                {
                    var employerStructure = new EmployeesStructure()
                    {
                        EmployeeId = employees[i].Id,
                        level = 0
                    };
                    employeesStructures.Add(employerStructure);
                    employees.RemoveAt(i);
                }
            }
            while (employees.Count > 0)
            {
                for(int i = (employees.Count - 1); i >= 0; i--)
                {
                    foreach(EmployeesStructure employeesStructure in employeesStructures)
                    {
                        if(employees[i].Superiorid == employeesStructure.EmployeeId)
                        {
                            bool all = false;
                            int superiorId = employeesStructure.EmployeeId;
                            int level = 1;
                            while (!all)
                            {
                                var parent = employeesList.Find(x => x.Id == superiorId);
                                var employerStructure = new EmployeesStructure()
                                {
                                    EmployeeId = employees[i].Id,
                                    level = level++,
                                    SeperiorId = parent.Id
                                };
                                employeesStructures.Add(employerStructure);
                                if (parent.Superiorid.HasValue)
                                    superiorId = parent.Superiorid.Value;
                                else
                                    all = true;
                            }
                            employees.RemoveAt(i);
                            break;
                            
                        }
                    }
                }
            }
            return employeesStructures;
        }
        public static List<Employee> fillEmployes()
        {
            var list = new List<Employee>();
            list.Add(new Employee() { Id = 1, Name = "Jon" });
            list.Add(new Employee() { Id = 2, Name = "Jill", Superiorid = 1 });
            list.Add(new Employee() { Id = 3, Name = "Ted", Superiorid = 1 });
            list.Add(new Employee() { Id = 4, Name = "Penny", Superiorid = 2 });
            list.Add(new Employee() { Id = 5, Name = "Carol", Superiorid = 2 });
            list.Add(new Employee() { Id = 6, Name = "Marry", Superiorid = 3 });
            list.Add(new Employee() { Id = 7, Name = "Mark", Superiorid = 3 });
            list.Add(new Employee() { Id = 8, Name = "Jill", Superiorid = 7 });
            list.Add(new Employee() { Id = 9, Name = "Jack" });
            list.Add(new Employee() { Id = 10, Name = "Jill", Superiorid = 11 });
            list.Add(new Employee() { Id = 11, Name = "Ted", Superiorid = 13 });
            list.Add(new Employee() { Id = 12, Name = "Penny", Superiorid = 15 });
            list.Add(new Employee() { Id = 13, Name = "Carol", Superiorid = 9 });
            list.Add(new Employee() { Id = 14, Name = "Marry", Superiorid = 16 });
            list.Add(new Employee() { Id = 15, Name = "Mark", Superiorid = 16 });
            list.Add(new Employee() { Id = 16, Name = "Jill", Superiorid = 9 });
            return list;
        }
        public static int? GetSupeieriorRowOfEmployee(int employeeId, int superiorId, List<EmployeesStructure> employeesStructures)
        {
            var structur = employeesStructures.Find(x => 
            x.SeperiorId == superiorId && 
            x.EmployeeId == employeeId);
            if (structur != null)
                return structur.level;
            else
                return null;
        }

    }
}
