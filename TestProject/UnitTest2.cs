using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities;
using Logic.Logic;
using Resources.RequestModels;
using Data;


namespace TestProject
{

[TestClass]
public class OrderItemLogicIntegrationTests
{
    private DbContextOptions<ServiceContext> _options;

    [TestInitialize]
    public void Setup()
    {
        _options = new DbContextOptionsBuilder<ServiceContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryTestDatabase")
            .Options;

        using (var context = new ServiceContext(_options))
        {
            context.Database.EnsureCreated();
        }
    }

    [TestMethod]
    public async Task InsertOrders_ShouldInsertOrdersIntoDatabase()
    {
        using (var context = new ServiceContext(_options))
        {
            var orderItemLogic = new OrderItemLogic(context);

                var ordersRequest = new OrdersRequest
                {
                    OrderItems = new List<OrderItem>
    {
        new OrderItem { IdUser = 1, IngredientName = "Ingredient1", Unit = "kg", Username = "User1" },
        new OrderItem { IdUser = 2, IngredientName = "Ingredient2", Unit = "kg", Username = "User2" }
    }
                };

                await orderItemLogic.InsertOrders(ordersRequest);

            var ordersInDatabase = context.Orders.ToList();
            Assert.AreEqual(2, ordersInDatabase.Count);
        }
    }
}

}
