using Restaurant_POS___Order_Management_System.Interfaces;
using Restaurant_POS___Order_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace Restaurant_POS___Order_Management_System.Services
{
    public class OrderService:IOrderService
    {
        private Dictionary<int, MenuItem> menuItems;
        private Dictionary<int, Order> orders;
        private Dictionary<int, List<OrderItem>> orderItems;
        private Dictionary<int, Table> tables;
        private Dictionary<int, Staff> staffs;
        private Dictionary<int,Receipt> receipts;
        private IStorage storage;

        // Constructor - This is called when OrderService is created in Program.cs
        // It loads all existing data from CSV files into memory
        // Think of it as the manager loading all information when restaurant opens for the day
        public OrderService(IStorage storage)
        {
            // Store the storage object so we can use it later to save/load data
            // IStorage is the interface, FileStorage is the actual implementation
            this.storage= storage;
            menuItems=storage.LoadMenuItems();
            orders=storage.LoadOrders();
            orderItems=storage.LoadOrderItems();
            tables=storage.LoadTables();
            staffs=storage.LoadStaff();
            receipts=storage.LoadReceipts();
        }

        // AddMenuItem - Creates a new menu item and adds it to the system
        // 1. Creates a new MenuItem object with the provided details
        // 2. Adds it to the in-memory dictionary for quick access
        // 3. Saves the updated dictionary to CSV so data is not lost when program closes
        public void AddMenuItem(int menuItemId, string name, decimal price, MenuCategory menucategory, FoodCategory foodcategory
            , string description)
        {
            if (menuItems.ContainsKey(menuItemId))
            {
                throw new ArgumentException("The menu item with this id already exists");
            }
            MenuItem menuItem = new MenuItem(menuItemId,name,price,menucategory,foodcategory,description);
            menuItems.Add(menuItemId, menuItem);
            storage.SaveMenuItems(menuItems);
        }

        public void RemoveMenuItem(int menuItemId)
        {
            if (!menuItems.ContainsKey(menuItemId))
            {
                throw new ArgumentException($"MenuItem With ID{menuItemId} does not exist ");

            }
            else
            {
                menuItems.Remove(menuItemId);
                storage.SaveMenuItems(menuItems);
            }
        }
        public void UpdateMenuItemPrice(int menuItemId,decimal price)
        {
            if (!menuItems.ContainsKey(menuItemId))
            {
                throw new ArgumentException($"MenuItem With ID{menuItemId} does not exist ");

            }
            menuItems[menuItemId].UpdatePrice(price);
            storage.SaveMenuItems(menuItems);
        }

        //TABLE MANAGEMENT
        public void AddTable(int tableNumber,int capacity)
        {
            if (tables.ContainsKey(tableNumber))
            {
                throw new ArgumentException($"Table with this Table Number: {tableNumber}already exists");
            }
            Table table = new Table(tableNumber,capacity);
            tables.Add(tableNumber, table);
            storage.SaveTables(tables);
        }

        public void RemoveTable(int tableNumber)
        {
            if (!tables.ContainsKey(tableNumber))
            {
                throw new ArgumentException($"Table with this Table Number: {tableNumber} does not exists");
            }
            tables.Remove(tableNumber);
            storage.SaveTables(tables);
        }
        public List<Table> GetAvailableTables()
        {
            List<Table> availableTables=new List<Table>();
            foreach(var table in tables.Values)
            {
                if (table.TableStatus == TableStatus.AVAILABLE)
                {
                    availableTables.Add(table);
                }
            }
            if(availableTables.Count == 0)
            {
                throw new ArgumentException("There are no avaialble Tables");
            }
            return availableTables;
        }
    }
}
