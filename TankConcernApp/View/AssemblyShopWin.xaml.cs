using Microsoft.EntityFrameworkCore;
using System.Windows;
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
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке заказов: {ex.Message}");
            }
        }

        private bool EnoughInStorage(long productId, long count)
        {
            try
            {
                var requiredParts = _dbContext.TankParts
                    .Where(p => p.ProductId == productId)
                    .ToList();

                if (!requiredParts.Any())
                {
                    MessageBox.Show("Для выбранного продукта не указаны необходимые запчасти. Невозможно принять заказ.");
                    return false;
                }
                else
                {
                    var inventory = _dbContext.PartsInventories
                        .Include(p => p.TankPart)
                        .ToList();

                    foreach (var part in requiredParts)
                    {
                        var inventoryItem = inventory.FirstOrDefault(i => i.TankPartId == part.TankPartId);
                        if (inventoryItem == null || inventoryItem.Count < count)
                        {
                            MessageBox.Show("Недостаточно запчастей на складе!");
                            return false;
                        }
                    }

                    foreach (var part in requiredParts)
                    {
                        var inventoryItem = inventory.First(i => i.TankPartId == part.TankPartId);
                        inventoryItem.Count -= count;
                        inventoryItem.LastUpdate = DateOnly.FromDateTime(DateTime.Now);
                    }

                    _dbContext.SaveChanges();
                    MessageBox.Show("Запчасти успешно списаны!");
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при списании деталей: {ex.Message}");
                return false;
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
                        if (EnoughInStorage(order.ProductId, order.Count))
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
                        else
                        {
                            return;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите заказ!");
                }
            }
            catch (Exception ex)
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
