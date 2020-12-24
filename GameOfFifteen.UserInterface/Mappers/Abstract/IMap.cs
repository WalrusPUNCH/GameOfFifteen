using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.UserInterface.Mappers.Abstract
{
    public interface IMap<TEntity, TModel> : IMapTo<TEntity, TModel>, IMapFrom<TEntity, TModel>
    {

    }
}
