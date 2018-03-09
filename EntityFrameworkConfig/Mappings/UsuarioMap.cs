using ProjetoContext.DomainModels;
using System.Data.Entity.ModelConfiguration;

namespace EntityFrameworkConfig.Mappings
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");
            HasKey(x => x.Id);

            Property(x => x.Login).HasMaxLength(100);
            Property(x => x.Senha).HasMaxLength(100);
        }
    }
}
