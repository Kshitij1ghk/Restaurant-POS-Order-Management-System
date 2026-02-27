using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant_POS___Order_Management_System.Models
{
    public class OrderItem
    {
        public int OrderId { get; private set; }
        
        public int MenuItemId {  get; private set; }
        public int Quantitiy {  get; private set; }

        public Decimal PriceAtTimeOfOrder {  get; private set; }

        public OrderItem(int orderId,int menuItemId,int quantity,decimal
            priceAtTimeOfOrder)
        {
            if (orderId <= 0)
            {
                throw new ArgumentException("order id cantbe less than or equal to zero!");
            }
            if(menuItemId <= 0)
            {
                throw new ArgumentException("Menu Item ID cannot be less than or equal to zero");
            }
            if (priceAtTimeOfOrder <= 0)
            {
                throw new ArgumentException("order price can't be less than or equal to zero!");
            }
            if(quantity <= 0)
            {
                throw new ArgumentException("quantity can't be less than or equal to zero!");
            }
            orderId = OrderId;
            MenuItemId = menuItemId;
            Quantitiy = quantity;
            PriceAtTimeOfOrder = priceAtTimeOfOrder;
        }
    }
}
