namespace VeyfonAI_part2
{
    public class NLPProcessor
    {
        public string GetIntent(string input)
        {
            input = input.ToLower();

            if (input.Contains("add task") || input.Contains("create task"))
                return "TASK";

            if (input.Contains("quiz") || input.Contains("test"))
                return "QUIZ";

            if (input.Contains("log") || input.Contains("history"))
                return "LOG";

            return "CHAT";
        }
    }
}