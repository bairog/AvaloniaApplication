
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

namespace AvaloniaApplication.DAL
{
    static public class StorageSystem
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        static StorageSystem()
        {
            try
            {
                using (var context = new BlankDbContext())
                {
                    var migrations = context.Database.GetPendingMigrations().ToList();
                    if (migrations.Any())
                    {
                        var migrator = context.Database.GetService<IMigrator>();
                        foreach (var migration in migrations)
                            migrator.Migrate(migration);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("При создании BlankDbContext произошла ошибка", ex);
            }
        }


        public static List<PlaneFlight> GetAllPlaneFlights()
        {
            List<PlaneFlight> flights;

            using (var context = new BlankDbContext())
            {
                flights = context.Flights.Include(fl => fl.PlaneBoard).AsSplitQuery().ToList();
            }

            return flights;
        }
    }
}