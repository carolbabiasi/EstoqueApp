using EstoqueApp.Application.Interfaces.Persistences;
using EstoqueApp.Application.Interfaces.Services;
using EstoqueApp.Application.Models.Commands;
using EstoqueApp.Application.Models.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Application.Services
{
    public class EstoqueAppService : IEstoqueAppService
    {
        private readonly IMediator? _mediator;
        private readonly IEstoquePersistences _estoquePersistence;

        public EstoqueAppService(IMediator? mediator, IEstoquePersistences estoquePersistence)
        {
            _mediator = mediator;
            _estoquePersistence = estoquePersistence;
        }

        public async Task<EstoqueQuery> Create(EstoqueCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<EstoqueQuery> Update(EstoqueUpdateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<EstoqueQuery> Delete(EstoqueDeleteCommand command)
        {
            return await _mediator.Send(command);
        }

        public List<EstoqueQuery> GetAll()
        {
            return _estoquePersistence.GetAll();
        }

        public EstoqueQuery GetById(Guid? id)
        {
            return _estoquePersistence.GetById(id.Value);
        }


    }
}
