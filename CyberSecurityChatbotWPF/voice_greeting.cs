
using System.Media;

namespace CyberSecurityChatbotWPF
{
    public class voice_greeting
    {
        public void greet()
        {
            try
            {
                SoundPlayer player =
                    new SoundPlayer("welcome.wav");

                player.Play();
            }
            catch
            {

            }
        }
    }
}
