using ProjetoContext.DomainModels;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace EntityFrameworkConfig
{
    public class AppDbContext : DbContext
    {
        static AppDbContext()
        {
            Database.SetInitializer<AppDbContext>(null);
        }

        public AppDbContext() 
            //: base(@"Data Source = (localdb)\MSSQLLocalDB; Integrated Security = true; Initial Catalog = BancoLaerteUliam")
            //: base(@"Server=localhost\SQLEXPRESS01;Database=BancoLaerteUliam;Trusted_Connection=True;")
            :base(ConfigurationManager.AppSettings["Conexao"].ToString())
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.EnsureTransactionsForFunctionsAndCommands = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigureConventions(modelBuilder);
            ConfigureMappings(modelBuilder);
        }

        private void ConfigureConventions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
        }

        private void ConfigureMappings(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new Mappings.ProdutoMap());
            modelBuilder.Configurations.Add(new Mappings.UsuarioMap());
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
