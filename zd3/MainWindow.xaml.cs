using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace zd3
{
    public partial class MainWindow : Window
    {
        // Списки для задач
        private List<string> tasks;
        private List<string> completedTasks;

        public MainWindow()
        {
            InitializeComponent();
            tasks = new List<string>();
            completedTasks = new List<string>();
        }

        // Добавление новой задачи
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var taskText = TaskInputTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(taskText))
            {
                tasks.Add(taskText);
                TaskListBox.Items.Add(taskText);
                TaskInputTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Введите текст задачи.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Удаление задачи
        private void RemoveTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem != null)
            {
                var selectedTask = TaskListBox.SelectedItem.ToString();
                tasks.Remove(selectedTask);
                TaskListBox.Items.Remove(selectedTask);
                RemoveTaskButton.IsEnabled = false;
                CompleteTaskButton.IsEnabled = false;
            }
        }

        // Отметка задачи как выполненной
        private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskListBox.SelectedItem != null)
            {
                var selectedTask = TaskListBox.SelectedItem.ToString();
                tasks.Remove(selectedTask);

                // Перенос задачи в список выполненных
                completedTasks.Add(selectedTask);
                CompletedTaskListBox.Items.Add(selectedTask);

                TaskListBox.Items.Remove(selectedTask);
                RemoveTaskButton.IsEnabled = false;
                CompleteTaskButton.IsEnabled = false;
            }
        }

        // Выбор задачи в активном списке
        private void TaskListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RemoveTaskButton.IsEnabled = TaskListBox.SelectedItem != null;
            CompleteTaskButton.IsEnabled = TaskListBox.SelectedItem != null;
        }
    }
}