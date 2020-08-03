using DAN_XLVIII_Bojana_Buljic.ViewModel;
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

namespace DAN_XLVIII_Bojana_Buljic.View
{
    /// <summary>
    /// Interaction logic for GuestView.xaml
    /// </summary>
    public partial class GuestView : Window
    {
        public GuestView(string username)
        {
            InitializeComponent();
            this.DataContext = new GuestViewModel(this, username);
        }
    }
}
