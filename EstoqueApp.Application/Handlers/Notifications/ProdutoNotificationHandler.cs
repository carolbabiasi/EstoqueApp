using EstoqueApp.Application.Interfaces.Persistences;
using EstoqueApp.Application.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Application.Handlers.Notifications
{
    public class ProdutoNotificationHandler
        : INotificationHandler<ProdutoNotification>
    {
        private readonly IProdutoPersinteces? _produtoPersistence;

        public ProdutoNotificationHandler(IProdutoPersinteces? produtoPersistence)
        {
            _produtoPersistence = produtoPersistence;
        }

        public Task Handle(ProdutoNotification notification, CancellationToken cancellationToken)
        {
            switch (notification.Action)
            {
                case ActionNotification.Create:
                    _produtoPersistence.Add(notification.Produto);
                    break;

                case ActionNotification.Update:
                    _produtoPersistence.Update(notification.Produto);
                    break;

                case ActionNotification.Delete:
                    _produtoPersistence.Delete(notification.Produto);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}


