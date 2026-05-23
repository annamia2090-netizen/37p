using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.IO;


namespace пр37
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    namespace пр37
    {
        public partial class MainWindow : Window
        {
            public MainWindow()
            {
                InitializeComponent();
                rbAssistant.IsChecked = true;
            }

            private void rbPosition_Checked(object sender, RoutedEventArgs e)
            {
                RadioButton selected = sender as RadioButton;
                if (selected != null)
                {
                    string position = selected.Content.ToString().Split('(')[0].Trim();
                    decimal salary = SalaryCalculator.GetSalaryByPosition(position);
                    tbSalary.Text = salary.ToString("F2");
                }
            }

            private void btnCalculate_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text))
                    {
                        MessageBox.Show("Введите фамилию и имя сотрудника!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    decimal salary = decimal.Parse(tbSalary.Text);
                    bool isDocent = chkDocent.IsChecked ?? false;
                    bool isHarm = chkHarm.IsChecked ?? false;
                    bool isCabinet = chkCabinet.IsChecked ?? false;
                    bool isNational = chkNational.IsChecked ?? false;
                    bool isCurator = chkCurator.IsChecked ?? false;
                    bool isHonored = chkHonored.IsChecked ?? false;
                    bool isCandidate = chkCandidate.IsChecked ?? false;
                    bool isDoctor = chkDoctor.IsChecked ?? false;
                    bool isMethodLit = chkMethodLit.IsChecked ?? false;
                    decimal allowances = SalaryCalculator.CalculateAllowances(salary, isDocent, isHarm, isCabinet, isNational, isCurator, isHonored, isCandidate, isDoctor, isMethodLit);
                    var result = SalaryCalculator.CalculateAll(salary, allowances);
                    tbAccrual.Text = result.accrual.ToString("F2");
                    tbUral.Text = result.ural.ToString("F2");
                    tbTax.Text = result.tax.ToString("F2");
                    tbToPay.Text = result.toPay.ToString("F2");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при расчете: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void btnSave_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(tbToPay.Text) || tbToPay.Text == "0.00")
                    {
                        MessageBox.Show("Сначала выполните расчет заработной платы!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
                    saveDialog.Filter = "Текстовые файлы|*.txt|CSV файлы|*.csv|Все файлы|*.*";
                    saveDialog.Title = "Сохранение ведомости";
                    saveDialog.DefaultExt = "txt";
                    saveDialog.FileName = $"Ведомость_{DateTime.Now:yyyyMMdd_HHmmss}";

                    if (saveDialog.ShowDialog() == true)
                    {
                        string fio = $"{txtLastName.Text} {txtFirstName.Text} {txtMiddleName.Text}";
                        string position = "";
                        if (rbAssistant.IsChecked == true) position = "Ассистент";
                        else if (rbTeacher.IsChecked == true) position = "Преподаватель";
                        else if (rbSeniorTeacher.IsChecked == true) position = "Старший преподаватель";
                        else if (rbDocent.IsChecked == true) position = "Доцент";
                        else if (rbProfessor.IsChecked == true) position = "Профессор";
                        else if (rbHeadOfDepartment.IsChecked == true) position = "Заведующий кафедрой";
                        else if (rbDean.IsChecked == true) position = "Декан факультета";
                        string data = $@"===========================================
                        ВЕДОМОСТЬ НАЧИСЛЕНИЯ ЗАРПЛАТЫ
                        Дата: {DateTime.Now:dd.MM.yyyy HH:mm:ss}
                        ===========================================
                        Сотрудник: {fio}
                        Должность: {position}
                        Оклад: {tbSalary.Text} руб.
                        Начисление: {tbAccrual.Text} руб.
                        Уральский коэффициент (15%): {tbUral.Text} руб.
                        Подоходный налог (13%): {tbTax.Text} руб.
                        К ВЫДАЧЕ: {tbToPay.Text} руб.
                        ===========================================";
                        File.WriteAllText(saveDialog.FileName, data);
                        MessageBox.Show($"Ведомость успешно сохранена!\nФайл: {saveDialog.FileName}", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void btnClear_Click(object sender, RoutedEventArgs e)
            {
                txtLastName.Clear();
                txtFirstName.Clear();
                txtMiddleName.Clear();
                tbSalary.Clear();
                tbAccrual.Clear();
                tbUral.Clear();
                tbTax.Clear();
                tbToPay.Clear();
                chkDocent.IsChecked = false;
                chkHarm.IsChecked = false;
                chkCabinet.IsChecked = false;
                chkNational.IsChecked = false;
                chkCurator.IsChecked = false;
                chkHonored.IsChecked = false;
                chkCandidate.IsChecked = false;
                chkDoctor.IsChecked = false;
                chkMethodLit.IsChecked = false;
                rbAssistant.IsChecked = true;
            }

            private void btnExit_Click(object sender, RoutedEventArgs e)
            {
                MessageBoxResult result = MessageBox.Show("Вы действительно хотите выйти?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes) this.Close();
            }
        }
    }
}
