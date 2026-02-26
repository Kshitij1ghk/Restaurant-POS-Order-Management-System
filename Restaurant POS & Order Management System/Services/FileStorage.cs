using Restaurant_POS___Order_Management_System.Interfaces;
using Restaurant_POS___Order_Management_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Services
{
    public class FileStorage:IStorage
    {
        private const string MenuItemsFile = "menuitems.csv";
        private const string OrdersFile = "orders.csv";
        private const string OrderItemsFile = "orderitems.csv";
        private const string TablesFile = "tables.csv";
        private const string StaffFile = "staffs.csv";
        private const string RecieptFile = "reciepts.csv";

        public void SaveMenuItems(Dictionary<int,MenuItem> menuitems)
        {
            List<string> lines = new List<string>();
            lines.Add("MenuItemId,Name,Price,MenuCategory,FoodCategory,description,IsAvailable");
            foreach(MenuItem menuitem in menuitems.Values)
            {
                //basic Csv PROTECTION(to replace commas in description)
                string SafeDescription = menuitem.Description.Replace(","," ");
                string line = menuitem.MenuItemId + "," +
                    menuitem.Name + "," +
                    menuitem.Price + "," +
                    menuitem.MenuCategory + "," +
                    menuitem.FoodCategory + "," +
                    menuitem.Description + "," +
                    menuitem.IsAvailable;
                lines.Add(line);
            }
            File.WriteAllLines(MenuItemsFile, lines);
            
        }
        public Dictionary<int, MenuItem> LoadMenuItems()
        {
            Dictionary<int,MenuItem> menuitems= new Dictionary<int,MenuItem>();
            if (!File.Exists(MenuItemsFile))
                return menuitems;
            string[] lines=File.ReadAllLines(MenuItemsFile);
            for(int i=0;i<lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int menuItemId = int.Parse(parts[0]);
                    string name = parts[1];
                    int price = int.Parse(parts[2]);
                    MenuCategory menuCategory = (MenuCategory)Enum.Parse(typeof(MenuCategory), parts[3]);
                    FoodCategory foodCategory = (FoodCategory)Enum.Parse(typeof(FoodCategory), parts[4]);
                    string description = parts[5];
                    bool isAvailable = bool.Parse(parts[6]);

                    MenuItem menuItem = new MenuItem(menuItemId, name, price, menuCategory, foodCategory, description,isAvailable);
                    menuitems.Add(menuItemId,menuItem);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return menuitems;
        }
    }
}
