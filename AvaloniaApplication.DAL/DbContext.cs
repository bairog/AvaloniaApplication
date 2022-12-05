using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AvaloniaApplication.DAL
{
    /// <summary>
    /// Борт
    /// </summary>
    public class PlaneBoard
    {
        //Идентификатор
        public UInt64 Id { get; set; }
        //Системное имя типа ВС
        public String PlaneType { get; set; }
        //Системное имя борта
        public String PlaneBoardNo { get; set; }
        //Список полётов борта
        public virtual IList<PlaneFlight> Flights { get; set; }

        public override string ToString()
        {
            return $"{PlaneBoardNo} ({PlaneType})";
        }
    }

    /// <summary>
    /// Полёт
    /// </summary>
    public class PlaneFlight
    {
        //Идентификатор
        public UInt64 Id { get; set; }
        //Дата полёта
        public DateTime FlightDate { get; set; }
        //Смена        
        public Int16 FlightSmena { get; set; }
        //Номер вылета
        public Int32 FlightNo { get; set; }
        //Позывной лётчика
        public String PilotName { get; set; }
        //Идентификатор борта
        public UInt64 PlaneBoardId { get; set; }
        //Борт
        public virtual PlaneBoard PlaneBoard { get; set; }

        public override string ToString()
        {
            return $"{PlaneBoard}: {FlightDate.ToShortDateString()}/{FlightSmena}/{FlightNo} {PilotName}";
        }
    }


    public class BlankDbContext : DbContext
    {
        public BlankDbContext()
        { }

        public BlankDbContext(DbContextOptions<BlankDbContext> options)
            : base(options)
        { }


        //static string str = "";
        //флаг, указывающий, что соединение было открыто данным драйвером (возможно "отложенное" открытие соединений, когда
        //, например, происходит enumerate возвращенного драйвером IEnumerable - может быть значительно позже места выхода из функции)
        internal Boolean IsConnectionOpenedByThisDriver = false;

        internal String databasePath { get; set; }

        public DbSet<PlaneBoard> PlaneBoards { get; set; }
        public DbSet<PlaneFlight> Flights { get; set; }

        //настройка ограничений по классам через Code First Fluent API
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlite("data source=Avalonia.db;Pooling=False;");

            // Compiling a query which loads related collections for more than one collection navigation either via 'Include' or through projection 
            //but no 'QuerySplittingBehavior' has been configured. By default Entity Framework will use 'QuerySplittingBehavior.SingleQuery' which 
            //can potentially result in slow query performance. See https://go.microsoft.com/fwlink/?linkid=2134277 for more information. 
            //To identify the query that's triggering this warning call 'ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))'
            optionsBuilder.ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning));

            //Entity Framework Core 5 simple logging example
            //optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Таблицы будут иметь имена точно как у классов, а не во множественном числе имени класса
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //ограничения на уникальность полей
            modelBuilder.Entity<PlaneBoard>().ToTable("PlaneBoard");
            modelBuilder.Entity<PlaneFlight>().ToTable("PlaneFlight");

            modelBuilder.Entity<PlaneBoard>().HasIndex(pb => new { pb.PlaneType, pb.PlaneBoardNo }).IsUnique();

            modelBuilder.Entity<PlaneFlight>().HasIndex(pf => new { pf.PlaneBoardId, pf.FlightDate, pf.FlightSmena, pf.FlightNo }).IsUnique();


            modelBuilder.Entity<PlaneBoard>().HasData(
                new PlaneBoard { Id = 1, PlaneType = "SU-27", PlaneBoardNo = "004" },
                new PlaneBoard { Id = 2, PlaneType = "MIG-29", PlaneBoardNo = "16" });

            modelBuilder.Entity<PlaneFlight>().HasData(
                new PlaneFlight { Id = 1, PlaneBoardId = 1, FlightDate = new DateTime(2010, 1, 12), FlightSmena = 1, FlightNo = 1, PilotName = "Иванов" },
                new PlaneFlight { Id = 2, PlaneBoardId = 1, FlightDate = new DateTime(2010, 1, 12), FlightSmena = 1, FlightNo = 2, PilotName = "Иванов" },
                new PlaneFlight { Id = 3, PlaneBoardId = 1, FlightDate = new DateTime(2017, 6, 16), FlightSmena = 1, FlightNo = 6, PilotName = "Петров" },
                new PlaneFlight { Id = 4, PlaneBoardId = 2, FlightDate = new DateTime(2010, 1, 12), FlightSmena = 1, FlightNo = 1, PilotName = "Сидоров" });
        }
    }
}