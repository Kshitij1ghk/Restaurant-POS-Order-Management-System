using Restaurant_POS___Order_Management_System.Interfaces;
using Restaurant_POS___Order_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace Restaurant_POS___Order_Management_System.Services
{
    public class OrderService : IOrderService
    {
        private Dictionary<int, MenuItem> menuItems;
        private Dictionary<int, Order> orders;
        private Dictionary<int, List<OrderItem>> orderItems;
        private Dictionary<int, Table> tables;
        private Dictionary<int, Staff> staffs;
        private Dictionary<int, Receipt> receipts;
        private IStorage storage;

        // Constructor - This is called when OrderService is created in Program.cs
        // It loads all existing data from CSV files into memory
        // Think of it as the manager loading all information when restaurant opens for the day
        public OrderService(IStorage storage)
        {
            // Store the storage object so we can use it later to save/load data
            // IStorage is the interface, FileStorage is the actual implementation
            this.storage = storage;
            menuItems = storage.LoadMenuItems();
            orders = storage.LoadOrders();
            orderItems = storage.LoadOrderItems();
            tables = storage.LoadTables();
            staffs = storage.LoadStaff();
            receipts = storage.LoadReceipts();
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
            MenuItem menuItem = new MenuItem(menuItemId, name, price, menucategory, foodcategory, description);
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
        public void UpdateMenuItemPrice(int menuItemId, decimal price)
        {
            if (!menuItems.ContainsKey(menuItemId))
            {
                throw new ArgumentException($"MenuItem With ID{menuItemId} does not exist ");

            }
            menuItems[menuItemId].UpdatePrice(price);
            storage.SaveMenuItems(menuItems);
        }

        //TABLE MANAGEMENT
        public void AddTable(int tableNumber, int capacity)
        {
            if (tables.ContainsKey(tableNumber))
            {
                throw new ArgumentException($"Table with this Table Number: {tableNumber}already exists");
            }
            Table table = new Table(tableNumber, capacity);
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
            List<Table> availableTables = new List<Table>();
            foreach (var table in tables.Values)
            {
                if (table.TableStatus == TableStatus.AVAILABLE)
                {
                    availableTables.Add(table);
                }
            }
            if (availableTables.Count == 0)
            {
                throw new ArgumentException("There are no avaialble Tables");
            }
            return availableTables;
        }

        //Order Management

        public void CreateDineInOrder(int orderId, int? tableNumber, int staffId)
        {
            if (orders.ContainsKey(orderId))
            {
                throw new ArgumentException("Order with this id already exists");
            }
            Order order = new Order(orderId, OrderType.DINE_IN, tableNumber, staffId);
            orders.Add(orderId, order);
            storage.SaveOrders(orders);
        }

        public void CreateTakeAwayOrder(int orderId, int staffId)
        {
            if (orders.ContainsKey(orderId))
            {
                throw new ArgumentException("The Order with this id already exists");
            }
            Order order = new Order(orderId, OrderType.TAKEAWAY, null, staffId);
            orders.Add(orderId, order);
            storage.SaveOrders(orders);
        }

        public void AddItemToOrder(int orderId, int menuItemId, int quantity)
        {
            if (!(orders.ContainsKey(orderId)))
            {
                throw new ArgumentException("Order with this Id Does Not Exist");
            }
            else if (!(menuItems.ContainsKey(menuItemId)))
            {
                throw new ArgumentException("MenuItem with this ID does not exist");
            }
            else if (orders[orderId].Status == OrderStatus
                .CANCELLED || orders[orderId].Status == OrderStatus.PAID)
            {
                throw new ArgumentException("Cannot add items to a CANCELLED or PAID orderd");
            }
            else if (orderItems.ContainsKey(orderId))
            {
                OrderItem existingItem = orderItems[orderId].Find(item => item.MenuItemId == menuItemId);

                if (existingItem != null)
                {
                    // item already exists → increase quantity
                    existingItem.IncreaseQuantity(quantity);
                }
                else
                {
                    // item doesn't exist → add new OrderItem to the list
                    decimal price = menuItems[menuItemId].Price;
                    OrderItem newItem = new OrderItem(orderId, menuItemId, quantity, price);
                    orderItems[orderId].Add(newItem);
                }
            }
            else
            {
                //order has no items yet so we create a new list with this item
                decimal price = menuItems[menuItemId].Price;
                OrderItem newItem = new OrderItem(orderId, menuItemId, quantity, price);
                orderItems.Add(orderId, new List<OrderItem> { newItem });
            }
            decimal newTotal = 0;
            foreach (OrderItem item in orderItems[orderId])
            {
                newTotal += item.PriceAtTimeOfOrder * item.Quantitiy;
            }
            orders[orderId].UpdateTotal(newTotal);

            storage.SaveOrderItems(orderItems);
            storage.SaveOrders(orders);

        }
        ///we needed to add a food item to an existing Order 
        ///so we first took The AddItemToOrder method defined in Interface to implement it here
        ///then we checked the validations in it first if such order id exists then menuitemID
        ///then we addedd a validation for order status that is cancelled or paid 
        ///then we used else if(orderItems.ContainsKey(orderId)) to check if this order already has any items
        ///if yes we enter the block
        ///then OrderItem existingItem = orderItems[orderId].Find(item => item.MenuItemId == menuItemId);
        ///Breaking this down:
        ///- `orderItems[orderId]` → gets the **list of items** for this order
        ///- `.Find(...)` → built-in method that searches through the list
        ///- `item => item.MenuItemId == menuItemId` → **for each item in the list, check if its MenuItemId matches * *
        ///-If found → returns that `OrderItem`
        //- If not found → returns `null`
        ///example
        ///Order 1 has: [Burger(101), Coke(102), Fries(103)]
        ///Find(item => item.MenuItemId == 102)
        ///yes returns coke
        ///if item was was found jusgt increase its quantity no duplicate created
        ///else if itme not found create a breand new OrderItem and add it to the existing list
        ///now if order has no items yet
        ///we create a new list with this item and recalculate total
        ///then we save it to the csv
        ///
        public void RemoveItemFromOrder(int orderId, int menuiItem)
        {
            if (!orders.ContainsKey(orderId))
            {
                throw new ArgumentException("No such order ID exists");
            }
            if (!orderItems.ContainsKey(orderId))
            {
                throw new ArgumentException("This order has no items");

            }
            OrderItem existingItem = orderItems[orderId].Find(item => item.MenuItemId == menuiItem);

            if (existingItem == null)
            {
                throw new ArgumentException("This item does not exist in the order");
            }

            orderItems[orderId].Remove(existingItem);

            decimal newTotal = 0;
            foreach(OrderItem item in orderItems[orderId])
            {
                newTotal += item.PriceAtTimeOfOrder * item.Quantitiy;
            }
            orders[orderId].UpdateTotal(newTotal);
            storage.SaveOrderItems(orderItems);
            storage.SaveOrders(orders);
        }

        public void AdvanceOrderStatus(int orderId)
        {
            if (!orders.ContainsKey(orderId))
            {
                throw new ArgumentException("order with this ID does not exist");
            }

            if (orders[orderId].Status==OrderStatus.PENDING)
                orders[orderId].UpdateStatus(OrderStatus.PREPARING);
            else if (orders[orderId].Status == OrderStatus.PREPARING)
                orders[orderId].UpdateStatus(OrderStatus.READY);
            else if (orders[orderId].Status == OrderStatus.READY)
                orders[orderId].UpdateStatus(OrderStatus.DELIVERED);
            else
                throw new ArgumentException("Order cannot be advanced further");

            storage.SaveOrders(orders);
        }

        public void CancelOrder(int orderId)
        {
            if (!orders.ContainsKey(orderId))
            {
                throw new ArgumentException("Order with this ID does Not exist");
            }
            if (orders[orderId].Status == OrderStatus.CANCELLED)
                throw new ArgumentException("The order is already cancelled");
            else if (orders[orderId].Status == OrderStatus.PAID)
            {
                throw new ArgumentException("The order cant be cancelled since it has been already paid ");
            }
            else
            {
                orders[orderId].UpdateStatus(OrderStatus.CANCELLED);
            }
            storage.SaveOrders(orders);
        }

        public 
    }
}
