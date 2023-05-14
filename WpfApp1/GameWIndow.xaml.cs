using Microsoft.VisualBasic;
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
using System.Windows.Shapes;
using WpfApp1.Models;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для GameWIndow.xaml
    /// </summary>
    public partial class GameWIndow : Window
    {
        Button[,] buttons;
        string Hod;
        public GameWIndow()
        {
            InitializeComponent();
            igrok1.Content = Models.Information.Player1.Name;
            igrok2.Content = Models.Information.Player2.Name;
            Hod = "X";
            InitializeComponent();
            igrok1.Content = Models.Information.Player1.Name;
            igrok2.Content = Models.Information.Player2.Name;
            buttons = new Button[Models.Information.Razmernost, Models.Information.Razmernost];
            int count = 0;
            for (int i = 0; i < Models.Information.Razmernost; i++)
            {
                for (int j = 0; j < Models.Information.Razmernost; j++)
                {
                    buttons[i, j] = new Button();
                    buttons[i, j].Width = 1600 / Models.Information.Razmernost;
                    buttons[i, j].Height = 700 / Models.Information.Razmernost;
                    buttons[i, j].Tag = count;
                    Grid.Children.Add(buttons[i, j]);
                    buttons[i, j].Click += onclick;
                    count++;
                }
            }

            void onclick(object sender, EventArgs e)
            {
                Button b = (Button)sender;
                if (string.IsNullOrEmpty(b.Content?.ToString()))
                    b.Content = Hod;
                if (Hod == "X")
                    Hod = "0";
                else
                    Hod = "X";
            }
        }
    }
}
