using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfFifteen.UserInterface.Mappers.Abstract
{
    public interface IMapFrom<out TModel, in TViewModel>
    {
        TModel MapFrom(TViewModel viewModel);
    }
}
