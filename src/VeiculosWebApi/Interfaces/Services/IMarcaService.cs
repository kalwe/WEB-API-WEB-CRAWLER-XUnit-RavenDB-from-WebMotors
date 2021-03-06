﻿using System.Collections.Generic;
using System.Threading.Tasks;
using VeiculosWebApi.Interfaces.Repositories;
using VeiculosWebApi.Models;

namespace VeiculosWebApi.Interfaces.Services
{
    public interface IMarcaService : IServiceBase<Marca>
    {
        Task SetInactiveStatus(string id);

        Task SetActiveStatus(string id);

        Task<IEnumerable<Marca>> Ativas();

        Task<IEnumerable<Marca>> PorCategoria(string category);

        Task<Marca> PorCategoriaENome(string categoria, string nome);
    }
}
