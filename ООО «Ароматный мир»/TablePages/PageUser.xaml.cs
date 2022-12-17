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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ООО__Ароматный_мир_.Data;

namespace ООО__Ароматный_мир_.TablePages
{
    /// <summary>
    /// Логика взаимодействия для PageUser.xaml
    /// </summary>
    public partial class PageUser : Page
    {
        int maxCount = 0;
        public PageUser()
        {
            InitializeComponent(); var allUsers = Entities.GetContext().User.ToList();
            //var queryBook1 = from p in allUsers
            //                 where p.UserID == MainPages.LoginPage.UserId
            //                 select p;
            //var us = queryBook1.ToList().First();
            //tbName.Text = "ФИО: " + us.UserSurname + " " + us.UserName + " " + us.UserPatronymic;
            UpdateProd();
        }
        private void UpdateProd()
        {
            var currentProd = Entities.GetContext().Product.ToList();


            currentProd = currentProd.Where(a => a.ProductName.ToLower().Contains(tbSearch.Text.ToLower())).ToList();

            Lview.ItemsSource = currentProd;
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    currentProd = currentProd.OrderByDescending(x => x.ProductCost).ToList();
                    break;
                case 1:
                    currentProd = currentProd.OrderBy(x => x.ProductCost).ToList();
                    break;
            }
            switch (cbSkidka.SelectedIndex)
            {
                case 1:
                    currentProd = currentProd.Where(a => a.ProductDiscountAmount > 0 && a.ProductDiscountAmount < 10).ToList();
                    break;
                case 2:
                    currentProd = currentProd.Where(a => a.ProductDiscountAmount >= 10 && a.ProductDiscountAmount < 15).ToList();
                    break;
                case 3:
                    currentProd = currentProd.Where(a => a.ProductDiscountAmount >= 15).ToList();
                    break;
            }
            Lview.ItemsSource = currentProd;
            tbCount.Text = Lview.Items.Count.ToString() + "/" + maxCount.ToString();
        }
        private void BtnClick_Add(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new AddPages.PageAddProduct());
        }

        private void BtnClick_Delete(object sender, RoutedEventArgs e)
        {
            var prodForRemoving = Lview.SelectedItems.Cast<Product>().ToList();
            try
            {
                Entities.GetContext().Product.RemoveRange(prodForRemoving);
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены");
                Lview.ItemsSource = Entities.GetContext().Product.ToList();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка");
            }

        }

        private void CbSort_change(object sender, SelectionChangedEventArgs e)
        {
            UpdateProd();
        }

        private void tbSearch_change(object sender, RoutedEventArgs e)
        {
            UpdateProd();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                maxCount = Lview.Items.Count;
                tbCount.Text = Lview.Items.Count.ToString() + "/" + maxCount.ToString();
                Lview.ItemsSource = Entities.GetContext().Product.ToList();
            }
        }

        private void Page_IsVisibleChanged_1(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                maxCount = Lview.Items.Count;
                tbCount.Text = Lview.Items.Count.ToString() + "/" + maxCount.ToString();
                Lview.ItemsSource = Entities.GetContext().Product.ToList();
            }
        }
    }
}
