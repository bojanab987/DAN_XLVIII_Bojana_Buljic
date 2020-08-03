using DAN_XLVIII_Bojana_Buljic.Commands;
using DAN_XLVIII_Bojana_Buljic.View;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DAN_XLVIII_Bojana_Buljic.ViewModel
{
    class LoginViewModel:ViewModelBase
    {
        LoginView logInView;

        public LoginViewModel(LoginView logInOpen)
        {
            logInView = logInOpen;
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        #region Commands
        /// <summary>
        /// LogIn Command
        /// </summary>
        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(LoginExecute, CanLoginExecute);
                }
                return login;
            }
        }

        /// <summary>
        /// Method for deciding which View will open according to logged in Employee credentials
        /// </summary>
        private void LoginExecute(object o)
        {
            try
            {
                string password = (o as PasswordBox).Password;
                if (Username == "Zaposleni" && password == "Zaposleni")
                {
                    EmployeeView employee = new EmployeeView();
                    logInView.Close();
                    employee.ShowDialog();
                }
                else if (Validations.Validation.IsValidJMBG(Username) && password == "Gost")
                {
                    GuestView guest = new GuestView();
                    logInView.Close();
                    guest.ShowDialog();                    
                }
                else
                {
                    MessageBox.Show("Username or password not correct." +
                        "Username must be valid JMBG.");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to confirm logIn command execution
        /// </summary>
        /// <returns></returns>
        private bool CanLoginExecute(object o)
        {
            return true;
        }
        #endregion
    }
}
