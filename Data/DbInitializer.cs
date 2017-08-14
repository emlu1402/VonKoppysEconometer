using Econometer.Data.Models;
using System;
using System.Linq;

namespace Econometer.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any clients.
            if (context.Clients.Any())
            {
                return;   // DB has been seeded
            }

            var clients = new Client[]
            {
                new Client{Orgnumber="12345612", Name="Tieto Enator", Email="info@tieto.se", Phone="08-4567891", Address="Kungsgatan 32", Zip="111 12 Stockholm", Poc="Pelle"},
                new Client{Orgnumber="12345613", Name="Webhallen", Email="info@webhallen.se", Phone="08-123568", Address="Sveavägen 24", Zip="110 11 Stockholm", Poc="Per Gunnarsson"},
                new Client{Orgnumber="12345614", Name="Persons Livs", Email="klagstorp@ica.se", Phone="0410-21782", Address="Byavägen 18", Zip="217 79 Klagstorp", Poc="Kajsa Jönsson"}
            };
            foreach (Client client in clients)
            {
                context.Clients.Add(client);
            }
            context.SaveChanges();
        }
    }
}