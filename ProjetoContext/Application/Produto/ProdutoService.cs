using SharedKernel.Helpers.Application;
using SharedKernel.Helpers.Repository;

namespace ProjetoContext.Application.Produto
{
    public class ProdutoService : ApplicationBase<DomainModels.Produto, ProdutoDto>
    {
        public ProdutoService(IRepository repository) : base(repository)
        {
        }
    }
}
