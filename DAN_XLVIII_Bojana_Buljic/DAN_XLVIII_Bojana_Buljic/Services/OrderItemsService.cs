using DAN_XLVIII_Bojana_Buljic.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_XLVIII_Bojana_Buljic.Services
{
    /// <summary>
    /// Service class for adding, removing items from OrderItem table from database
    /// </summary>
    class OrderItemsService
    {
        /// <summary>
        /// Method create list of guest ordered items.
        /// </summary>
        /// <param name="username">Username of guest.</param>
        /// <returns>List of ordered item.</returns>
        public List<vwOrderItem> GetAllOrderedItems(string username)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    return context.vwOrderItems.Where(x => x.JMBG == username).ToList();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Method for adding ordered item to OrderItem table and saves changes into database.
        /// </summary>
        /// <param name="menuItem">Item from menu.</param>
        /// <param name="order">Which order to add item.</param>
        /// <param name="quantity">Quatity of item.</param>
        public void AddOrderItem(vwMenu menuItem, vwOrder order, int quantity)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrderItem itemToAdd = new tblOrderItem
                    {
                        ItemId = menuItem.ItemId,
                        Quantity = quantity,
                        OrderID = order.OrderId
                    };
                    context.tblOrderItems.Add(itemToAdd);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
        /// <summary>
        /// Method for deleting ordered item and saves changes to database.
        /// </summary>
        /// <param name="id">ID of order</param>
        public void RemoveItem(int id)
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    tblOrderItem itemToDelete = context.tblOrderItems.Where(x => x.ID == id).FirstOrDefault();
                    context.tblOrderItems.Remove(itemToDelete);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
            }
        }
    }
}
