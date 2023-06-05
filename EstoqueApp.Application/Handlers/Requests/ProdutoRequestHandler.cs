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
    public class ProdutoRequestHandler :
        IRequestHandler<ProdutoCreateCommand, ProdutoQuery>,
        IRequestHandler<ProdutoUpdateCommand, ProdutoQuery>,
        IRequestHandler<ProdutoDeleteCommand, ProdutoQuery>
    {
        private readonly IMediator? _mediator;
        private readonly IMapper? _mapper;
        private readonly IProdutoDomainService? _produtoDomainService;

        public ProdutoRequestHandler(IMediator? mediator, IMapper? mapper, IProdutoDomainService? produtoDomainService)
        {
            _mediator = mediator;
            _mapper = mapper;
            _produtoDomainService = produtoDomainService;
        }

        public async Task<ProdutoQuery> Handle(ProdutoCreateCommand request, CancellationToken cancellationToken)
        {
            var produto = _mapper.Map<Produto>(request);
            _produtoDomainService.Add(produto);

            var produtoQuery = _mapper.Map<ProdutoQuery>(produto);
            await _mediator.Publish(
                    new ProdutoNotification
                    {
                        Action = ActionNotification.Create,
                        Produto = produtoQuery
                    }
                );

            return produtoQuery;
        }

        public async Task<ProdutoQuery> Handle(ProdutoUpdateCommand request, CancellationToken cancellationToken)
        {
            var produto = _produtoDomainService.GetById(request.Id.Value);
            produto.Nome = request.Nome;
            produto.Preco = request.Preco;
            produto.Quantidade = request.Quantidade;
            produto.EstoqueId = request.EstoqueId;

            _produtoDomainService.Update(produto);

            var produtoQuery = _mapper.Map<ProdutoQuery>(produto);
            await _mediator.Publish(
                    new ProdutoNotification
                    {
                        Action = ActionNotification.Update,
                        Produto = produtoQuery
                    }
                );

            return produtoQuery;
        }

        public async Task<ProdutoQuery> Handle(ProdutoDeleteCommand request, CancellationToken cancellationToken)
        {
            var produto = _produtoDomainService.GetById(request.Id.Value);
            _produtoDomainService.Delete(produto);

            var produtoQuery = _mapper.Map<ProdutoQuery>(produto);
            await _mediator.Publish(
                    new ProdutoNotification
                    {
                        Action = ActionNotification.Delete,
                        Produto = produtoQuery
                    }
                );

            return produtoQuery;
        }
    }
}



