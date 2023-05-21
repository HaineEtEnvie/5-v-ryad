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
    public partial class GameWIndow : Window
    {
        private Button[,] buttons; // Массив кнопок
        private bool isPlayer1Turn = true; // Ход игрока 1
        int CR = Models.Information.Razmernost;
        //private const int CR = a; // Количество столбцов и столбцов
        private const int WIN_LENGTH = 5; // Длина выигрышной последовательности
        public GameWIndow()
        {
            InitializeComponent();
            igrok1.Content = Models.Information.Player1.Name;
            igrok2.Content = Models.Information.Player2.Name;

            // создание Grid
            Grid grid = new Grid();
            // Добавляем строки и столбцы
            RowDefinition qwe = new RowDefinition();
            qwe.Height = new GridLength(1, GridUnitType.Star);
            grid.RowDefinitions.Add(qwe);

            for (int i = 1; i < CR; i++)
            {
                RowDefinition row = new RowDefinition();
                row.Height = new GridLength(1, GridUnitType.Star);
                grid.RowDefinitions.Add(row);

                ColumnDefinition col = new ColumnDefinition();
                col.Width = new GridLength(1, GridUnitType.Star);
                grid.ColumnDefinitions.Add(col);
            }

            // Создаем массив кнопок
            buttons = new Button[CR, CR];

            // Добавляем кнопки в каждую ячейку
            for (int i = 0; i < CR; i++)
            {
                for (int j = 0; j < CR; j++)
                {
                    Button button = new Button();
                    button.Content = ""; // Пустой текст кнопки
                    button.Click += Button_Click; // Обработчик клика на кнопку
                    Grid.SetRow(button, i); // Устанавливаем строку
                    Grid.SetColumn(button, j); // Устанавливаем столбец
                    grid.Children.Add(button); // Добавляем кнопку в Grid
                    buttons[i, j] = button; // Добавляем кнопку в массив
                }
            }

            // Добавляем Grid на окно
            this.Content = grid;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            // Если кнопка уже нажата, выходим
            if (button.Content.ToString() != "") return;

            // Устанавливаем текст кнопки в зависимости от хода игрока
            if (isPlayer1Turn)
            {
                button.Content = "X";
            }
            else
            {
                button.Content = "O";
            }

            // Проверяем, есть ли победитель
            if (CheckForWinner())
            {
                MessageBox.Show($"Игрок {(isPlayer1Turn ? "1" : "2")} победил!");
                ResetGame();
                return;
            }

            // Переключаем ход игрока
            isPlayer1Turn = !isPlayer1Turn;
        }

        private bool CheckForWinner()
        {
            // Проверяем горизонтали
            for (int i = 0; i < CR; i++)
            {
                for (int j = 0; j <= CR - WIN_LENGTH; j++)
                {
                    bool isWin = true;
                    for (int k = 0; k < WIN_LENGTH; k++)
                    {
                        if (buttons[i, j + k].Content.ToString() != buttons[i, j].Content.ToString())
                        {
                            isWin = false;
                            break;
                        }
                    }
                    if (isWin) return true;
                }
            }

            // Проверяем вертикали
            for (int i = 0; i <= CR - WIN_LENGTH; i++)
            {
                for (int j = 0; j < CR; j++)
                {
                    bool isWin = true;
                    for (int k = 0; k < WIN_LENGTH; k++)
                    {
                        if (buttons[i + k, j].Content.ToString() != buttons[i, j].Content.ToString())
                        {
                            isWin = false;
                            break;
                        }
                    }
                    if (isWin) return true;
                }
            }

            // Проверяем диагонали слева направо
            for (int i = 0; i <= CR - WIN_LENGTH; i++)
            {
                for (int j = 0; j <= CR - WIN_LENGTH; j++)
                {
                    bool isWin = true;
                    for (int k = 0; k < WIN_LENGTH; k++)
                    {
                        if (buttons[i + k, j + k].Content.ToString() != buttons[i, j].Content.ToString())
                        {
                            isWin = false;
                            break;
                        }
                    }
                    if (isWin) return true;
                }
            }

            // Проверяем диагонали справа налево
            for (int i = 0; i <= CR - WIN_LENGTH; i++)
            {
                for (int j = WIN_LENGTH - 1; j < CR; j++)
                {
                    bool isWin = true;
                    for (int k = 0; k < WIN_LENGTH; k++)
                    {
                        if (buttons[i + k, j - k].Content.ToString() != buttons[i, j].Content.ToString())
                        {
                            isWin = false;
                            break;
                        }
                    }
                    if (isWin) return true;
                }
            }
            return false;
        }
        private void ResetGame()
        {
            // Очищаем текст на кнопках
            foreach (Button button in buttons)
            {
                button.Content = "";
            }

            // Сбрасываем ход игрока
            isPlayer1Turn = true;
        }
    }
}
