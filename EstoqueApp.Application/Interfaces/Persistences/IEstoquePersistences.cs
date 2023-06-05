using EstoqueApp.Application.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueApp.Application.Interfaces.Persistences
{
    public interface IEstoquePersistences : IBasePersistences<EstoqueQuery, Guid>
    {

    }
}
