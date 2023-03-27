using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;

namespace StoreApp.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string FilePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "RememberData.txt");

        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
    }
}
