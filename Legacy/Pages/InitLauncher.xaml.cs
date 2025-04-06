using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Arcane_Launcher.Responses.Lightswitch;
using Arcane_Launcher.Utils;
using Arcane_Launcher.Services;
using System.Reflection;
using DiscordRPC;

namespace Arcane_Launcher.Pages
{
    /// <summary>
    /// Interaction logic for InitLauncher.xaml
    /// </summary>
    public partial class InitLauncher : Page
    {
        private string domain = "http://legacy-service-prod.ol.evolvefn.com:3551";
        private static readonly HttpClient httpClient = new HttpClient();

        public InitLauncher()
        {
            InitializeComponent();
            SetupLauncher();
        }

        private async Task SetupLauncher()
        {
            Utils.Logger.good($"Execution Path: {System.IO.Path.ChangeExtension(Assembly.GetExecutingAssembly().Location, ".exe")}");
            if (!string.IsNullOrEmpty(Properties.Settings.Default.GamePath))
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(Properties.Settings.Default.GamePath, "FortniteGame\\Content\\Splash", "Splash.bmp")))
                {
                    System.IO.File.Copy(
                        System.IO.Path.Combine(Properties.Settings.Default.GamePath, "FortniteGame\\Content\\Splash", "Splash.bmp"),
                        System.IO.Path.Combine(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources"), "Splash.bmp"),
                        overwrite: true
                    );
                }
                else
                {
                    Utils.Logger.warn("No splash image found!");
                }
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.access_token) && !string.IsNullOrEmpty(Properties.Settings.Default.refresh_token))
            {
                JObject json = await Authentication.RefreshToken(Properties.Settings.Default.refresh_token);
                if (json.ContainsKey("access_token"))
                {
                    Utils.Globals.DiscordPresence.Details = "Selecting Game Build...";
                    Utils.Globals.DiscordPresence.State = $"Logged in as {Properties.Settings.Default.displayName}";
                    Utils.Globals.DiscordRpcClient.SetPresence(Utils.Globals.DiscordPresence);
                    Utils.Globals.MainFrame.Navigate(new Pages.Launcher.MainView());
                }
                else
                {
                    Utils.Logger.warn("Could not refresh token!");
                    ShowErrorOverlay(json["errorCode"]?.ToString(), json["errorMessage"]?.ToString());
                    Utils.Globals.MainFrame.Navigate(new Pages.Auth.Login());
                }
            }
            else
            {
                Utils.Globals.MainFrame.Navigate(new Pages.Auth.Login());
            }
        }

        private async Task<LightswitchStatus> GetLightswitchStatusAsync()
        {
            try
            {
                string url = $"{domain}/lightswitch/api/service/launcher/status";
                HttpResponseMessage response = await httpClient.GetAsync(url);
                if ((int)response.StatusCode != 200)
                {
                    Utils.Logger.warn("got bad response from " + url + " : " + await response.Content.ReadAsStringAsync());
                    return null;
                }
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Utils.Logger.good("Successfully got response from " + url + " : " + responseBody);

                LightswitchStatus status = JsonConvert.DeserializeObject<LightswitchStatus>(responseBody);

                Utils.Logger.good($"Successfully got status: {status?.Status}");
                Utils.Logger.good($"Message: {status?.Message}");
                Utils.Logger.good($"Deserialized App Name: {status?.LauncherInfoDTO?.AppName}");

                return status;
            }
            catch
            {
                Utils.Logger.warn($"Could not get service status!");
                return null;
            }
        }

        public void ShowErrorOverlay(string errorTitle, string errorMessage)
        {
            Dispatcher.Invoke(() =>
            {
                ErrorTitle.Text = errorTitle;
                ErrorMessage.Text = errorMessage;
                ErrorOverlay.Visibility = Visibility.Visible;
            });
        }

        private void CloseErrorOverlay(object sender, RoutedEventArgs e)
        {
            ErrorOverlay.Visibility = Visibility.Collapsed;
        }
    }
}