using AutoMapper;
using SharedKernel.Helpers.Application;
using SharedKernel.Helpers.Repository;
using System.Linq;

namespace ProjetoContext.Application.Usuario
{
    public class UsuarioService : ApplicationBase<DomainModels.Usuario, UsuarioDto>
    {
        public UsuarioService(IRepository repository) : base(repository)
        {
        }

        public UsuarioDto GetBy(string username, string password)
        {
            var entidade =_repository
                .AsQueryable<DomainModels.Usuario>()
                .FirstOrDefault(x => x.Login == username && x.Senha == password);

            return Mapper.Map<UsuarioDto>(entidade);
        }
    }
}
