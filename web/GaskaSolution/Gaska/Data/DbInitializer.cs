//using Gaska.Data.DbContexts;
//using Gaska.Data.Models;
//using System;
//using System.Linq;

//namespace Gaska.Data
//{
//    public static class DbInitializer
//    {
//        public static void Initialize(GaskaDbContext context)
//        {
//            context.Database.EnsureCreated();

//            // Look for any students.
//            if (context.Cars.Any())
//            {
//                return;   // DB has been seeded
//            }

           

//            var oilsFiltersLogs = new ServiceBookLog[]
//            {
//            new ServiceBookLog{OilsFiltersLogId=1050, Oil="SuperOil", ChangeDate = DateTime.Now.AddMonths(-1), ExpectedNextChangeDate = DateTime.Now.AddMonths(6), Mileage = 95000},
//            new ServiceBookLog{OilsFiltersLogId=1051, Oil="GazpromOil", ChangeDate = DateTime.Now.AddMonths(-16), ExpectedNextChangeDate = DateTime.Now.AddMonths(-12), Mileage = 70000},
//            new ServiceBookLog{OilsFiltersLogId=1045, Oil="NormalOil", ChangeDate = DateTime.Now.AddMonths(-4), ExpectedNextChangeDate = DateTime.Now.AddMonths(2), Mileage = 245000}
//            };
//            foreach (ServiceBookLog oilsFiltersLog in oilsFiltersLogs)
//            {
//                context.ServiceBookLogs.Add(oilsFiltersLog);
//            }
//            context.SaveChanges();

//            var serviceBooks = new ServiceBook[]
//            {
//            new ServiceBook{CarId=19, ServiceBookLogId=1050},
//            new ServiceBook{CarId=19, ServiceBookLogId=1051},
//            new ServiceBook{CarId=20, ServiceBookLogId=1045}
//            };
//            foreach (ServiceBook serviceBook in serviceBooks)
//            {
//                context.ServiceBooks.Add(serviceBook);
//            }
//            context.SaveChanges();
//        }
//    }
//}
