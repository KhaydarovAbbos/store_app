using StoreApp.Domain.Entities.Products;
using StoreApp.Domain.Entities.Stores;
using StoreApp.Service.Interfaces;
using StoreApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static StoreApp.View.UI.StoreViews.StoreView;

namespace StoreApp.View.UI.CashViews
{
    /// <summary>
    /// Логика взаимодействия для EditTabItemWindow.xaml
    /// </summary>
    public partial class EditTabItemWindow : Window
    {
        ITabControlService tabControlService = new TabControlService();
        CashView Cashview;
        MyTextBlock myTextBlock;

        public EditTabItemWindow(CashView cashView)
        {
            InitializeComponent();

            Cashview = cashView;
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            TabController tabController = new TabController
            {
                Id = myTextBlock.TotalInfo.Id,
                Name = txtName.Text.Trim()
            };

            await tabControlService.Update(tabController);

            Cashview.WindowLoad();

            this.Close();

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var items = await tabControlService.GetAll();

            foreach (var item in items)
            {

                Border border = new Border
                {
                    Background = Brushes.White,
                    Width = 150,
                    Height = 40,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(10, 10, 0, 0),
                    CornerRadius = new CornerRadius(10),
                    Padding = new Thickness(0)
                    
                };
                //border.MouseUp += new MouseButtonEventHandler(btnBorder_Click);


                MyTextBlock txtName = new MyTextBlock
                {
                    Background = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 0, 0, 0),
                    Text = item.Name,
                    TextWrapping = TextWrapping.Wrap,
                    FontWeight = FontWeights.Bold,
                    TextAlignment = TextAlignment.Center,
                    FontSize = 20,
                    Width = 140,
                    TotalInfo = new TotalInfo { Id = item.Id, Name = item.Name }
                };

                MyButton myButton = new MyButton
                {
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Transparent,
                    Content = txtName,
                };
                myButton.Click += new RoutedEventHandler(MyButton_Click);


                border.Child = myButton;

                panel.Children.Add(border);

            }
        }

        private void MyButton_Click(object sender, RoutedEventArgs e)
        {
            MyButton myButton = sender as MyButton;

            txtName.Text = (myButton.Content as MyTextBlock).Text;

            myTextBlock = myButton.Content as MyTextBlock;
        }

        private void btnBorder_Click(object sender, MouseButtonEventArgs e)
        {
            //Border border = sender as Border;

            //txtName.Text = (border.Child as MyTextBlock).Text;

            //myTextBlock = border.Child as MyTextBlock;
        }
    }
}
