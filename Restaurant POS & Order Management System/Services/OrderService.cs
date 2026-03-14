using Restaurant_POS___Order_Management_System.Interfaces;
using Restaurant_POS___Order_Management_System.Models;
using System;
using System.Collections.Generic;

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
            this.storage= storage;
            menuItems=storage.LoadMenuItems();
            orders=storage.LoadOrders();
            orderItems=storage.LoadOrderItems();
            tables=storage.LoadTables();
            staffs=storage.LoadStaff();
            receipts=storage.LoadReceipts();
        }
    }
}
