using Data;
using Entities.Entities;
using Entities.Relations;
using Logic.Ilogic;
using Logic.Logic;
using Microsoft.EntityFrameworkCore;
using Resources.RequestModels;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Logic.Logic
{
    public class IngredientItemLogic : BaseContextLogic, IIngredientItemLogic
    {
        public IngredientItemLogic (ServiceContext serviceContext) : base(serviceContext) { }


        public List<IngredientItem> GetIngredients()
        {



            return _serviceContext.Ingredients.ToList();

         }

        public int InsertIngredient(IngredientItem ingredientItem)
        {

            //var newIngredient = new IngredientItem
            //{
            //    Id = ingredientRequest.Id,
            //    Name = ingredientRequest.Name,
            //};

            if (!_serviceContext.Ingredients.Any(i => i.Ingredient.Equals(ingredientItem.Ingredient, StringComparison.OrdinalIgnoreCase)))
            {
                _serviceContext.Ingredients.Add(ingredientItem);
                _serviceContext.SaveChanges();
                return ingredientItem.Id;
            }
            return -1;
         

         
            
        }


    }
}



//var recipeData =  _serviceContext.Set<RecipeItem>().ToList();
//var alergens = _serviceContext.Set<Recipe_Alergen>().Where

//foreach (var recipe in recipeData) 
//{
//  recipeRequest.Name= recipe.Name;
//  recipeRequest.Alergens = 
//}





//{
//    List<RecipeItem> recipes;
//    using (var context = new ServiceContext(Recipe))
//    {
//        recipes = _serviceContext.Recipes
//            .Include(r => r.Category)

//            .ToList();
//    }
//    return recipes;
//}