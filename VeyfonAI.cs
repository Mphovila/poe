namespace VeyfonAI_part2
{
    public class VeyfonAI
    {
        public string GetResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("password"))
                return "Use strong passwords with symbols.";

            if (input.Contains("phishing"))
                return "Phishing is fake messages stealing data.";

            if (input.Contains("malware"))
                return "Malware is harmful software.";

            if (input.Contains("vpn"))
                return "VPN hides your identity online.";

            return "Ask about cybersecurity topics.";
        }
    }
}