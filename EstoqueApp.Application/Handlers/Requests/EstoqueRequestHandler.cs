using AutoMapper;
using EstoqueApp.Application.Handlers.Notifications;
using EstoqueApp.Application.Models.Commands;
using EstoqueApp.Application.Models.Queries;
using EstoqueApp.Application.Notifications;
using EstoqueApp.Domain.Interfaces.Services;
using EstoqueApp.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Application.Handlers.Requests
{
    public class EstoqueRequestHandler :
        IRequestHandler<EstoqueCreateCommand, EstoqueQuery>,
        IRequestHandler<EstoqueUpdateCommand, EstoqueQuery>,
        IRequestHandler<EstoqueDeleteCommand, EstoqueQuery>
    {
        private readonly IMediator? _mediator;
        private readonly IMapper? _mapper;
        private readonly IEstoqueDomainService? _estoqueDomainService;

        public EstoqueRequestHandler(IMediator? mediator, IMapper? mapper, IEstoqueDomainService? estoqueDomainService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _estoqueDomainService = estoqueDomainService;
        }

        public async Task<EstoqueQuery> Handle(EstoqueCreateCommand request, CancellationToken cancellationToken)
        {
            var estoque = _mapper.Map<Estoque>(request);
            _estoqueDomainService.Add(estoque);

            var estoqueQuery = _mapper.Map<EstoqueQuery>(estoque);
            await _mediator.Publish(
                    new EstoqueNotification
                    {
                        Action = ActionNotification.Create,
                        Estoque = estoqueQuery
                    }
                );

            return estoqueQuery;
        }

        public async Task<EstoqueQuery> Handle(EstoqueUpdateCommand request, CancellationToken cancellationToken)
        {
            var estoque = _estoqueDomainService.GetById(request.Id.Value);
            estoque.Nome = request.Nome;
            estoque.Descricao = request.Descricao;

            _estoqueDomainService.Update(estoque);

            var estoqueQuery = _mapper.Map<EstoqueQuery>(estoque);
            await _mediator.Publish(
                    new EstoqueNotification
                    {
                        Action = ActionNotification.Update,
                        Estoque = estoqueQuery
                    }
                );

            return estoqueQuery;
        }

        public async Task<EstoqueQuery> Handle(EstoqueDeleteCommand request, CancellationToken cancellationToken)
        {
            var estoque = _estoqueDomainService.GetById(request.Id.Value);
            _estoqueDomainService.Delete(estoque);

            var estoqueQuery = _mapper.Map<EstoqueQuery>(estoque);
            await _mediator.Publish(
                    new EstoqueNotification
                    {
                        Action = ActionNotification.Delete,
                        Estoque = estoqueQuery
                    }
                );

            return estoqueQuery;
        }
    }
}



