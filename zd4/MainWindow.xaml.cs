using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace zd4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                txtImagePath.Text = openFileDialog.FileName;

                try
                {
                    BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                    imageControl.Source = bitmap;

                    // Сбрасываем значение ползунка при загрузке нового изображения
                    sliderZoom.Value = 1;
                    imageControl.Width = bitmap.PixelWidth; // Устанавливаем начальную ширину
                    imageControl.Height = bitmap.PixelHeight; // Устанавливаем начальную высоту
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при загрузке изображения: {ex.Message}");
                }
            }
        }

        private void sliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            // Проверяем, что imageControl и его Source не равны null
            if (imageControl != null && imageControl.Source != null)
            {
                double scale = sliderZoom.Value;

                // Проверяем, что source является BitmapSource
                if (imageControl.Source is BitmapSource bitmapSource)
                {
                    // Обновляем размеры изображения
                    imageControl.Width = bitmapSource.PixelWidth * scale;
                    imageControl.Height = bitmapSource.PixelHeight * scale;
                }
                else
                {
                    MessageBox.Show("Изображение не является допустимым форматом.");
                }
            }
            else
            {
                /*MessageBox.Show("Изображение не загружено или элемент управления не инициализирован.");*/
            }
        }
    }
}
