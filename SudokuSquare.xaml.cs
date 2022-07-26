using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace Exam
{
    /// <summary>
    /// Логика взаимодействия для SudokuSquare.xaml
    /// </summary>
    public partial class SudokuSquare : UserControl
    {
        public event EventHandler ValueCharge;
        private bool hasConflict;
        private bool locked;

        public string Notes 
        {
            get => tbNotes.Text;
            set => tbNotes.Text = value;
        }

        public bool HasConflict // правильный ввод или нет
        {
            get => hasConflict;
            set
            {
                if (hasConflict == value) return;
                hasConflict = value;
                _ = hasConflict ? Grid1.Background = new SolidColorBrush(Color.FromArgb(251, 227, 123, 123)) : Grid1.Background = null ;
            }
        }

        public bool Locked // начало игры закрытые ячейки
        {
            get => locked;
            set
            {
                if (locked == value) return;
                locked = value;
                if (locked)
                {
                    tbxValue.Foreground = new SolidColorBrush(Color.FromRgb(97, 94, 94));
                    tbxValue.IsReadOnly = true;
                }
                else
                {
                    tbxValue.Foreground = new SolidColorBrush(Colors.Black);
                    tbxValue.IsReadOnly = false;

                }
            }
        }

        public char Value // если что то есть то вернуть символ, иначе \0
        {
            get
            {
                if (tbxValue.Text.Length > 0) return tbxValue.Text[0];
                return char.MinValue;
            }
        }

        public SudokuSquare()
        {
            InitializeComponent();
        }

        public void tbxValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.Changes != null)
            {
                if (sender is TextBox textBox)
                {
                    
                    string lineText = textBox.GetLineText(0);
                    if (MainWindow.availableChars.Contains(lineText.Substring(e.Changes.First().Offset, e.Changes.First().AddedLength))) // есть ли этот символ в программой заданных символах... lineText.Substring(e.Changes.First().Offset, e.Changes.First().AddedLength) чтоб можно было очистить textbox
                    {
                        if (textBox.Text != lineText.Substring(e.Changes.First().Offset, e.Changes.First().AddedLength)) textBox.Text = lineText.Substring(e.Changes.First().Offset, e.Changes.First().AddedLength);
                    }
                    else if (textBox.Text != " ") textBox.Text = "";
                    OnValueCharge(this, EventArgs.Empty); // отвечает за не правильный ввод
                }
            }


        }

        private void tbxValue_PreviewMouseDown(object sender, MouseButtonEventArgs e)=>tbxValue.Focus();


        public void ShowSelection()  //для движения клавишами
        {
            Border1.Background = new SolidColorBrush(Color.FromArgb(241, 242, 161, 107));
            tbxValue.Focus();
        }

        public void HideSelection() => Border1.Background = null; // делаем без цвета, для движения клавишами

        public void SetNotes(string notes) => tbNotes.Text = notes;

        public string GetText() => tbxValue.Text;

        public void FillPosition() // если подсказка осталась 1 цифра то автоматически ставим её
        {
            if (tbNotes.Text.Length == 1)
            {
                tbxValue.Text = tbNotes.Text;
                tbNotes.Text = "";
            }
        }

        public void Clear() // очищаем всё
        {
            tbxValue.Text = "";
            tbNotes.Text = "";
        }

        public void SetText(string str) => tbxValue.Text = str;

        private void OnValueCharge(object sender, EventArgs e) => ValueCharge?.Invoke(sender, e); // событие для проверки введёного символа


    }
}
