using ProjetoContext.Application.Produto;
using System.Collections.Generic;
using System.Web.Http;

namespace Api.Controllers
{
    public class ProdutoController : ApiController
    {
        ProdutoService _produtoService;
        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }
        
        [Authorize]
        public IEnumerable<ProdutoDto> Get()
        {
            return _produtoService.GetAll();
        }
        
    }
}
