using System;

namespace ReplaceConditionalStatements
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeAdministrative employeeAdministrative = new() { Name = "Diana" };
            EmployeeManager employeeManager = new() { Name = "Arthur" };
            EmployeeGeneral employeeGeneral = new() { Name = "Bruce" };

            Payments payments = new();

            Console.WriteLine("Administrative: {0} Pay: {1}",
                employeeAdministrative.Name, payments.Pay(employeeAdministrative));

            Console.WriteLine("Manager: {0} Pay: {1}",
                employeeManager.Name, payments.Pay(employeeManager));

            Console.WriteLine("General: {0} Pay {1}",
                employeeGeneral.Name, payments.Pay(employeeGeneral));

            Console.ReadLine();
        }
    }

    public abstract class Employee
    {
        public string Name { get; set; }
        public EmployeeType Type { get; protected set; }
    }
    public class EmployeeAdministrative : Employee
    {
        public EmployeeAdministrative()
        {
            Type = EmployeeType.Administrative;
        }
    }
    public class EmployeeManager : Employee
    {
        public EmployeeManager()
        {
            Type = EmployeeType.Manager;
        }
    }
    public class EmployeeGeneral : Employee
    {
        public EmployeeGeneral()
        {
            Type = EmployeeType.General;
        }
    }
    public class Payments
    {
        public double Pay(EmployeeAdministrative employee) => 100;
        public double Pay(EmployeeManager employee) => -1000;
        public double Pay(EmployeeGeneral employee) => 200;
    }

    public enum EmployeeType
    {
        Manager, Administrative, General
    }
}
