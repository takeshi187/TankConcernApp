using System.Windows;
using TankConcernApp.database;
using TankConcernApp.Model;
using TankConcernApp.View;

namespace TankConcernApp
{
    public partial class LogInWin : Window
    {
        private readonly TankConcernDbContext _dbContext = new TankConcernDbContext();
        public LogInWin()
        {
            InitializeComponent();
        }

        private void Btn_Entry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = TxtBox_Login.Text.Trim();
                string password = PassBox_Password.Password.Trim();

                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                {
                    MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var user = _dbContext.Users.FirstOrDefault(u => u.Login == username && u.Password == password);

                if (user == null)
                {
                    MessageBox.Show("Неверный логин или пароль!");
                    return;
                }

                if (user != null)
                {
                    var roleId = user.RoleId;
                    var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == user.EmployeeId);
                    if (employee.EmployeeStatusId == 3)
                        MessageBox.Show("Вы уволены и не можете авторизоваться в системе!");
                    else
                    {
                        user.LastLogin = DateOnly.FromDateTime(DateTime.Now);
                        _dbContext.SaveChanges();
                        switch (roleId)
                        {
                            case 1:
                                MessageBox.Show($"Добро пожаловать! Администратор: {employee.LastName}");
                                AdminWin adminWin = new AdminWin();
                                adminWin.Show();
                                break;
                            case 2:
                                MessageBox.Show($"Добро пожаловать! Главный бригадир: {employee.LastName}");
                                BrigadierMainWin brigadierMainWin = new BrigadierMainWin();
                                brigadierMainWin.Show();
                                break;
                            case 3:
                                CheckHeadOfWorkshop(user);
                                break;
                            case 4:
                                CheckProdManager(user);
                                break;
                            case 5:
                                MessageBox.Show($"Добро пожаловать! Менеджер по поставкам: {employee.LastName}");
                                InventoryManagerWin inventoryManagerWin = new InventoryManagerWin();
                                inventoryManagerWin.Show();
                                break;
                            case 6:
                                MessageBox.Show($"Добро пожаловать! Сотрудник бригады: {employee.LastName}");
                                EmployeeWin employeeWin = new EmployeeWin(employee.EmployeeId);
                                employeeWin.Show();
                                break;
                            default:
                                MessageBox.Show("Такой роли не существует!");
                                break;
                        }
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при авторизации: ", ex.Message);
            }
        }

        private void CheckHeadOfWorkshop(User user)
        {
            try
            {
                var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == user.EmployeeId);
                var brigadeId = _dbContext.EmployeeBrigades
                    .Where(b => b.EmployeeId == employee.EmployeeId)
                    .Select(b => b.BrigadeId)
                    .FirstOrDefault();
                var workshopId = _dbContext.BrigadeWorkshopAssignments
                    .Where(w => w.BrigadeId == brigadeId)
                    .Select(w => w.WorkshopId)
                    .FirstOrDefault();
                var workshopType = _dbContext.WorkshopTypes.FirstOrDefault(w => w.WorkshopTypeId == workshopId);
                if (workshopType != null)
                {
                    switch (workshopType.WorkshopTypeId)
                    {
                        case 1:
                            MessageBox.Show($"Добро пожаловать в сборочный цех! Начальник цеха: {employee.LastName}");
                            AssemblyShopWin assemblyShopWin = new AssemblyShopWin(workshopId);
                            assemblyShopWin.Show();
                            this.Close();
                            break;
                        case 2:
                            MessageBox.Show($"Добро пожаловать в покрасочный цех! Начальник цеха: {employee.LastName}");
                            PaintingShopWin paintingShopWin = new PaintingShopWin(workshopId);
                            paintingShopWin.Show();
                            this.Close();
                            break;
                        case 3:
                            MessageBox.Show($"Добро пожаловать в цех тестирования! Начальник цеха: {employee.LastName}");
                            TestingShopWin testingShopWin = new TestingShopWin(workshopId);
                            testingShopWin.Show();
                            this.Close();
                            break;
                        case 4:
                            MessageBox.Show($"Добро пожаловать в складской цех! Начальник цеха: {employee.LastName}");
                            StorageShopWin storageShopWin = new StorageShopWin(workshopId);
                            storageShopWin.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Такой роли не существует!");
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при авторизации workshop");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при авторизации начальника цеха: {ex.Message}");
            }
        }

        private void CheckProdManager(User user)
        {
            try
            {
                var employee = _dbContext.Employees.FirstOrDefault(e => e.EmployeeId == user.EmployeeId);
                var brigadeId = _dbContext.EmployeeBrigades
                    .Where(b => b.EmployeeId == employee.EmployeeId)
                    .Select(b => b.BrigadeId)
                    .FirstOrDefault();
                var workshopId = _dbContext.BrigadeWorkshopAssignments
                   .Where(w => w.BrigadeId == brigadeId)
                   .Select(w => w.WorkshopId)
                   .FirstOrDefault();

                ProdManagerWin prodManagerWin = new ProdManagerWin(employee.EmployeeId, brigadeId, workshopId);
                MessageBox.Show($"Добро пожаловать! Менеджер по производству: {employee.LastName}");
                prodManagerWin.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при авторизации менеджера по производству: {ex.Message}");
            }
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Clean_Click(object sender, RoutedEventArgs e)
        {
            TxtBox_Login.Clear();
            PassBox_Password.Clear();
        }
    }
}
