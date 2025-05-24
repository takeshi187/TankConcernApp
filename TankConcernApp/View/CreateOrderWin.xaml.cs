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
using TankConcernApp.database;
using TankConcernApp.Model;

namespace TankConcernApp.View
{
    public partial class CreateOrderWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        public CreateOrderWin()
        {
            InitializeComponent();
            LoadCustomers();
            LoadProducts();
        }

        private void LoadCustomers()
        {
            var customers = _dbContext.Customers
                .Select(c => new
                {
                    c.CustomerId,
                    DisplayName = $"Id: {c.CustomerId} - {c.CustomerName}"
                }).ToList();
            ComboBox_Customers.ItemsSource = customers;
            ComboBox_Customers.DisplayMemberPath = "DisplayName";
            ComboBox_Customers.SelectedValuePath = "CustomerId";
        }

        private void LoadProducts()
        {
            var products = _dbContext.Products
                .Select(p => new
                {
                    p.ProductId,
                    DisplayName = $"Id: {p.ProductId} - {p.ProductName}"
                }).ToList();
            ComboBox_Products.ItemsSource = products;
            ComboBox_Products.DisplayMemberPath = "DisplayName";
            ComboBox_Products.SelectedValuePath = "ProductId";
        }

        private void Btn_CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_Customers.SelectedValue is long customerId &&
                ComboBox_Products.SelectedValue is long productId &&
                int.TryParse(TextBox_Count.Text, out int count) &&
                decimal.TryParse(TextBox_Price.Text, out decimal price))
            {
                var order = new Order
                {
                    CustomerId = customerId,
                    ProductId = productId,
                    Count = count,
                    TotalPrice = price,
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    OrderStatusId = 1
                };

                _dbContext.Orders.Add(order);
                _dbContext.SaveChanges();

                MessageBox.Show("Заказ успешно создан!");
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            AdminWin adminWin = new AdminWin();
            adminWin.Show();
            this.Close();
        }
    }
}
