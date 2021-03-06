﻿//---------------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs">(c) Mike Fourie. All other rights reserved.</copyright>
//---------------------------------------------------------------------------------------------------------------------------
namespace MSBuildExplorer
{
    using System;
    using System.Windows;
    
    /// <summary>
    /// Interaction logic for MainWindow
    /// </summary>
    public partial class MainWindow
    {
        private buildMode mode = buildMode.Explorer;

        public MainWindow()
        {
            this.InitializeComponent();
            var userPrefs = new UserPreferences();
            this.Height = userPrefs.WindowHeight;
            this.Width = userPrefs.WindowWidth;
            this.Top = userPrefs.WindowTop;
            this.Left = userPrefs.WindowLeft;
            this.WindowState = userPrefs.WindowState;
        }

        private enum buildMode
        {
            /// <summary>
            /// BuildPad
            /// </summary>
            BuildPad,

            /// <summary>
            /// Explorer
            /// </summary>
            Explorer
        }

        private void menuShowExplorer(object sender, RoutedEventArgs e)
        {
            this.ClearControls();
            this.mode = buildMode.Explorer;
            this.Explorer.Visibility = System.Windows.Visibility.Visible;
        }

        private void menuShowBuildPad(object sender, RoutedEventArgs e)
        {
            this.ClearControls();
            this.mode = buildMode.BuildPad;
            this.BuildPad.Visibility = System.Windows.Visibility.Visible;
        }

        private void menuBlog(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://mikefourie.wordpress.com");
        }

        private void menuHomePage(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.msbuildexplorer.com");
        }

        private void menuMSBEP(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.msbuildextensionpack.com");
        }

        private void menuOpen(object sender, RoutedEventArgs e)
        {
            if (this.mode == buildMode.Explorer)
            {
                this.Explorer.MenuOpen();
            }
            else
            {
                this.BuildPad.MenuOpen();
            }
        }

        private void menuAbout(object sender, RoutedEventArgs e)
        {
            this.ClearControls();
            this.About.Visibility = Visibility.Visible;
        }

        private void menuSaveFavorite(object sender, RoutedEventArgs e)
        {
            if (this.mode == buildMode.Explorer)
            {
                this.SaveFavorite();
            }
            else
            {
                this.BuildPad.SaveFile();
            }
        }

        private void menuConsoleBuild(object sender, RoutedEventArgs e)
        {
            if (this.mode == buildMode.Explorer)
            {
                this.Explorer.Build();
            }
            else
            {
                this.BuildPad.BuildFile();
            }
        }

        private void menuOptions(object sender, RoutedEventArgs e)
        {
            this.ClearControls();
            this.Options.Visibility = Visibility.Visible;
        }
        
        private void SaveFavorite()
        {
            this.Explorer.SaveFavorite();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var userPrefs = new UserPreferences
                            {
                                WindowHeight = this.ActualHeight,
                                WindowWidth = this.ActualWidth,
                                WindowTop = this.Top,
                                WindowLeft = this.Left,
                                WindowState = this.WindowState,
                                WindowLeftColumn = Convert.ToDouble(this.Explorer.columnLeft.Width.ToString()),
                                WindowRightColumn =
                                    Convert.ToDouble(this.Explorer.columnRight.Width.ToString())
                            };
            userPrefs.Save();
        }

        private void ClearControls()
        {
            this.Explorer.Visibility = Visibility.Hidden;
            this.BuildPad.Visibility = Visibility.Hidden;
            this.About.Visibility = Visibility.Hidden;
            this.Options.Visibility = Visibility.Hidden;
        }
    }
}
