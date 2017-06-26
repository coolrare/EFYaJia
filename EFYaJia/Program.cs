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
                db.Database.Log = (log) => { Console.WriteLine(log); };

                var data = db.Course
                    .Where(p => p.Title.Contains("Git"))
                    .ToList();

                foreach (var item in data)
                {
                    Console.WriteLine(item.Title + "\t" + item.Department.Name);
                }
            }

        }
    }
}
