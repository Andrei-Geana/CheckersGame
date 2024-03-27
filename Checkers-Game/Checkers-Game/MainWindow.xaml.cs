using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Checkers_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var board = this.FindName("Board") as Grid;

            int n = 8;
            for (int i = 0; i < n; i++)
            {
                ColumnDefinition column = new ColumnDefinition();
                board.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < n; i++)
            {
                RowDefinition row = new RowDefinition();
                board.RowDefinitions.Add(row);
            }
            for(int i=0; i < n; i++)
            {
                for(int j=0; j < n; j++)
                {
                    Button button = new Button
                    {
                        Content = '(' + i.ToString() + ',' + j.ToString() +')'
                    };
                    button.Margin = new Thickness(2);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    board.Children.Add(button);
                }
            }
            
        }
    }
}
