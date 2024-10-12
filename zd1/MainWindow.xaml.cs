using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace zd1
{
    public partial class MainWindow : Window
    {
        private bool isDrawing = false;
        private Point startPoint;
        private Shape currentShape;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Начало рисования при нажатии левой кнопки мыши
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDrawing = true;
            startPoint = e.GetPosition(DrawingCanvas); // Сохраняем начальную точку

            string selectedShape = (ShapeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // В зависимости от выбранной фигуры создаем объект Shape
            switch (selectedShape)
            {
                case "Линия":
                    currentShape = new Line
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 1,
                        X1 = startPoint.X,
                        Y1 = startPoint.Y,
                        X2 = startPoint.X,
                        Y2 = startPoint.Y
                    };
                    break;
                case "Круг":
                    currentShape = new Ellipse
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    Canvas.SetLeft(currentShape, startPoint.X);
                    Canvas.SetTop(currentShape, startPoint.Y);
                    break;
                case "Квадрат":
                    currentShape = new Rectangle
                    {
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    Canvas.SetLeft(currentShape, startPoint.X);
                    Canvas.SetTop(currentShape, startPoint.Y);
                    break;
            }

            // Добавляем фигуру на холст
            if (currentShape != null)
            {
                DrawingCanvas.Children.Add(currentShape);
            }
        }

        // Процесс рисования при движении мыши
        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDrawing || currentShape == null) return;

            var pos = e.GetPosition(DrawingCanvas); // Получаем текущую позицию мыши

            if (currentShape is Line line)
            {
                // Обновляем координаты конца линии
                line.X2 = pos.X;
                line.Y2 = pos.Y;
            }
            else if (currentShape is Ellipse ellipse)
            {
                // Обновляем размеры окружности
                double radius = Math.Max(Math.Abs(pos.X - startPoint.X), Math.Abs(pos.Y - startPoint.Y));
                ellipse.Width = ellipse.Height = radius * 2;
                Canvas.SetLeft(ellipse, startPoint.X - radius);
                Canvas.SetTop(ellipse, startPoint.Y - radius);
            }
            else if (currentShape is Rectangle rect)
            {
                // Обновляем размеры и позицию квадрата
                double side = Math.Max(Math.Abs(pos.X - startPoint.X), Math.Abs(pos.Y - startPoint.Y));

                rect.Width = rect.Height = side;

                // Установка положения в зависимости от того, куда тянется мышь
                if (pos.X < startPoint.X)
                {
                    Canvas.SetLeft(rect, startPoint.X - side); // Влево от начальной точки
                }
                else
                {
                    Canvas.SetLeft(rect, startPoint.X); // Вправо от начальной точки
                }

                if (pos.Y < startPoint.Y)
                {
                    Canvas.SetTop(rect, startPoint.Y - side); // Вверх от начальной точки
                }
                else
                {
                    Canvas.SetTop(rect, startPoint.Y); // Вниз от начальной точки
                }
            }
        }

        // Завершение рисования при отпускании левой кнопки мыши
        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDrawing = false;
            currentShape = null;
        }
    }
}