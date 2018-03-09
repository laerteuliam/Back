using ProjetoContext.DomainModels;
using System.Data.Entity.ModelConfiguration;

namespace EntityFrameworkConfig.Mappings
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            ToTable("Produto");
            HasKey(x => x.Id);

            Property(x => x.Nome).HasMaxLength(100);
        }
    }
}
