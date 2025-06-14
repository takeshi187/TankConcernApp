﻿using System.Windows;
using TankConcernApp.database;

namespace TankConcernApp.View
{
    public partial class EditEmployeeStatusWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        public EditEmployeeStatusWin()
        {
            InitializeComponent();
            LoadEmployees();
            LoadStatuses();
        }

        private void LoadEmployees()
        {
            try
            {
                var employees = _dbContext.Employees
                    .Select(e => new
                    {
                        e.EmployeeId,
                        FullName = $"Id: {e.EmployeeId} - {e.FirstName} {e.LastName}"
                    }).ToList();
                ComboBox_Employees.ItemsSource = employees;
                ComboBox_Employees.DisplayMemberPath = "FullName";
                ComboBox_Employees.SelectedValuePath = "EmployeeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке пользователей: {ex.Message}");
            }
        }

        private void LoadStatuses()
        {
            try
            {
                var statuses = _dbContext.EmployeeStatuses
                    .Select(s => new
                    {
                        StatusId = s.EmployeeStatusId,
                        StatusName = s.EmployeeStatusName
                    }).ToList();
                ComboBox_Statuses.ItemsSource = statuses;
                ComboBox_Statuses.DisplayMemberPath = "StatusName";
                ComboBox_Statuses.SelectedValuePath = "StatusId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке статусов: {ex.Message}");
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ComboBox_Employees.SelectedValue is long employeeId &&
                    ComboBox_Statuses.SelectedValue is long statusId)
                {
                    var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                    if (employee != null)
                    {
                        employee.EmployeeStatusId = statusId;
                        _dbContext.SaveChanges();
                        MessageBox.Show("Статус успешно изменён.");
                    }
                }
                else
                {
                    MessageBox.Show("Пожалуйста, выберите сотрудника и статус.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ошибка при изменении статуса пользователя: {ex.Message}");
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
