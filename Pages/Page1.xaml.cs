using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FontAwesome.Sharp;
namespace DFS.DesktopV2.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public List<City> MyItems { get; set; }
        public City SelectedItem;

        public Page1()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender,RoutedEventArgs e)
        {
            // สร้าง ObservableCollection ของเมือง
            MyItems = new List<City>()
            {
                new City("กรุงเทพมหานคร"),
                new City("เชียงใหม่"),
                new City("พัทยา"),
                // เพิ่มเมืองอื่น ๆ ตามต้องการ
            };
            cmb.ItemsSource = MyItems;


            List<User> users = new List<User>();
            users.Add(new User() { Id = 1,Charector = "J",Name = "John Doe",Birthday = new DateTime(1971,7,23) ,Status = "Active"});
            users.Add(new User() { Id = 2,Charector = "J",Name = "Jane Doe",Birthday = new DateTime(1974,1,17) ,Status = "Active"});
            users.Add(new User() { Id = 3,Charector = "S",Name = "Sammy Doe",Birthday = new DateTime(1991,9,2) ,Status = "Active"});
            users.Add(new User() { Id = 4,Charector = "J",Name = "John Doe",Birthday = new DateTime(1971,7,23) ,Status = "Inactive" });
            users.Add(new User() { Id = 5,Charector = "J",Name = "Jane Doe",Birthday = new DateTime(1974,1,17) ,Status = "Active"});
            users.Add(new User() { Id = 6,Charector = "N",Name = "Nuchit Khamsim",Birthday = new DateTime(1991,9,2),Status = "Completed" });
            users.Add(new User() { Id = 7,Charector = "J",Name = "John Doe",Birthday = new DateTime(1971,7,23),Status = "Inactive" });
            users.Add(new User() { Id = 8,Charector = "J",Name = "Jane Doe",Birthday = new DateTime(1974,1,17),Status = "Inactive" });
            users.Add(new User() { Id = 9,Charector = "S",Name = "Sammy Doe",Birthday = new DateTime(1991,9,2),Status = "Active" });


            dgvProduct.ItemsSource = users;
        }
        private bool isPrimary = true;
        private void btnWeight_Click(object sender,RoutedEventArgs e)
        {
            int rowIndex = 5;

            var item = dgvProduct.Items.GetItemAt(rowIndex);
            dgvProduct.SelectedItem = item;
            dgvProduct.ScrollIntoView(item);
            //var row = (DataGridRow)dgvProduct.ItemContainerGenerator.ContainerFromIndex(5);
            //row.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));

            DataGridRow row = GetDataGridRow(rowIndex);
            if(row != null)
            {
                row.Background = new SolidColorBrush(Colors.LightGreen);
            }


            var button = sender as System.Windows.Controls.Button;
            if(isPrimary)
            {
                button.Style = (Style)FindResource("ButtonSuccessStyle");
                iconBlock.Icon = IconChar.Save;
                textBlock.Text = "บันทึก";
            }
            else
            {
                button.Style = (Style)FindResource("ButtonPrimaryStyle");
                textBlock.Text = "ชั่งน้ำหนัก";
                iconBlock.Icon = IconChar.ScaleBalanced;
            }
            isPrimary = !isPrimary;

            // Find the StackPanel inside the button's content
            //var stackPanel = button.Content as StackPanel;
            //if(stackPanel != null)
            //{
            //    // Find the IconBlock inside the StackPanel
            //    var iconBlock = FindChild<IconBlock>(stackPanel);
            //    if(iconBlock != null)
            //    {
            //        // Toggle the icon
            //        iconBlock.Icon = isPrimary ? IconChar.Save : IconChar.ScaleBalanced;
            //        isPrimary = !isPrimary;
            //    }
            //}
        }
        private DataGridRow GetDataGridRow(int index)
        {
            DataGridRow row = (DataGridRow)dgvProduct.ItemContainerGenerator.ContainerFromIndex(index);
            if(row == null)
            {
                dgvProduct.UpdateLayout();
                dgvProduct.ScrollIntoView(dgvProduct.Items[index]);
                row = (DataGridRow)dgvProduct.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if(current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while(current != null);
            return null;
        }

        // Helper method to find a child element of a specific type
        private T FindChild<T>(DependencyObject parent) where T : DependencyObject
        {
            if(parent == null)
                return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for(int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent,i);
                var childType = child as T;
                if(childType == null)
                {
                    foundChild = FindChild<T>(child);
                    if(foundChild != null)
                        break;
                }
                else
                {
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        private void Button_Click(object sender,RoutedEventArgs e)
        {
            // Get the button that was clicked
            Button button = sender as Button;

            // Get the DataGridRow containing the button
            DataGridRow row = FindAncestor<DataGridRow>(button);

            // Get the data item associated with the row
            User user = row?.Item as User;

            if(user != null)
            {
                MessageBox.Show($"Button clicked for {user.Name}, Age: {user.Birthday}");
            }
        }
    }
    public class City
    {
        public string Name { get; set; }

        public City(string name)
        {
            Name = name;
        }
    }
    public class User
    {
        public int Id { get; set; }

        public string Charector { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }
        public string Status { get; set; }
    }
}
