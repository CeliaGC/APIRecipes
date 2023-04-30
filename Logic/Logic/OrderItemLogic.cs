﻿using Data;
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
    public class OrderItemLogic : BaseContextLogic, IOrderItemLogic
    {
        public OrderItemLogic (ServiceContext serviceContext) : base(serviceContext) { }



        public List<OrderItem>GetOrders()
        {
            return _serviceContext.Orders.ToList()
              ;
        }

        public async Task<IEnumerable<OrderItem>> InsertOrder(IEnumerable<OrderRequest> orderRequests)
        {
            var newOrders = new List<OrderItem>();
            foreach (var orderRequest in orderRequests)
            {
                var orderData = orderRequest.Items;
                foreach (var item in orderData)
                {
                    var userId = item.IdUser;
                    var ingredientId = item.IdIngredient;

                    var newOrder = new OrderItem
                    {
                        IdUser = userId,
                        IdIngredient = ingredientId,
                        Amount = item.Amount,
                        Unit = item.Unit
                    };

                    _serviceContext.Orders.Add(newOrder);
                    await _serviceContext.SaveChangesAsync();
                    newOrders.Add(newOrder);
                }
            }

            return newOrders;
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