using DAN_XLVIII_Bojana_Buljic.ViewModel;
using System.Windows;

namespace DAN_XLVIII_Bojana_Buljic.View
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : Window
    {
        public EmployeeView()
        {
            InitializeComponent();
            this.DataContext = new EmployeeViewModel(this);
        }
    }
}
