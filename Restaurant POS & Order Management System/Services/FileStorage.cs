using Restaurant_POS___Order_Management_System.Interfaces;
using Restaurant_POS___Order_Management_System.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
                    SafeDescription + "," +
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
            for(int i=1;i<lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int menuItemId = int.Parse(parts[0]);
                    string name = parts[1];
                    decimal price = decimal.Parse(parts[2]);
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

        public void SaveOrders(Dictionary<int, Order> orders)
        {
            List<string> lines = new List<string>();
            lines.Add("OrderId,OrderType,Status,TableNumber,StaffId,CreatedAt,CompletedAt" +
                ",PaymentMethod,TotalAmount");
            foreach (Order order in orders.Values)
            {
                string line = order.OrderId + "," +
                    order.OrderType + "," +
                    order.Status + "," +
                    order.TableNumber + "," +
                    order.StaffId + "," +
                    order.CreatedAt + "," +
                    order.CompletedAt + "," +
                    order.PaymentMethod + "," +
                    order.TotalAmount;
                lines.Add(line);
            }
            File.WriteAllLines(OrdersFile, lines);
        }
        public Dictionary<int, Order> LoadOrders()
        {
            Dictionary<int,Order> orders = new Dictionary<int,Order>();
            if (!File.Exists(OrdersFile))
                return orders;
            string[] lines=File.ReadAllLines(OrdersFile);
            for(int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int orderId = int.Parse(parts[0]);
                    OrderType orderType = (OrderType)Enum.Parse(typeof(OrderType), parts[1]);
                    OrderStatus status = (OrderStatus)Enum.Parse(typeof(OrderStatus), parts[2]);
                    int? tableNumber = string.IsNullOrEmpty(parts[3]) ? (int?)null : int.Parse(parts[3]);
                    int staffId = int.Parse(parts[4]);
                    DateTime createdAt = DateTime.Parse(parts[5]);
                    DateTime? completedAt = string.IsNullOrEmpty(parts[6]) ? (DateTime?)null : DateTime.Parse(parts[6]);
                    PaymentMethod? paymentMethod = string.IsNullOrEmpty(parts[7])?(PaymentMethod?)null:(PaymentMethod)Enum.Parse(typeof(PaymentMethod), parts[7]);
                    decimal totalAmount = decimal.Parse(parts[8]);

                    Order order = new Order(orderId, orderType, status, tableNumber, staffId, createdAt,
                        completedAt,paymentMethod,totalAmount);
                    orders.Add(orderId,order);

                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            return orders;
        }

        public void SaveOrderItems(Dictionary<int, List<OrderItem>> orderaItems)
        {
            List<string> lines=new List<string>();
            lines.Add("OrderId,MenuItemId,Quantity,PriceAtTimeOfOrder");
            foreach(KeyValuePair<int,List<OrderItem>> pair in orderaItems)
            {
                foreach(OrderItem orderItem in pair.Value)
                {
                    string line = orderItem.OrderId + "," +
                        orderItem.MenuItemId + "," +
                        orderItem.Quantitiy + "," +
                        orderItem.PriceAtTimeOfOrder;
                    lines.Add(line);
                }
            }
            File.WriteAllLines(OrderItemsFile, lines);
        }

        public Dictionary<int, List<OrderItem>> LoadOrderItems()
        {
            Dictionary<int, List<OrderItem>> orderitems = new Dictionary<int, List<OrderItem>>();
            if (!File.Exists(OrderItemsFile))
                return orderitems;
            string[] lines = File.ReadAllLines(OrderItemsFile);
            for(int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int orderId = int.Parse(parts[0]);
                    int menuItemId = int.Parse(parts[1]);
                    int quantity = int.Parse(parts[2]);
                    decimal priceAtTimeOfOrder = decimal.Parse(parts[3]);

                    OrderItem orderItem = new OrderItem(orderId, menuItemId, quantity, priceAtTimeOfOrder);
                    if (orderitems.ContainsKey(orderId))
                    {
                        orderitems[orderId].Add(orderItem);
                    }
                    else
                    {
                        List<OrderItem> newList = new List<OrderItem>();
                        newList.Add(orderItem);
                        orderitems.Add(orderId, newList);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return orderitems;
        }

        public void SaveTables(Dictionary<int, Table> tables)
        {
            List<string> lines = new List<string>();
            lines.Add("TableNumber,Capacity,TableStatus");
            foreach(Table table in tables.Values)
            {
                string line = table.TableNumber+","+
                    table.Capacity+","+
                    table.TableStatus;
                lines.Add(line);

            }
            File.WriteAllLines(TablesFile, lines);
        }
        public Dictionary<int, Table> LoadTables()
        {
            Dictionary<int,Table> tables=new Dictionary<int,Table>();
            if (!File.Exists(TablesFile))
                return tables;
            string[] lines = File.ReadAllLines(TablesFile);
            for(int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int tableNumber = int.Parse(parts[0]);
                    int capacity = int.Parse(parts[1]);
                    TableStatus tableStatus = (TableStatus)Enum.Parse(typeof(TableStatus), parts[2]);
                     Table table=new Table(tableNumber, capacity, tableStatus);
                    tables.Add(tableNumber, table);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            return tables;

        }
        public void SaveStaff(Dictionary<int, Staff> staffs)
        {
            List<string> lines = new List<string>();
            lines.Add("StaffId,Name,Role,IsOnDuty");
            foreach(Staff staff in staffs.Values)
            {
                string line = staff.StaffId + "," +
                    staff.Name + "," +
                    staff.StaffRole+ "," +
                    staff.IsOnDuty;
                lines.Add(line);
            }
            File.WriteAllLines(StaffFile, lines);
        }

        public Dictionary<int, Staff> LoadStaff()
        {
            Dictionary<int,Staff> staffs=new Dictionary<int, Staff> ();
            if(!File.Exists(StaffFile))
                return staffs;
            string[] lines=File.ReadAllLines(StaffFile);
            for(int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int staffId = int.Parse(parts[0]);
                    string name = parts[1];
                    StaffRole staffRole = (StaffRole)Enum.Parse(typeof(StaffRole), parts[2]);
                    bool isOnDuty = bool.Parse(parts[3]);

                    Staff staff = new Staff(staffId, name,staffRole,isOnDuty);
                    staffs.Add(staffId, staff);
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            return staffs;
        }
        public void SaveReceipts(Dictionary<int, Receipt> receipts)
        {
            List<string> lines=new List<string> ();
            lines.Add("ReceiptId,OrderID,SubTotal,TaxAmount,TotalAmount,PayementMethod,PaidAt");
            foreach(Receipt receipt in receipts.Values)
            {
                string line = receipt.ReceiptId + "," +
                    receipt.OrderId + "," +
                    receipt.SubTotal + "," +
                    receipt.TaxAmount + "," +
                    receipt.TotalAmount + "," +
                    receipt.PaymentMethod + "," +
                    receipt.PaidAt;
                lines.Add(line);
            }
            File.WriteAllLines(RecieptFile, lines);
        }

        public Dictionary<int, Receipt> LoadReceipts()
        {
            Dictionary<int,Receipt> receipts=new Dictionary<int, Receipt> ();
            if(!File.Exists(RecieptFile))
                return receipts;
            string[] lines=File.ReadAllLines(RecieptFile);
            for(int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i]))
                    continue;
                try
                {
                    string[] parts = lines[i].Split(',');
                    int receiptId = int.Parse(parts[0]);
                    int orderId = int.Parse(parts[1]);
                    decimal subTotal = decimal.Parse(parts[2]);
                    decimal taxAmount = decimal.Parse(parts[3]);
                    decimal totalAmount = decimal.Parse(parts[4]);
                    PaymentMethod paymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), parts[5]);
                    DateTime paidAt = DateTime.Parse(parts[6]);

                    Receipt receipt = new Receipt(receiptId, orderId, subTotal, taxAmount, totalAmount, paymentMethod, paidAt);
                    receipts.Add(receiptId, receipt);
                }
                catch(Exception ex)
                {
                    continue;
                }
            }
            return receipts;
            
        }
    }
}
