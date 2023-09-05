using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entities.Entities;
using System;

namespace TestProject
{
    [TestClass]
    public class OrderItemTests
    {
        [TestMethod]
        public void Constructor_ShouldSetDefaultValues()
        {
            // Arrange & Act
            var orderItem = new OrderItem();

            // Assert
            Assert.IsTrue(orderItem.IsActive);
            Assert.IsTrue((DateTime.Now - orderItem.InsertDate).TotalSeconds < 1); // Comprobando que InsertDate es muy cercano a DateTime.Now
        }

        [TestMethod]
        public void Properties_ShouldGetAndSetValues()
        {
            // Arrange
            var orderItem = new OrderItem
            {
                Id = 1,
                IdUser = 2,
                Username = "testuser",
                IdIngredient = 3,
                IngredientName = "testIngredient",
                Amount = 10.5m,
                Unit = "kg",
                IsActive = false
            };

            // Assert
            Assert.AreEqual(1, orderItem.Id);
            Assert.AreEqual(2, orderItem.IdUser);
            Assert.AreEqual("testuser", orderItem.Username);
            Assert.AreEqual(3, orderItem.IdIngredient);
            Assert.AreEqual("testIngredient", orderItem.IngredientName);
            Assert.AreEqual(10.5m, orderItem.Amount);
            Assert.AreEqual("kg", orderItem.Unit);
            Assert.IsFalse(orderItem.IsActive);
        }
    }
}
