using Restaurant_POS___Order_Management_System.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Interfaces
{
    public interface IOrderService 
    {
        void AddMenuItem(int menuItemId, string name, decimal price, MenuCategory menucategory, FoodCategory foodcategory
            , string description);
        void RemoveMenuItem(int menuItemId);

        void UpdateMenuItemPrice(int menuItemId, decimal price);

        //Table Management

        void AddTable(int tableNumber,int capacity);

        void RemoveTable(int tableNumber);

        List<Table> GetAvailableTables();

        //Order Management

        void CreateDineInOrder(int orderId, int? tableNumber, int staffId);

        void CreateTakeAwayOrder(int orderId, int staffId);

        void AddItemToOrder(int orderId,int menuItemId,int quantity);
        
        void RemoveItemFromOrder(int orderId, int menuItemId);

        void AdvanceOrderStatus(int orderId);
        void CancelOrder(int orderId);

        void PayOrder(int orderId,PaymentMethod paymentMethod);

        //Viewing

        Order GetOrder(int orderId);
        List<Order> GetAllActiveOrders();

        //REPORTS

        void GenerateDailySalesReport(DateTime date);
    }
}
