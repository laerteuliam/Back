namespace EntityFrameworkConfig.Migrations
{
    using ProjetoContext.DomainModels;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityFrameworkConfig.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntityFrameworkConfig.AppDbContext context)
        {
            SeedProdutos(context);
            SeedUsuarios(context);
        }

        private static void SeedUsuarios(AppDbContext context)
        {
            if (context.Usuarios.FirstOrDefault() != null) return;

            context.Usuarios.Add(new Usuario()
            {
                Login = "admin",
                Senha = "1234"
            });
        }

        private static void SeedProdutos(AppDbContext context)
        {
            if (context.Produtos.FirstOrDefault() != null) return;

            for (int i = 0; i <= 10; i++)
            {
                context.Produtos.Add(new Produto()
                {
                    Nome = "Produto - " + i,
                    Preco = 10 + i
                });
            }
        }
    }
}

