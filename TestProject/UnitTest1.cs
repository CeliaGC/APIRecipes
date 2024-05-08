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
            var orderItem = new OrderItem();

            Assert.IsTrue(orderItem.IsActive);
            Assert.IsTrue((DateTime.Now - orderItem.InsertDate).TotalSeconds < 1); 
        }

    }
}
