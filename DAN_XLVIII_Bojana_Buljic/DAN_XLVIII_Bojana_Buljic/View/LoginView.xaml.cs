using DAN_XLVIII_Bojana_Buljic.ViewModel;
using System.Windows;

namespace DAN_XLVIII_Bojana_Buljic.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel(this);
        }
    }
}
