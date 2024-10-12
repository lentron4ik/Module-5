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

namespace zd5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string input = string.Empty; // Текущий ввод
        private string operation = string.Empty; // Операция
        private double firstNumber = 0; // Первое число
        private bool isNewOperation = false; // Указывает, является ли следующая операция новой

        public MainWindow()
        {
            InitializeComponent();
        }

        // Обработка нажатий на цифровые кнопки
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string value = button.Content.ToString();

            // Если была выполнена операция, сбрасываем ввод
            if (isNewOperation)
            {
                input = string.Empty;
                Display.Text = string.Empty;
                isNewOperation = false;
            }

            // Добавляем нажатую цифру в текущее поле
            input += value;
            Display.Text += value; // Показываем текущий ввод
        }

        // Обработка нажатий на операции (+, -, *, /)
        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // Если введено число, сохраняем его и операцию
            if (double.TryParse(input, out firstNumber))
            {
                operation = button.Content.ToString();
                Display.Text += $" {operation} "; // Показываем введенную операцию
                input = string.Empty; // Очищаем поле для второго числа
            }
        }

        // Обработка нажатия на кнопку "="
        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            double secondNumber;

            // Если введено второе число
            if (double.TryParse(input, out secondNumber))
            {
                double result = 0;

                // Выполняем операцию
                switch (operation)
                {
                    case "+":
                        result = firstNumber + secondNumber;
                        break;
                    case "-":
                        result = firstNumber - secondNumber;
                        break;
                    case "*":
                        result = firstNumber * secondNumber;
                        break;
                    case "/":
                        if (secondNumber != 0)
                        {
                            result = firstNumber / secondNumber;
                        }
                        else
                        {
                            MessageBox.Show("Деление на ноль невозможно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        break;
                }

                // Очищаем текстовое поле и показываем результат
                Display.Text = result.ToString();

                // Сохраняем результат как первое число для следующей операции
                firstNumber = result;
                input = result.ToString();
                isNewOperation = true; // Следующая операция будет новой
            }
        }

        // Очистка калькулятора
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            input = string.Empty;
            firstNumber = 0;
            operation = string.Empty;
            Display.Text = string.Empty; // Очищаем экран
        }
    }
}
