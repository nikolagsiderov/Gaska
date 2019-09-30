//using Gaska.Data.DbContexts;
//using Gaska.Data.Models;
//using System;
//using System.Linq;

//namespace Gaska.Data
//{
//    public static class DbInitializer
//    {
//        public static void Initialize(DataAccessContext context)
//        {
//            context.Database.EnsureCreated();

//            if (context.Data.Any())
//            {
//                return;   // DB has been seeded
//            }



//            var data = new Data.Models.Data[]
//            {
//            new Data.Models.Data{DataString="Масло", ModuleId = ""},
//            };
//            foreach (Data.Models.Data singleData in data)
//            {
//                context.Data.Add(singleData);
//            }
//            context.SaveChanges();
//        }
//    }
//}
