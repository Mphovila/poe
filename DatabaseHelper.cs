using MySql.Data.MySqlClient;
using MySqlConnector;
using System.Collections.Generic;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;

namespace VeyfonAI_part2
{
    public class DatabaseHelper
    {
        string conn = "server=localhost;user=root;password=;database=veyfonai;";

        public void AddTask(TaskItem task)
        {
            using var c = new MySqlConnection(conn);
            c.Open();

            var cmd = new MySqlCommand(
                "INSERT INTO tasks(title,description,reminder,status) VALUES(@t,@d,@r,'Pending')", c);

            cmd.Parameters.AddWithValue("@t", task.Title);
            cmd.Parameters.AddWithValue("@d", task.Description);
            cmd.Parameters.AddWithValue("@r", task.ReminderDate);

            cmd.ExecuteNonQuery();
        }

        public void DeleteTask(int id)
        {
            using var c = new MySqlConnection(conn);
            c.Open();

            var cmd = new MySqlCommand("DELETE FROM tasks WHERE id=@id", c);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void CompleteTask(int id)
        {
            using var c = new MySqlConnection(conn);
            c.Open();

            var cmd = new MySqlCommand(
                "UPDATE tasks SET status='Completed' WHERE id=@id", c);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public List<TaskItem> GetTasks()
        {
            List<TaskItem> list = new List<TaskItem>();

            using var c = new MySqlConnection(conn);
            c.Open();

            var cmd = new MySqlCommand("SELECT * FROM tasks", c);
            var r = cmd.ExecuteReader();

            while (r.Read())
            {
                list.Add(new TaskItem
                {
                    Id = r.GetInt32("id"),
                    Title = r.GetString("title"),
                    Description = r.GetString("description"),
                    ReminderDate = r.GetString("reminder"),
                    Status = r.GetString("status")
                });
            }

            return list;
        }
    }
}