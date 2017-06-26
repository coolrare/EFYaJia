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
            Console.WriteLine("Started.");

            using (var db = new ContosoUniversityEntities())
            {

                QueryData(db);

                db.Course.Add(new Course()
                {
                    Title = "Hello 1",
                    Credits = 5,
                    Department = db.Department.Find(5)
                });

                db.Database.Log = (log) => { Console.WriteLine(log); };

                db.SaveChanges();

                db.Database.Log = null;

                QueryData(db);


            }

        }

        private static void QueryData(ContosoUniversityEntities db)
        {
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
