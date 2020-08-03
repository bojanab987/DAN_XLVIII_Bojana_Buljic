using DAN_XLVIII_Bojana_Buljic.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_XLVIII_Bojana_Buljic.Services
{
    class OrderService
    {
        /// <summary>
        /// This method adds new order to DbSet and then saves changes to database.
        /// </summary>
        /// <param name="username"></param>
        public void AddOrder(string username)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder order = new tblOrder
                    {
                        OrderDateTime = DateTime.Now,
                        TotalPrice = 0,
                        JMBG = username,
                        OrderStatus = "pennding"
                    };
                    context.tblOrders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());               
            }
        }

        /// <summary>
        /// Method retreiving order based on forwarded Jmbg/username
        /// </summary>
        /// <param name="username">Username of guest</param>
        /// <returns>Order</returns>
        public vwOrder ViewOrder(string username)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    return context.vwOrders.Where(x => x.JMBG == username).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method for calculating total price of order.
        /// </summary>
        /// <param name="orderID">ID of order.</param>
        /// <returns>Total price of order.</returns>
        public int CalculateTotalPrice(int orderID)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder order = context.tblOrders.Where(x => x.OrderId == orderID).FirstOrDefault();
                    List<vwOrderItem> orders = context.vwOrderItems.Where(x => x.OrderID == orderID).ToList();
                    int total = 0;
                    foreach (var item in orders)
                    {
                        total += item.Price * item.Quantity;
                    }
                    order.TotalPrice = total;
                    context.SaveChanges();
                    return total;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return 0;
            }
        }

        /// <summary>
        /// Method is canceling the order and deletes order and every item in that order from DB.
        /// </summary>
        /// <param name="orderID">ID of order.</param>
        public void CancelOrder(int orderID)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder order = context.tblOrders.Where(x => x.OrderId == orderID).FirstOrDefault();
                    List<tblOrderItem> orders = context.tblOrderItems.Where(x => x.OrderID == orderID).ToList();
                    foreach (var item in orders)
                    {
                        context.tblOrderItems.Remove(item);
                        context.SaveChanges();
                    }
                    context.tblOrders.Remove(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// Method for confirming the making of order afterwhich date and time of order is set to now
        /// </summary>
        /// <param name="order">Order.</param>
        public void ConfirmOrder(vwOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder orderToEdit = context.tblOrders.Where(x => x.OrderId == order.OrderId).FirstOrDefault();
                    orderToEdit.OrderDateTime = DateTime.Now;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// This method creates a list of all orders.
        /// </summary>
        /// <returns>List of all orders.</returns>
        public List<vwOrder> GetAllOrders()
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    List<vwOrder> orders = new List<vwOrder>();
                    orders = (from x in context.vwOrders select x).ToList();
                    return orders;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Change order status to approved and saves changes to database.
        /// </summary>
        /// <param name="order">Order for approval.</param>
        public void ApproveOrder(vwOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder orderToApprove = context.tblOrders.Where(x => x.OrderId == order.OrderId).FirstOrDefault();
                    orderToApprove.OrderStatus = "approved";
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// Change order status to denied and saves changes to database.
        /// </summary>
        /// <param name="order"></param>
        public void DenyOrder(vwOrder order)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrder orderToReject = context.tblOrders.Where(x => x.OrderId == order.OrderId).FirstOrDefault();
                    orderToReject.OrderStatus = "denied";
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }

        /// <summary>
        /// This method checks if user already ordered.
        /// </summary>
        /// <param name="username">Username of guest.</param>
        /// <returns>True if ordered, false if not.</returns>
        public bool CheckIfUserOrdered(string username)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    List<vwOrder> orders = context.vwOrders.Where(x => x.JMBG == username).ToList();
                    if (orders.Count() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return false;
            }
        }
    }
}
