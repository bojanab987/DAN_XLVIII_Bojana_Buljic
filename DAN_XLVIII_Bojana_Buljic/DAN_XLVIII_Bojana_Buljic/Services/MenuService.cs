using DAN_XLVIII_Bojana_Buljic.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DAN_XLVIII_Bojana_Buljic.Services
{
    class MenuService
    {
        /// <summary>
        /// Method gets all pizzas from menu from database and returns list
        /// </summary>
        /// <returns>List of pizzas</returns>
        public List<vwMenu> GetMenu()
        {
            try
            {
                using (PizzeriaEntities context = new PizzeriaEntities())
                {
                    List<vwMenu> menu = new List<vwMenu>();
                    menu = (from x in context.vwMenus select x).ToList();
                    return menu;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception" + ex.Message.ToString());
                return null;
            }
        }
    }
}
