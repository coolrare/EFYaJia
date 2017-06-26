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
                var data = db.Course.ToList();

                foreach (var item in data)
                {
                    Console.WriteLine(item.Title);
                }
            }

        }
    }
}
