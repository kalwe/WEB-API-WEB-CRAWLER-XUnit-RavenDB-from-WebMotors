using System.Collections.Generic;
using System.Threading.Tasks;
using VeiculosWebApi.Interfaces.Repositories;
using VeiculosWebApi.Interfaces.Services;
using VeiculosWebApi.Models;

namespace VeiculosWebApi.Services
{
    public class CategoriaService : ServiceBase<Categoria>, ICategoriaService
    {
        private Categoria Categoria;

        public CategoriaService(IRepositoryBase<Categoria> repository)
            : base(repository)
        {
            Categoria = new Categoria();
        }

        public async Task InverteActiveStatus(string id)
        {
            await SwitchInactiveStatus("categorias/"+id);
        }

        public async Task<IEnumerable<Categoria>> Ativas()
        {
            return Categoria.Ativas(await ListAllAsync());
        }

        public async Task<Categoria> PorNome(string nome)
        {
            return Categoria.PorNome(await ListAllAsync(), nome);
        }
    }
}