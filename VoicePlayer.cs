using System;
using System.IO;
using System.Media;

namespace VeyfonAI_part2
{
    public class VoicePlayer
    {
        public void PlayGreeting()
        {
            try
            {
                
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "greeting.wav"
                );

                if (File.Exists(path))
                {
                    SoundPlayer player = new SoundPlayer(path);
                    player.Play();
                }
            }
            catch
            {
                // keep silent (no crash)
            }
        }
    }
}