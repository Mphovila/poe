using System;
using System.Collections.Generic;

namespace VeyfonAI_part2
{
    public class ActivityLogger
    {
        List<string> logs = new List<string>();

        public void Log(string action)
        {
            logs.Add($"{DateTime.Now}: {action}");
        }

        public string GetLogs()
        {
            string result = "Activity Log:\n";

            int start = Math.Max(0, logs.Count - 10);

            for (int i = start; i < logs.Count; i++)
                result += logs[i] + "\n";

            return result;
        }
    }
}