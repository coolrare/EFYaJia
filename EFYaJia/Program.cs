using EFYaJia.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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

                db.Database.Log = (log) => { Console.WriteLine(log); };
                //QueryData(db);
                db.Database.Log = null;

                //AddNewRecord(db);
                //UpdateData(db);
                //DeleteData(db);
                //多對多關聯新增(db);

                var c = db.Course.Find(1);

                Console.WriteLine(db.Entry(c).State);

                c.Credits++;

                Console.WriteLine(db.Entry(c).State);
                var ce = db.Entry(c);
                Console.WriteLine("修改前: " + ce.OriginalValues.GetValue<int>("Credits"));
                Console.WriteLine("修改後: " + ce.CurrentValues.GetValue<int>("Credits"));

                var cc = new Course()
                {
                    Title = "Hello 2",
                    Department = db.Department.Find(5)
                };
                db.Course.Add(cc);

                var ce2 = db.Entry(cc);

                if (ce2.State == System.Data.Entity.EntityState.Added)
                {
                    ce2.Entity.CreatedOn = DateTime.Now;
                }

                db.SaveChanges();

                Console.WriteLine(db.Entry(c).State);


                //Console.WriteLine(DateTime.Now + "\t" + "SaveChanges Started.");
                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (DbEntityValidationException ex)
                //{
                //    throw ex;
                //}


                //Console.WriteLine(DateTime.Now + "\t" + "Query Started.");
                //QueryData(db);
            }

            Console.WriteLine(DateTime.Now + "\t" + "Ended.");
        }

        private static void 多對多關聯新增(ContosoUniversityEntities db)
        {
            var c = db.Course.Find(1);

            //c.Person.Add(db.Person.Find(3));
            c.Person.Add(new Person()
            {
                FirstName = "AA",
                LastName = "BB",
                Discriminator = "123"
            });
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
            db.Configuration.ProxyCreationEnabled = false;

            var depts = db.Department.Include("Course").ToList();

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
