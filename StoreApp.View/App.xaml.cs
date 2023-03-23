using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
