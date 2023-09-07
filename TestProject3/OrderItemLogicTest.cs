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
        //Una vez instalado el paquete InMemory, inyecto las dependencias con las que voy a trabajar: 
        // DbContextOptions y ServiceContext + las clases donde están los métodos que voy a testear.
        private DbContextOptions<ServiceContext> _options;
        private ServiceContext _serviceContext;
        private OrderItemLogic _orderItemLogic;


        // Aquí determino que se usa InMemoryDataBase para comprobar los métodos sin afectar a la base de datos real
        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ServiceContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _serviceContext = new ServiceContext(options);
            _orderItemLogic = new OrderItemLogic(_serviceContext);
        }

        // Los test
        [TestMethod]
        public async Task InsertOrders_ShouldInsertOrdersIntoDatabase()
        {
            // Arrange. Las variables, los elementos del entorno que estoy testeando

       

                var ordersRequest = new OrdersRequest
                {
                    OrderItems = new List<OrderItem>
    {
        new OrderItem { IdUser = 1, IngredientName = "Ingredient1", Unit = "kg", Username = "User1" },
        new OrderItem { IdUser = 2, IngredientName = "Ingredient2", Unit = "kg", Username = "User2" }
    }
                };


                // Act. La acción, en este caso un método insert para hacer un post, que voy a comprobar
                await _orderItemLogic.InsertOrders(ordersRequest);

                // Assert. La afirmación que espero que se cumpla como resultado de realizar la acción
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

