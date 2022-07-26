using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int Size_ = 9;
        int count = 0;
        SudokuSquare[,] squares = new SudokuSquare[Size_, Size_];
        SudokuSquare selectedSquare;
        public static string availableChars;
        List<string> initialGame = new List<string>();
        DispatcherTimer timer;
        int count_close=0;
        DateTime dateTime;
        SoundPlayer sp = new SoundPlayer();
        bool n = true;

        public SudokuSquare SelectedSquare
        { 
            get => selectedSquare;
            set
            {
                if (selectedSquare == value) return;
                if (selectedSquare != null) selectedSquare.HideSelection();
                selectedSquare = value;
                selectedSquare.ShowSelection();
            }
        }
        public MainWindow(object selectedItem)
        {
            InitializeComponent();
            InitializeSquares();
            count = Convert.ToInt32(selectedItem) - 1;
            HooKEvents();
            SelectedSquare = squares[0, 0];
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    squares[i, j].tbxValue.HorizontalContentAlignment = HorizontalAlignment.Center;
                    squares[i, j].tbxValue.VerticalContentAlignment = VerticalAlignment.Center;
                }
            }
            initialGame.Add(Properties.Resources.Level1);
            initialGame.Add(Properties.Resources.Level3);
            initialGame.Add(Properties.Resources.Level2);
            dateTime = new DateTime();
            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += new EventHandler(Timer_Tick);
            btnSolve.IsEnabled = false;
            btnClear.IsEnabled = false;
            btnDeleteAllNotes.IsEnabled = false;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dateTime = dateTime.AddSeconds(1);
            lb_time.Content = dateTime.ToLongTimeString();
            SquaresIsEmpty();
        }

        private void SquaresIsEmpty()
        {
            count_close = 0;
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    if (squares[i, j].tbxValue.Text != "") count_close++;
                }
            }
            progressBar.Value = count_close;
        }

        private void SetUpGame(string gameStr)
        {
            ClearGame();
            string[] lines = gameStr.Split('\n');
            int row = 0;
            foreach (var line in lines)
            {
                AddLine(row, line.TrimEnd()); // для удаления разделителей в строке
                row++;
            }
        }

        private void AddLine(int row, string line) //Каждую цифру по полученной строке
        {
            char chr;
            for (int column = 0; column < line.Length; column++)
            {
                chr = line[column];
                if(chr != ' ')
                {
                    squares[row, column].SetText(chr.ToString());
                    squares[row, column].Locked = true;
                }
                     
            }
        }

        private void ClearGame() // очистка
        {
            ClearConflicts();
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    squares[i, j].Clear();
                }
            }
        }

        private void ClearConflicts()
        {
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    squares[i, j].HasConflict = false;
                }
            }
        }

        private void InitializeSquares()
        {
            squares[0, 0] = tbx0_0;
            squares[0, 1] = tbx0_1;
            squares[0, 2] = tbx0_2;
            squares[0, 3] = tbx0_3;
            squares[0, 4] = tbx0_4;
            squares[0, 5] = tbx0_5;
            squares[0, 6] = tbx0_6;
            squares[0, 7] = tbx0_7;
            squares[0, 8] = tbx0_8;

            squares[1, 0] = tbx1_0;
            squares[1, 1] = tbx1_1;
            squares[1, 2] = tbx1_2;
            squares[1, 3] = tbx1_3;
            squares[1, 4] = tbx1_4;
            squares[1, 5] = tbx1_5;
            squares[1, 6] = tbx1_6;
            squares[1, 7] = tbx1_7;
            squares[1, 8] = tbx1_8;

            squares[2, 0] = tbx2_0;
            squares[2, 1] = tbx2_1;
            squares[2, 2] = tbx2_2;
            squares[2, 3] = tbx2_3;
            squares[2, 4] = tbx2_4;
            squares[2, 5] = tbx2_5;
            squares[2, 6] = tbx2_6;
            squares[2, 7] = tbx2_7;
            squares[2, 8] = tbx2_8;

            squares[3, 0] = tbx3_0;
            squares[3, 1] = tbx3_1;
            squares[3, 2] = tbx3_2;
            squares[3, 3] = tbx3_3;
            squares[3, 4] = tbx3_4;
            squares[3, 5] = tbx3_5;
            squares[3, 6] = tbx3_6;
            squares[3, 7] = tbx3_7;
            squares[3, 8] = tbx3_8;

            squares[4, 0] = tbx4_0;
            squares[4, 1] = tbx4_1;
            squares[4, 2] = tbx4_2;
            squares[4, 3] = tbx4_3;
            squares[4, 4] = tbx4_4;
            squares[4, 5] = tbx4_5;
            squares[4, 6] = tbx4_6;
            squares[4, 7] = tbx4_7;
            squares[4, 8] = tbx4_8;

            squares[5, 0] = tbx5_0;
            squares[5, 1] = tbx5_1;
            squares[5, 2] = tbx5_2;
            squares[5, 3] = tbx5_3;
            squares[5, 4] = tbx5_4;
            squares[5, 5] = tbx5_5;
            squares[5, 6] = tbx5_6;
            squares[5, 7] = tbx5_7;
            squares[5, 8] = tbx5_8;

            squares[6, 0] = tbx6_0;
            squares[6, 1] = tbx6_1;
            squares[6, 2] = tbx6_2;
            squares[6, 3] = tbx6_3;
            squares[6, 4] = tbx6_4;
            squares[6, 5] = tbx6_5;
            squares[6, 6] = tbx6_6;
            squares[6, 7] = tbx6_7;
            squares[6, 8] = tbx6_8;

            squares[7, 0] = tbx7_0;
            squares[7, 1] = tbx7_1;
            squares[7, 2] = tbx7_2;
            squares[7, 3] = tbx7_3;
            squares[7, 4] = tbx7_4;
            squares[7, 5] = tbx7_5;
            squares[7, 6] = tbx7_6;
            squares[7, 7] = tbx7_7;
            squares[7, 8] = tbx7_8;

            squares[8, 0] = tbx8_0;
            squares[8, 1] = tbx8_1;
            squares[8, 2] = tbx8_2;
            squares[8, 3] = tbx8_3;
            squares[8, 4] = tbx8_4;
            squares[8, 5] = tbx8_5;
            squares[8, 6] = tbx8_6;
            squares[8, 7] = tbx8_7;
            squares[8, 8] = tbx8_8;
        }

        private void HooKEvents() // подписываемся на событие по ошибкам
        {
            for (int row = 0; row < Size_; row++)
            {
                for (int column = 0; column < Size_; column++)
                {
                    squares[row, column].ValueCharge += SudokuSquare_ValueChanged;
                }
            }
        }

        private void ShowConflicts() // считаем по колонкам и строкам и выводим правильно ввели или нет 
        {
            SudokuSquare[] column;
            SudokuSquare[] row;
            SudokuSquare[] block;
            string text;
            int blockRow;
            int blockColumn;
            ClearConflicts();
            for (int r = 0; r < Size_; r++)
            {
                for (int c = 0; c < Size_; c++)
                {
                    text = squares[r, c].tbxValue.Text; 
                    if (string.IsNullOrWhiteSpace(text)) continue;
                    column = GetColumn(c);
                    row = GetRow(r);
                    block = GetBlock(r, c);
                    for (int i = 0; i < Size_; i++) // ищем совпадение по строке
                    {
                        if (i != r && column[i].GetText() == text)column[i].HasConflict = true;
                    }
                    for (int i = 0; i < Size_; i++) // ищем совпадение по колонке
                    {
                        if (i != c && row[i].GetText() == text) row[i].HasConflict = true;
                    }
                    for (int i = 0; i < Size_; i++) // ищем совпадение по блоку 3*3
                    {
                        GetSquarePosition(block[i], out blockRow, out blockColumn);
                        if (blockRow == r && blockColumn == c) continue;
                        if (block[i].GetText() == text)block[i].HasConflict = true;
                    }
                    if (squares[r, c].HasConflict == true)SystemSounds.Exclamation.Play();
                }
            }

        }

        private void SudokuSquare_ValueChanged(object sender, EventArgs e)=> ShowConflicts();

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) // сдвиг
        {
            if (e.Key == Key.Right)
            {
                GetSquarePosition(SelectedSquare, out int row, out int column);
                SelectSquare(row, column + 1);
            }
            else if (e.Key == Key.Left)
            {
                GetSquarePosition(SelectedSquare, out int row, out int column);
                SelectSquare(row, column - 1);
            }
            else if (e.Key == Key.Up)
            {
                GetSquarePosition(SelectedSquare, out int row, out int column);
                SelectSquare(row - 1, column);
            }
            else if (e.Key == Key.Down)
            {
                GetSquarePosition(SelectedSquare, out int row, out int column);
                SelectSquare(row + 1, column);
            }

        }

        private void GetSquarePosition(SudokuSquare square, out int row, out int column) // возвращаем где находимся
        {
            for (int j = 0; j < Size_; j++)
            {
                for (int i = 0; i < Size_; i++)
                {
                    if (squares[i, j] == square)
                    {
                        row = i;
                        column = j;
                        return;
                    }
                }
            }
            row = -1;
            column = -1;
        }

        private void SelectSquare(int row, int column) // переход если больше 9 или меньше 1
        {
            if (column >= Size_) column -= Size_;
            else if (column < 0) column += Size_;
            if (row >= Size_) row -= Size_;
            else if (row < 0) row += Size_;
            SelectedSquare = squares[row, column];
        }


        private void txbAvailableCharacters_TextChanged(object sender, TextChangedEventArgs e) =>SetAvailableCharacters();

        private void SetAvailableCharacters() => availableChars = txbAvailableCharacters.Text.Trim(); 

        private void btnFill_Click(object sender, RoutedEventArgs e) =>Fill();

        private void Fill()
        {
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    squares[i, j].FillPosition(); /* проверяем наличие 1 значение в tbxNotes */
                }
            }
        }

        private void ShowNotes(SudokuSquare square)
        {
            int r;
            int c;
            if (square.GetText().Trim().Length > 0) return;
            GetSquarePosition(square, out r, out c); // получаем строку и колонку
            if (r != -1 && c != -1)
            {
                SudokuSquare[] column = GetColumn(c); // возвращаем значения в колонке
                SudokuSquare[] row = GetRow(r);// возвращаем значения в стороке
                SudokuSquare[] block = GetBlock(r, c);// возвращаем значения в блоке 3*3
                List<char> availableChars = new List<char>();
                foreach (var item in txbAvailableCharacters.Text) // возвращаем разрешенную строку для ввода 
                {
                    availableChars.Add(item);
                }
                RemoveChars(availableChars, column);
                RemoveChars(availableChars, row);
                RemoveChars(availableChars, block);
                square.SetNotes(string.Join(", ", availableChars));
            }
           
        }

        private void RemoveChars(List<char> availableChars, SudokuSquare[] group)  // если в tbxNotes есть значение tbxValue удаляем его
        {
            for (int i = 0; i < Size_; i++)
            {
                if (availableChars.Contains(group[i].Value)) availableChars.Remove(group[i].Value);
            }
        }

        private SudokuSquare[] GetBlock(int row, int column)
        {
            int topRow = 3 * (int)Math.Floor((decimal)row / 3);
            int leftColumn = 3 * (int)Math.Floor((decimal)column / 3);
            int index = 0;
            SudokuSquare[] result = new SudokuSquare[Size_];
            for (int r = topRow; r < topRow+3; r++)
            {
                for (int c = leftColumn; c < leftColumn+3; c++)
                {
                    result[index] = squares[r, c];
                    index++;
                }
            }
            return result;
        }

        private SudokuSquare[] GetColumn(int column)
        {
            SudokuSquare[] result = new SudokuSquare[Size_];
            for (int row = 0; row < Size_; row++)
            {
                result[row] = squares[row, column];
            }
            return result;
        }

        private SudokuSquare[] GetRow(int row)
        {
            SudokuSquare[] result = new SudokuSquare[Size_];
            for (int column = 0; column < Size_; column++)
            {
                result[column] = squares[row, column];
            }
            return result;
        }

        private void btnDeleteAllNotes_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    squares[i, j].tbNotes.Text = "";
                }
            }
        }

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            Fill();
            AllNotes();
        }

        private void AllNotes()
        {
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    ShowNotes(squares[i, j]);
                }
            }
        }

        private void btnGame_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    squares[i, j].Locked = false;
                }
            }
            btnSolve.IsEnabled = true;
            btnClear.IsEnabled = true;
            btnDeleteAllNotes.IsEnabled = true;
            grd_Sudoku.IsEnabled = true;
            timer.Start();
            SetUpGame(initialGame[count]);
            btnGame.IsEnabled = false;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < Size_; i++)
            {
                for (int j = 0; j < Size_; j++)
                {
                    if(squares[i,j].IsEnabled == false)
                    {
                        squares[i, j].tbxValue.Text = "";
                        squares[i, j].tbNotes.Text = "";
                    }
                }
            }
            ClearConflicts();
            SetUpGame(initialGame[count]);
        }

        private void Window_Closed(object sender, EventArgs e)=>sp.Stop();

        private void progressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(progressBar.Value == Size_*Size_)
            {
                timer.Stop();
                MessageBox.Show($"Вы прошли {count +1} уровень!");
                if (count == 0) Menu.mass[0] = true;
                else if(count == 1) Menu.mass[1] = true;
                else if(count == 2) Menu.mass[2] = true;
                if (Menu.Mass_true())
                {
                    MessageBox.Show("Вы прошли игру!");
                    if (MessageBox.Show("Начать заново?", " ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        for (int i = 0; i < Menu.mass.Length; i++)
                        {
                            Menu.mass[i] = false;
                        }
                        count = 0;
                        btnSolve.IsEnabled = false;
                        btnClear.IsEnabled = false;
                        btnDeleteAllNotes.IsEnabled = false;
                        ClearGame();
                        btnGame_Click(this, new RoutedEventArgs());
                        return;
                    }
                    else Close();
                }
                if(count != 2)
                {
                    if (MessageBox.Show($"Перейти на {count + 2} уровень ?", " ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        btnSolve.IsEnabled = false;
                        btnClear.IsEnabled = false;
                        btnDeleteAllNotes.IsEnabled = false;
                        dateTime = new DateTime();
                   
                        if (count < 2) count++;
                        ClearGame();
                        btnGame_Click(this, new RoutedEventArgs());
                    }
                else
                {
                    if (MessageBox.Show("Пройти этот уровень ещё раз ?", " ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        dateTime = new DateTime();
                        ClearGame();
                        btnGame_Click(this, new RoutedEventArgs());
                    }
                    else Close();
                }
                }
               
                
            }
        }

        private void btnMusic_Click(object sender, RoutedEventArgs e)
        {
            if (n)
            {
                sp.SoundLocation = "1.wav";
                sp.Load();
                sp.PlayLooping();
                n = false;
            }
            else
            {
                sp.Stop();
                n = true;
            }
        }
    }
}
