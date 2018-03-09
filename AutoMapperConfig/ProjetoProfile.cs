using AutoMapper;
using ProjetoContext.Application.Produto;
using ProjetoContext.Application.Usuario;
using ProjetoContext.DomainModels;

namespace AutoMapperConfig
{
    public class ProjetoProfile : Profile
    {
        public ProjetoProfile()
        {
            CreateMap<Produto, ProdutoDto>();
            CreateMap<Usuario, UsuarioDto>();
        }
    }

}
