using DAN_XLVIII_Bojana_Buljic.Commands;
using DAN_XLVIII_Bojana_Buljic.Model;
using DAN_XLVIII_Bojana_Buljic.Services;
using DAN_XLVIII_Bojana_Buljic.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLVIII_Bojana_Buljic.ViewModel
{
    class GuestViewModel:ViewModelBase
    {
        GuestView guestView;
        MenuService menuService = new MenuService();
        OrderService orderService = new OrderService();
        OrderItemsService oiService = new OrderItemsService();
        private string username;
        BackgroundWorker background=new BackgroundWorker();

        #region Constructor      

        public GuestViewModel(GuestView guestViewOpen, string username)
        {
            guestView = guestViewOpen;
            MenuList = menuService.GetMenu();
            Username = username;            
            background.DoWork += CheckStatus;

            //check if guest already has an order
            if(orderService.CheckIfUserOrdered(username)==false)
            {                
                orderService.AddOrder(Username);
                Ordered = orderService.ViewOrder(Username);
                totalPrice = Ordered.TotalPrice;
            }
            else
            {
                IsVisibleMenu = Visibility.Hidden;
                OrderList = oiService.GetAllOrderedItems(Username);
                IsVisibleOrderStatus = Visibility.Visible;
                IsConfirmed = Visibility.Hidden;
                Ordered = orderService.ViewOrder(Username);
                totalPrice = Ordered.TotalPrice;
                background.RunWorkerAsync();
            }

        }

        #endregion

        #region Method
        /// <summary>
        /// This method is handler for backgroundWorker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CheckStatus(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (Ordered.OrderStatus != "pennding")
                {
                    Thread.Sleep(2000);
                    IsVisibleMenu = Visibility.Visible;
                    IsConfirmed = Visibility.Visible;
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Username property, represents guests jmbg
        /// </summary>
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        /// <summary>
        /// Single menu item property
        /// </summary>
        private vwMenu menuItem;
        public vwMenu MenuItem
        {
            get
            {
                return menuItem;
            }
            set
            {
                menuItem = value;
                OnPropertyChanged("MenuItem");
            }
        }

        /// <summary>
        /// Whole menu list
        /// </summary>
        private List<vwMenu> menuList;
        public List<vwMenu> MenuList
        {
            get
            {
                return menuList;
            }
            set
            {
                menuList = value;
                OnPropertyChanged("MenuList");
            }
        }

        /// <summary>
        /// View of ordered items
        /// </summary>
        private vwOrder ordered;
        public vwOrder Ordered
        {
            get
            {
                return ordered;
            }
            set
            {
                ordered = value;
                OnPropertyChanged("Ordered");
            }
        }
        
        private vwOrderItem orderItem;
        public vwOrderItem OrderItem
        {
            get
            {
                return orderItem;
            }
            set
            {
                orderItem = value;
                OnPropertyChanged("OrderItem");
            }
        }

        /// <summary>
        /// List of ordered items
        /// </summary>
        private List<vwOrderItem> orderList;
        public List<vwOrderItem> OrderList
        {
            get
            {
                return orderList;
            }
            set
            {
                orderList = value;
                OnPropertyChanged("OrderList");
            }
        }

        private int quantity;
        public int Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                quantity = value;
                OnPropertyChanged("Quantity");
            }
        }        

        /// <summary>
        /// Menu visibility
        /// </summary>
        private Visibility isVisibleMenu;
        public Visibility IsVisibleMenu
        {
            get
            {
                return isVisibleMenu;
            }
            set
            {
                isVisibleMenu = value;
                OnPropertyChanged("IsVisibleMenu");
            }
        }

        /// <summary>
        /// Order status visibility
        /// </summary>
        private Visibility isVisibleOrderStatus = Visibility.Hidden;
        public Visibility IsVisibleOrderStatus
        {
            get
            {
                return isVisibleOrderStatus;
            }
            set
            {
                isVisibleOrderStatus = value;
                OnPropertyChanged("IsVisibleOrderStatus");
            }
        }

        private int totalPrice;
        public int TotalPrice
        {
            get
            {
                return totalPrice;
            }
            set
            {
                totalPrice = value;
                OnPropertyChanged("TotalPrice");
            }
        }

        private Visibility isConfirmed;
        public Visibility IsConfirmed
        {
            get
            {
                return isConfirmed;
            }
            set
            {
                isConfirmed = value;
                OnPropertyChanged("IsConfirmed");
            }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Command for deleting ordered item
        /// </summary>
        private ICommand deleteItem;
        public ICommand DeleteItem
        {
            get
            {
                if (deleteItem == null)
                {
                    deleteItem = new RelayCommand(param => DeleteItemExecute(), param => CanDeleteItemExecute());
                }
                return deleteItem;
            }
        }

        /// <summary>
        /// Method for executing command of deleting item
        /// </summary>
        public void DeleteItemExecute()
        {
            try
            {
                if (MenuItem != null)
                {
                    //deleting item
                    oiService.RemoveItem(OrderItem.ID);
                    //update list of order
                    OrderList = oiService.GetAllOrderedItems(Username);
                    TotalPrice = orderService.CalculateTotalPrice(Ordered.OrderId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method for confirming delete command can be executed
        /// </summary>
        /// <returns></returns>
        public bool CanDeleteItemExecute()
        {
            return true;
        }

        /// <summary>
        /// Command for adding item in order
        /// </summary>
        private ICommand addItem;
        public ICommand AddItem
        {
            get
            {
                if (addItem == null)
                {
                    addItem = new RelayCommand(param => AddItemExecute(), param => CanAddItemExecute());
                }
                return addItem;
            }
        }

        /// <summary>
        /// Method for executing command of adding item to order.
        /// </summary>
        public void AddItemExecute()
        {
            try
            {
                oiService.AddOrderItem(MenuItem, Ordered, Quantity);
                //update a list of ordered items
                OrderList = oiService.GetAllOrderedItems(Username);
                TotalPrice = orderService.CalculateTotalPrice(Ordered.OrderId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method Checks if user if there is appropriate quantity input.
        /// </summary>
        /// <returns>True if valid, false if not.</returns>
        public bool CanAddItemExecute()
        {
            if (Int32.TryParse(Quantity.ToString(), out int number) && number > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Command for order canceling
        /// </summary>
        private ICommand cancelOrder;
        public ICommand CancelOrder
        {
            get
            {
                if (cancelOrder == null)
                {
                    cancelOrder = new RelayCommand(param => CancelOrderExecute(), param => CanCancelOrderExecute());
                }
                return cancelOrder;
            }
        }

        /// <summary>
        /// Method for canceling order command execution
        /// </summary>
        public void CancelOrderExecute()
        {
            try
            {
                orderService.CancelOrder(Ordered.OrderId);
                OrderList = oiService.GetAllOrderedItems(username);
                guestView.Close();
                LoginView newLogin = new LoginView();
                newLogin.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Confirms execution of command
        /// </summary>
        /// <returns></returns>
        public bool CanCancelOrderExecute()
        {
            return true;
        }

        /// <summary>
        /// Command for confirming order
        /// </summary>
        private ICommand confirmOrder;
        public ICommand ConfirmOrder
        {
            get
            {
                if (confirmOrder == null)
                {
                    confirmOrder = new RelayCommand(param => ConfirmOrderExecute(), param => CanConfirmOrderExecute());
                }
                return confirmOrder;
            }
        }

        /// <summary>
        /// This method invokes method to confirm order.
        /// </summary>
        public void ConfirmOrderExecute()
        {
            try
            {
                orderService.ConfirmOrder(Ordered);
                IsVisibleMenu = Visibility.Hidden;
                IsVisibleOrderStatus = Visibility.Visible;
                IsConfirmed = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method to check if execution of confirm command is possible
        /// </summary>
        /// <returns></returns>
        public bool CanConfirmOrderExecute()
        {
            //if there is any item in order list execution of confirm is true
            if (OrderList != null)
            {
                if (OrderList.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// LogOut Command
        /// </summary>
        private ICommand logOut;
        public ICommand LogOut
        {
            get
            {
                if (logOut == null)
                {
                    logOut = new RelayCommand(param => LogOutExecute(), param => CanLogOutExecute());
                }
                return logOut;
            }
        }

        /// <summary>
        /// Method for logging out guest from app
        /// </summary>  
        private void LogOutExecute()
        {
            try
            {
                //closes guest view and opens empty LogInView
                LoginView loginView = new LoginView();
                guestView.Close();
                loginView.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Method check if logout is possible to be Executed
        /// </summary>
        private bool CanLogOutExecute()
        {
            return true;
        }

        #endregion

    }
}
