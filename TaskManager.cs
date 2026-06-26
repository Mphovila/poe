using System.Collections.Generic;

namespace VeyfonAI_part2
{
    public class TaskManager
    {
        private List<string> tasks = new List<string>();

        public string HandleTask(string input)
        {
            input = input.ToLower();

            if (input.Contains("add"))
            {
                string task = input.Replace("add task", "").Trim();
                tasks.Add(task);
                return "Task added: " + task;
            }

            return "Try: add task [your task]";
        }

        public string ViewTasks()
        {
            if (tasks.Count == 0) return "No tasks.";

            string result = "Tasks:\n";
            foreach (var t in tasks)
                result += "- " + t + "\n";

            return result;
        }
    }
}