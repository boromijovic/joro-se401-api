using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Data
{
    public class DbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            context.Database.EnsureCreated();

            // Look for any pacijenti.
            if (context.Patients.Any())
            {
                return;   // DB has been seeded
            }

            //var patients = new Patient[]
            //{
            //new Patient{FirstName="Carson",LastName="Alexander",LBO="170799571022222", Phone="0656565664646", Adress="Buljubase Petra Dobrnjca 19", Email="carson@mail.com"},
            //new Patient{FirstName="Meredith",LastName="Alonso",LBO="170799571022222", Phone="0656565664646", Adress="Buljubase Petra Dobrnjca 19", Email="Meredith@mail.com"},
            //new Patient{FirstName="Arturo",LastName="Anand",LBO="170799571022222", Phone="0656565664646", Adress="Buljubase Petra Dobrnjca 19", Email="carson@mail.com"},
            //new Patient{FirstName="Gytis",LastName="Barzdukas",LBO="170799571022222", Phone="0656565664646", Adress="Buljubase Petra Dobrnjca 19", Email="carson@mail.com"},
            //new Patient{FirstName="Yan",LastName="Li",LBO="170799571022222", Phone="0656565664646", Adress="Buljubase Petra Dobrnjca 19", Email="carson@mail.com"},
            //new Patient{FirstName="Peggy",LastName="Justice",LBO="170799571022222", Phone="0656565664646", Adress="Buljubase Petra Dobrnjca 19", Email="carson@mail.com"}
            //};
            //foreach (Patient p in patients)
            //{
            //    context.Patients.Add(p);
            //}
            context.SaveChanges();
        }
    }
}
