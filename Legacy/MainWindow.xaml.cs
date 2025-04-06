using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DiscordRPC;
using DiscordRPC.Events;
using DiscordRPC.Logging;
using Arcane_Launcher.Utils;

namespace Arcane_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Utils.Globals.MainFrame = MainFrame;

            Utils.Globals.DiscordRpcClient = new DiscordRpcClient("1357677873317810237");

            Utils.Globals.DiscordRpcClient.OnReady += (sender, e) =>
            {
                Utils.Logger.good("Discord RPC is ready!");
            };

            Utils.Globals.DiscordRpcClient.OnError += (sender, e) =>
            {
                Utils.Logger.good($"Discord RPC error: {e.Message}");
            };

            Utils.Globals.DiscordRpcClient.Initialize();
            SetRichPresence();

            MainFrame.Navigate(new Pages.InitLauncher());
        }

        public void SetRichPresence()
        {
            Utils.Globals.DiscordPresence = new RichPresence
            {
                Details = "Logging in...",
                State = "Idling...",

                Timestamps = Timestamps.Now,
                Assets = new Assets
                {
                    LargeImageKey = "https://avatars.githubusercontent.com/u/202678516",
                    LargeImageText = "Legacy",
                },

                Buttons = new DiscordRPC.Button[]
                {
                    new DiscordRPC.Button()
                    {
                        Label = "Join Legacy!",
                        Url = "https://discord.gg/3AP942Wc96"
                    },
                    new DiscordRPC.Button()
                    {
                        Label = "View our github!",
                        Url = "https://github.com/Legacy-OG"
                    }
                }
            };
            Utils.Globals.DiscordRpcClient.SetPresence(Utils.Globals.DiscordPresence);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}