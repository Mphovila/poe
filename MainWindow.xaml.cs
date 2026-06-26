using System;
using System.Windows;
using System.Windows.Input;

namespace VeyfonAI_part2
{
    public partial class MainWindow : Window
    {
        DatabaseHelper db = new DatabaseHelper();
        ActivityLogger log = new ActivityLogger();
        QuizManager quiz = new QuizManager();
        NLPProcessor nlp = new NLPProcessor();

        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
        }

        // ---------------- CHAT ----------------

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            HandleChat();
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                HandleChat();
        }

        private void HandleChat()
        {
            string input = txtInput.Text;
            txtInput.Clear();

            string intent = nlp.GetIntent(input);

            switch (intent)
            {
                case "TASK":
                    txtChat.Text += "Bot: Task command detected\n";
                    break;

                case "QUIZ":
                    StartQuiz();
                    break;

                case "LOG":
                    txtChat.Text += log.GetLogs();
                    break;

                default:
                    txtChat.Text += "Bot: I understand.\n";
                    break;
            }
        }

        // ---------------- TASKS ----------------

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            TaskItem task = new TaskItem
            {
                Title = txtTaskTitle.Text,
                Description = txtTaskDesc.Text,
                ReminderDate = dpReminder.SelectedDate?.ToString("yyyy-MM-dd"),
                Status = "Pending"
            };

            db.AddTask(task);
            log.Log("Task added: " + task.Title);

            MessageBox.Show("Task added successfully.");
            LoadTasks();
        }

        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dgTasks.SelectedItem is TaskItem t)
            {
                db.CompleteTask(t.Id);
                log.Log("Task completed: " + t.Title);

                MessageBox.Show("Task marked as completed.");
                LoadTasks();
            }
        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            if (dgTasks.SelectedItem is TaskItem t)
            {
                db.DeleteTask(t.Id);
                log.Log("Task deleted: " + t.Title);

                MessageBox.Show("Task deleted.");
                LoadTasks();
            }
        }

        private void LoadTasks()
        {
            dgTasks.ItemsSource = db.GetTasks();
        }

        // ---------------- QUIZ ----------------

        private void StartQuiz()
        {
            quiz.Reset();
            txtChat.Text += "Quiz started!\n";
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            var q = quiz.GetCurrent();

            if (q == null)
            {
                txtChat.Text += quiz.Final();
                log.Log("Quiz finished");
                return;
            }

            txtChat.Text += q.Question + "\nA: " + q.A + "\nB: " + q.B + "\nC: " + q.C + "\nD: " + q.D + "\n";
        }
    }
}