using Microsoft.EntityFrameworkCore;
using System.Windows;
using TankConcernApp.database;
using TankConcernApp.Model;

namespace TankConcernApp.View
{
    public partial class InventoryManagerWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        public InventoryManagerWin()
        {
            InitializeComponent();
            LoadInventory();
        }

        private void LoadInventory()
        {
            try
            {
                var inventory = _dbContext.PartsInventories
                    .Include(p => p.PartType)
                    .Include(p => p.TankPart)
                    .ToList();
                DGPartsInventory.ItemsSource = inventory;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке инвентаря: {ex.Message}");
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGPartsInventory.SelectedItem is PartsInventory selectedInventory)
                {
                    if (long.TryParse(TxtBox_Count.Text, out var addCount) && addCount > 0)
                    {
                        selectedInventory.Count += addCount;
                        selectedInventory.LastUpdate = DateOnly.FromDateTime(DateTime.Now);
                        _dbContext.SaveChanges();
                        MessageBox.Show("Склад успешно пополнен!");
                        LoadInventory();
                    }
                    else
                    {
                        MessageBox.Show("Введите корректное положительное число для пополнения!");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите запчасть для пополнения!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при пополнении склада: {ex.Message}");
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
