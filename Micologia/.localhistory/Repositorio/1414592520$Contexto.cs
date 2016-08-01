using System;
using System.Collections.Generic;
using System.Data.Entity;
using Micologia.Modelo;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;

namespace Micologia.Repositorio
{
    internal sealed class Configuration : DbMigrationsConfiguration<Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;//have also tried true
        }
    }

    public class Contexto : DbContext
    {
        public DbSet<MICOLOGIA_Cultura> Cultura { get; set; }
        public DbSet<MICOLOGIA_Exame> Exame { get; set; }
        public DbSet<MICOLOGIA_Procedencia> Procedencia { get; set; }
        public DbSet<MICOLOGIA_Resultado> Resultado { get; set; }
        public DbSet<MICOLOGIA_Usuario> Usuario { get; set; }
        public DbSet<MICOLOGIA_ExameResultado> ExameResultado { get; set; }
        public DbSet<MICOLOGIA_Seguranca> Seguranca { get; set; }
        public DbSet<MICOLOGIA_Tela> Tela { get; set; }
        public DbSet<MICOLOGIA_Paciente> Paciente { get; set; }
        public DbSet<vwMICOLOGIA_PEDIDOEXAME> vwMICOLOGIA_PEDIDOEXAME { get; set; }

        public Contexto()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<Contexto, Configuration>());



          

            this.Configuration.LazyLoadingEnabled = true;
            Database.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["MICOLOGIA"].ConnectionString;
            ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = 300;
        }       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var initializer = new MigrateDatabaseToLatestVersion<Contexto, Configuration>();
            Database.SetInitializer(initializer);
            Database.Initialize(true);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //DECLARAÇÃO DE PKs  
            modelBuilder.Entity<MICOLOGIA_Cultura>().HasKey(r => r.nIDCultura);
            modelBuilder.Entity<MICOLOGIA_Resultado>().HasKey(r => r.nIDResultado);
            modelBuilder.Entity<MICOLOGIA_Usuario>().HasKey(r => r.nIDLogin);
            modelBuilder.Entity<MICOLOGIA_Exame>().HasKey(r => r.nIDExame);
            modelBuilder.Entity<MICOLOGIA_Procedencia>().HasKey(r => r.nIDProcedencia);
            modelBuilder.Entity<MICOLOGIA_ExameResultado>().HasKey(r => r.nIDExameResultado);
            modelBuilder.Entity<MICOLOGIA_Seguranca>().HasKey(r => r.nIDSeguranca);
            modelBuilder.Entity<MICOLOGIA_Tela>().HasKey(r => r.nIDTela);
            modelBuilder.Entity<MICOLOGIA_Paciente>().HasKey(r => r.nIDPaciente);
            modelBuilder.Entity<vwMICOLOGIA_PEDIDOEXAME>().HasKey(r => r.ID);

            //modelBuilder.Entity<MICOLOGIA_Exame>().Property(p => p.nNumeroExame).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
