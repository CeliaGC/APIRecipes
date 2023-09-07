using Data;
using Entities.Entities;
using Logic.Logic;
using Microsoft.EntityFrameworkCore;
using Resources.RequestModels;

namespace TestProject3
{
    [TestClass]
    public class IngredientItemLogicTest
    {
        private DbContextOptions<ServiceContext> _options;
        private ServiceContext _serviceContext;
        private IngredientItemLogic _ingredientItemLogic;

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
        public void IngredientInsertNotRepeatRegardlessCase()
        {
            var ingredient1 = new IngredientItem { Ingredient = "TOMATE" };
            var ingredient2 = new IngredientItem { Ingredient = "tomate" };

            _ingredientItemLogic.InsertIngredient(ingredient1);
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
