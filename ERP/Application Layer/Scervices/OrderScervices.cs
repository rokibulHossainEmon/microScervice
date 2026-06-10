using Application_Layer.Interfaces;
using Domain_Layer.Interfaces;
using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Scervices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _unitOfWork.Orders.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _unitOfWork.Orders.GetByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            // 1. Fetch the product to check its price and stock
            var product = await _unitOfWork.Products.GetByIdAsync(order.ProductId);

            if (product == null)
                throw new Exception("Product not found.");

            if (product.StockQuantity < order.Quantity)
                throw new Exception($"Not enough stock! Only {product.StockQuantity} left.");

            // 2. Calculate the total price for the order
            order.TotalPrice = product.Price * order.Quantity;

            // 3. Decrease the product's stock quantity
            product.StockQuantity -= order.Quantity;

            // We update the product in memory (it won't save to DB until SaveChangesAsync is called)
            _unitOfWork.Products.Update(product);

            // 4. Add the new order
            await _unitOfWork.Orders.AddAsync(order);

            // 5. Save BOTH the updated product stock AND the new order to the database at the exact same time
            await _unitOfWork.SaveChangesAsync();

            return order;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var existingOrder = await _unitOfWork.Orders.GetByIdAsync(order.Id);
            if (existingOrder == null) return false;

            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(id);
            if (order == null) return false;

            _unitOfWork.Orders.Delete(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}
