using Checkers_Game.Command;
using Checkers_Game.Model;
using Checkers_Game.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers_Game.ViewModel
{
    public class CellViewModel : BaseViewModel
    {
        GameLogic bl;
        public CellViewModel(Cell currentCell, GameLogic bl)
        {
            SimpleCell = currentCell;
            this.bl = bl;
        }

        //am adus celula din Model in VM
        public Cell SimpleCell { get; set; }

        private ICommand clickCommand;
        public ICommand ClickCommand
        {
            get
            {
                if (clickCommand == null)
                {
                    clickCommand = new RelayCommand<Cell>(bl.ClickAction, bl.IsClickable);
                }
                return clickCommand;
            }
        }

    }
}
