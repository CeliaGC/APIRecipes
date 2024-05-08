using Data;
using Entities.Entities;
using Logic.Ilogic;
using Logic.Logic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestClass]
    public class IngredientItemLogicTest
    {
        private DbContextOptions<ServiceContext> _options;
        private ServiceContext _serviceContext;
        private IIngredientItemLogic _ingredientItemLogic;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ServiceContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
            _serviceContext = new ServiceContext(options);
            _ingredientItemLogic = new IngredientItemLogic(_serviceContext);
        }

        [TestMethod]
        public void Test_InsertIngredient_AvoidsDuplicatesRegardlessOfCase()
        {
            var ingredient1 = new IngredientItem { Ingredient = "tomate" };
            _ingredientItemLogic.InsertIngredient(ingredient1);

            var ingredient2 = new IngredientItem { Ingredient = "TOMATE" };
            _ingredientItemLogic.InsertIngredient(ingredient2);

            var allIngredients = _ingredientItemLogic.GetIngredients();
            Assert.AreEqual(1, allIngredients.Count);
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
