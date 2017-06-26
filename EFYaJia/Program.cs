using EFYaJia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFYaJia
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ContosoUniversityEntities())
            {
                //db.Database.Log = (log) => { Console.WriteLine(log); };

                var depts = db.Department.ToList();

                foreach (var dept in depts)
                {
                    Console.WriteLine("部門: " + dept.Name);
                    foreach (var course in dept.Course)
                    {
                        Console.WriteLine("\t" + course.Title + "\t" + course.CreatedOn);
                    }
                }
            }

        }
    }
}
