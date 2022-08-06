using Microsoft.EntityFrameworkCore;

namespace TesteBancoDados
{
    public class MyDBContext : DbContext, IDisposable
    {
        public DbSet<ClienteModel> Clientes { get; set; }

        //private string BasePath { get; set; }

        public MyDBContext(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Filename={PathDB()}");

        /// <summary>
        /// https://devblogs.microsoft.com/xamarin/building-android-apps-entity-framework/
        /// </summary>
        public async void CreateDataBaseEF() => await Database.EnsureCreatedAsync();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteModel>()
                .Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            modelBuilder.Entity<ClienteModel>()
                .Property(b => b.Nome);
            modelBuilder.Entity<ClienteModel>()
                .Property(b => b.Funcao);
            modelBuilder.Entity<ClienteModel>()
                .Property(b => b.CPF);

            base.OnModelCreating(modelBuilder);
        }


        private string PathDB()
        {
            var dbFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fileName = "teste.db";
            var dbFullPath = Path.Combine(dbFolder, fileName);
            Console.WriteLine(dbFullPath);
            return dbFullPath;
        }

    }
}
