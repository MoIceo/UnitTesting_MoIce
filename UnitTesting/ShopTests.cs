using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingLib.Shop;

namespace UnitTesting
{
    public class ShopTests
    {
        private readonly Mock<IOrderRepository> mockOrderRepository;
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<INotificationService> mockNotificationService;

        public ShopTests()
        {
            mockOrderRepository = new Mock<IOrderRepository>();
            mockCustomerRepository = new Mock<ICustomerRepository>();
            mockNotificationService = new Mock<INotificationService>();
        }

        [Fact]
        public void CreateOrder_ShouldReturnTrue_WhenOrderCreated()
        {
            var customer = new Customer { Id = 1, Name = "Test Customer", Email = "Email@gmail.com" };
            var order = new Order { Id = 1, Date = DateTime.Today, Customer = customer , Amount = 1 };

            mockCustomerRepository.Setup(repo => repo.GetCustomerById(1)).Returns(customer);
            mockOrderRepository.Setup(repo => repo.GetOrderById(1)).Returns(order);

            var service = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);

            service.CreateOrder(order);

            mockOrderRepository.Verify(repo => repo.AddOrder(order), Times.Once);
            mockNotificationService.Verify(s => s.SendNotification(customer.Email, $"Order {order.Id} created for customer {order.Customer.Name} total price {order.Amount}"), Times.Once);
        }

        [Fact]
        public void GetCustomerInfo_ShouldReturnCustomerInfo()
        {

        }
    }
}
