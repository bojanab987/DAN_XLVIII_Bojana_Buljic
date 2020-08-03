using DAN_XLVIII_Bojana_Buljic.Commands;
using DAN_XLVIII_Bojana_Buljic.Model;
using DAN_XLVIII_Bojana_Buljic.Services;
using DAN_XLVIII_Bojana_Buljic.View;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLVIII_Bojana_Buljic.ViewModel
{
    class EmployeeViewModel:ViewModelBase
    {
        EmployeeView employeeView;
        OrderService orderService = new OrderService();

        #region Constructor
        public EmployeeViewModel(EmployeeView employeeView)
        {
            this.employeeView = employeeView;
            OrderList = orderService.GetAllOrders();
        }
        #endregion

        #region Properties
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

        private List<vwOrder> orderList;
        public List<vwOrder> OrderList
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
        #endregion      

        #region Commands
        /// <summary>
        /// Delete order command
        /// </summary>
        private ICommand deleteOrder;
        public ICommand DeleteOrder
        {
            get
            {
                if (deleteOrder == null)
                {
                    deleteOrder = new RelayCommand(param => DeleteOrderExecute(), param => CanDeleteOrderExecute());
                }
                return deleteOrder;
            }
        }

        /// <summary>
        /// Command for approving order
        /// </summary>
        private ICommand approveOrder;
        public ICommand ApproveOrder
        {
            get
            {
                if (approveOrder == null)
                {
                    approveOrder = new RelayCommand(param => ApproveOrderExecute(), param => CanApproveOrderExecute());
                }
                return approveOrder;
            }
        }

        /// <summary>
        /// Command for denying/rejecting order
        /// </summary>
        private ICommand denyOrder;
        public ICommand DenyOrder
        {
            get
            {
                if (denyOrder == null)
                {
                    denyOrder = new RelayCommand(param => DenyOrderExecute(), param => CanDenyOrderExecute());
                }
                return denyOrder;
            }
        }

        /// <summary>
        /// Method  for deleting command execution and deleting order from db
        /// </summary>
        public void DeleteOrderExecute()
        {
            try
            {
                orderService.CancelOrder(Ordered.OrderId);
                //update a list of orders
                OrderList = orderService.GetAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if order is selected and status of order. Checks if delete order command is possible to be executed
        /// </summary>
        /// <returns>True if status different from on hold, false if not.</returns>
        public bool CanDeleteOrderExecute()
        {
            try
            {
                if (Ordered != null)
                {
                    if (Ordered.OrderStatus == "pennding")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Method to execute command for approving order.
        /// </summary>
        public void ApproveOrderExecute()
        {
            try
            {
                orderService.ApproveOrder(Ordered);
                //update a list of orders
                OrderList = orderService.GetAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// Checks if order can be approved.
        /// </summary>
        /// <returns>True if can, false if not.</returns>
        public bool CanApproveOrderExecute()
        {
            try
            {
                if (Ordered != null)
                {
                    if (Ordered.OrderStatus == "approved" || Ordered.OrderStatus == "denied")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// This method invokes method for denying order.
        /// </summary>
        public void DenyOrderExecute()
        {
            try
            {
                orderService.DenyOrder(Ordered);
                //update a list of orders
                OrderList = orderService.GetAllOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// This method checks can order be rejected.
        /// </summary>
        /// <returns>True if can, false if not.</returns>
        public bool CanDenyOrderExecute()
        {
            try
            {
                if (Ordered != null)
                {
                    if (Ordered.OrderStatus == "approved" || Ordered.OrderStatus == "denied")
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
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
        /// Method for logging out employee from app
        /// </summary>  
        private void LogOutExecute()
        {
            try
            {
                //closes guest view and opens empty LogInView
                LoginView loginView = new LoginView();
                employeeView.Close();
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
