using Restaurant_POS___Order_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Interfaces
{
    public interface IStorage
    {
        void SaveMenuItems(List<MenuItem> menuItems);
        void LoadMenuItems();

        void SaveOrders(List<Order> orders);
        void LoadOrders();

        void SaveOrderItems(List<OrderItem> orderItems);
        void LoadOrderItems();

        void SaveTables(List<Table> tables);
        void LoadTables();

        void SaveStaff(List<Staff> staffs);
        void LoadStaff();

        void SaveReceipts(List<Receipt> receipts);
        void LoadReceipts();
    }
}
