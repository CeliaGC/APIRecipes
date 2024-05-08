using Data;
using Entities.Entities;
using Logic.Ilogic;
using Logic.Logic;
using Microsoft.EntityFrameworkCore;
using Resources.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject3
{
    [TestClass]
    public class OrderItemLogicTest
    {
        private DbContextOptions<ServiceContext> _options;
        private ServiceContext _serviceContext;
        private OrderItemLogic _orderItemLogic;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ServiceContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _serviceContext = new ServiceContext(options);
            _orderItemLogic = new OrderItemLogic(_serviceContext);
        }

        [TestMethod]
        public async Task InsertOrders_ShouldInsertOrdersIntoDatabase()
        {
                var ordersRequest = new OrdersRequest
                {
                    OrderItems = new List<OrderItem>
    {
        new OrderItem { IdUser = 1, IngredientName = "Ingredient1", Unit = "kg", Username = "User1" },
        new OrderItem { IdUser = 2, IngredientName = "Ingredient2", Unit = "kg", Username = "User2" }
    }
                };

                await _orderItemLogic.InsertOrders(ordersRequest);

                var ordersInDatabase = _serviceContext.Orders.ToList();
                Assert.AreEqual(2, ordersInDatabase.Count);
            
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_serviceContext != null)
            {
                _serviceContext.Database.EnsureDeleted();
            }
        }
    }
}

