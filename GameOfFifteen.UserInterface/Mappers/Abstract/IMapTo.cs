using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.UserInterface.Mappers.Abstract
{
    public interface IMapTo<in TModel, out TViewModel>
    {
        TViewModel MapTo(TModel model);
    }
}
