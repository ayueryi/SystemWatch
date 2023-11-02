// This Source Code Form is subject to the terms of the MIT License.
// If a copy of the MIT was not distributed with this file, You can obtain one at https://opensource.org/licenses/MIT.
// Copyright (C) Leszek Pomianowski and WPF UI Contributors.
// All Rights Reserved.

using System.Windows.Controls;
using System.Windows.Media.Imaging;

using SystemWatch.Views.Windows;

using Wpf.Ui.Controls;

namespace SystemWatch.ViewModels.Pages
{
    public partial class SettingsViewModel : ObservableObject, INavigationAware
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _appVersion = String.Empty;

        [ObservableProperty]
        private Wpf.Ui.Appearance.ApplicationTheme _currentTheme = Wpf.Ui.Appearance.ApplicationTheme.Unknown;

        public void OnNavigatedTo()
        {
            if (!_isInitialized)
                InitializeViewModel();
        }

        public void OnNavigatedFrom() { }

        private void InitializeViewModel()
        {
            CurrentTheme = Wpf.Ui.Appearance.ApplicationThemeManager.GetAppTheme();
            AppVersion = $"SystemWatch - {GetAssemblyVersion()}";

            _isInitialized = true;
        }

        private string GetAssemblyVersion()
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version?.ToString()
                ?? String.Empty;
        }

        [RelayCommand]
        private void OnChangeTheme(string parameter)
        {
            var titleBar = App.GetService<MainWindow>().TitleBar;

            switch (parameter)
            {
                case "theme_light":
                    if (CurrentTheme == Wpf.Ui.Appearance.ApplicationTheme.Light)
                        break;

                    Wpf.Ui.Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Light);
                    CurrentTheme = Wpf.Ui.Appearance.ApplicationTheme.Light;
                    titleBar.Icon = new ImageIcon() { Source = new BitmapImage(new Uri("pack://application:,,,/Assets/logo-black.png")) };

                    break;

                default:
                    if (CurrentTheme == Wpf.Ui.Appearance.ApplicationTheme.Dark)
                        break;

                    Wpf.Ui.Appearance.ApplicationThemeManager.Apply(Wpf.Ui.Appearance.ApplicationTheme.Dark);
                    CurrentTheme = Wpf.Ui.Appearance.ApplicationTheme.Dark;
                    titleBar.Icon = new ImageIcon() { Source = new BitmapImage(new Uri("pack://application:,,,/Assets/logo-white.png")) };

                    break;
            }
        }
    }
}
