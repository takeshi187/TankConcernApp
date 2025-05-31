using Microsoft.EntityFrameworkCore;
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
    public partial class AssemblyShopWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        private readonly long _WorkshopId;
        public AssemblyShopWin(long workshopId)
        {
            InitializeComponent();
            _WorkshopId = workshopId;
            LoadOrders();
            TxtBox_WorkshoId.Text = _WorkshopId.ToString();
        }

        private void LoadOrders()
        {
            try
            {
                var orders = _dbContext.Orders
                    .Where(o => o.OrderStatusId == 1)
                    .Include(o => o.OrderStatus)
                    .Include(o => o.Customer)
                    .Include(o => o.Product)
                    .Select(o => new OrderDisplay
                    {
                        OrderId = o.OrderId,
                        CustomerName = o.Customer.CustomerName,
                        ProductName = o.Product.ProductName,
                        OrderDate = o.OrderDate,
                        Count = o.Count,
                        TotalPrice = o.TotalPrice,
                        OrderStatusName = o.OrderStatus.OrderStatusName
                    }).ToList();

                DGOrders.ItemsSource = orders;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заказов: {ex.Message}");
            }
        }

        private void Btn_AcceptOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGOrders.SelectedItem is OrderDisplay selectedOrder)
                {
                    var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == selectedOrder.OrderId);
                    if (order != null)
                    {
                        order.OrderStatusId = 2;

                        var productStage = new ProductStage
                        {
                            OrderId = order.OrderId,
                            WorkshopId = _WorkshopId,
                            ProductStageTypeId = 1,
                        };

                        _dbContext.ProductStages.Add(productStage);
                        _dbContext.SaveChanges();

                        MessageBox.Show("Заказ принят и стадия продукта создана!");
                        LoadOrders();
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите заказ!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ошибка при принятии заказа: {ex.Message}");
            }            
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
