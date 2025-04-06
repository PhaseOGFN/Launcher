using Arcane_Launcher.Pages.Launcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using DiscordRPC;

namespace Arcane_Launcher.Utils
{
    public class Globals
    {
        public static Frame MainFrame;
        public static Frame ViewFrame;

        public static MainView MainView;

        public static RichPresence DiscordPresence;
        public static DiscordRpcClient DiscordRpcClient;
    }
}
