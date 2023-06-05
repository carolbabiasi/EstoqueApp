using AutoMapper;
using EstoqueApp.Application.Models.Commands;
using EstoqueApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Application.Profiles
{
    public class CommandToModelProfile : Profile
    {
        public CommandToModelProfile()
        {
            CreateMap<EstoqueCreateCommand, Estoque>()
                .AfterMap((command, model) =>
                {
                    model.Id = Guid.NewGuid();
                });

            CreateMap<ProdutoCreateCommand, Produto>()
                .AfterMap((command, model) =>
                {
                    model.Id = Guid.NewGuid();
                });
        }
    }
}
