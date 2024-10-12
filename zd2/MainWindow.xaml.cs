using Microsoft.Win32;
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
using System.IO;

namespace zd2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentFilePath; // Для хранения пути текущего файла

        public MainWindow()
        {
            InitializeComponent();
        }

        // Открыть файл
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == true)
                {
                    currentFilePath = openFileDialog.FileName;
                    string fileContent = File.ReadAllText(currentFilePath);
                    EditorTextBox.Text = fileContent; // Читаем содержимое файла
                    MessageBox.Show("Файл успешно загружен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Сохранить файл
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Если путь к текущему файлу не задан, то открываем диалог для сохранения файла
                if (string.IsNullOrEmpty(currentFilePath))
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

                    if (saveFileDialog.ShowDialog() == true)
                    {
                        currentFilePath = saveFileDialog.FileName; // Устанавливаем путь к файлу
                    }
                    else
                    {
                        return; // Если пользователь отменил сохранение, выходим из метода
                    }
                }

                // Сохраняем текст из TextBox в текущий файл
                File.WriteAllText(currentFilePath, EditorTextBox.Text);
                MessageBox.Show("Файл успешно сохранен", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
