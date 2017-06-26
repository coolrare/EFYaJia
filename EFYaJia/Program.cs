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
            Console.WriteLine(DateTime.Now + "\t" + "Started.");

            using (var db = new ContosoUniversityEntities())
            {
                Console.WriteLine(DateTime.Now + "\t" + "Query Started.");
                QueryData(db);

                //AddNewRecord(db);
                //UpdateData(db);
                //DeleteData(db);

                db.Database.Log = (log) => { Console.WriteLine(log); };

                Console.WriteLine(DateTime.Now + "\t" + "SaveChanges Started.");
                db.SaveChanges();

                db.Database.Log = null;

                Console.WriteLine(DateTime.Now + "\t" + "Query Started.");
                QueryData(db);
            }

            Console.WriteLine(DateTime.Now + "\t" + "Ended.");
        }

        private static void DeleteData(ContosoUniversityEntities db)
        {
            foreach (var item in db.Course.Where(p => p.CourseID >= 11 && p.CourseID <= 18).ToList())
            {
                //db.Course.Remove(new Course() { CourseID = item.CourseID });
                db.Course.Remove(item);
            }

            db.Database.ExecuteSqlCommand("DELETE FROM dbo.Course WHERE CourseID >= @p0 AND CourseID <= @p1", 11, 18);
        }

        private static void UpdateData(ContosoUniversityEntities db)
        {
            foreach (var item in db.Course.ToList())
            {
                item.Credits += 1;
            }

            var course = db.Course.Find(5);
            course.Credits = 5;
            course.Department = db.Department.Find(2);
        }

        private static void AddNewRecord(ContosoUniversityEntities db)
        {
            db.Course.Add(new Course()
            {
                Title = "Hello 1",
                Department = db.Department.Find(5)
            });
        }

        private static void QueryData(ContosoUniversityEntities db)
        {
            var depts = db.Department.ToList();

            foreach (var dept in depts)
            {
                Console.WriteLine("部門: " + dept.Name);
                foreach (var course in dept.Course)
                {
                    Console.WriteLine("\t" + course.Title);
                }
            }
        }
    }
}
