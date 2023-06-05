using EstoqueApp.Application.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Application.Handlers.Notifications
{
    internal class ProdutoNotificationHandler : INotificationHandler<ProdutoNotification>
    {
        public Task Handle(ProdutoNotification notification, CancellationToken cancellationToken)
        {
            //TODO Enviar para o MongoDB
            throw new NotImplementedException();
        }
    }
}
