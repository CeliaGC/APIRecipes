﻿using API.Attributes;
using API.IServices;
using Data;
using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using Resources.RequestModels;
using System.Web.Http.Cors;

namespace API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Route("[controller]/[action]")]
    public class AlergenController : ControllerBase
    {

        private readonly IIngredientItemService _alergenItemService;
        private readonly ServiceContext _serviceContext;

        public AlergenController(IIngredientItemService alergenItemService, ServiceContext serviceContext)
        {
            _alergenItemService = alergenItemService;
            _serviceContext = serviceContext;

        }

        [EndpointAuthorize(AllowsAnonymous = true)]
        [HttpPost(Name = "InsertAlergen")]
        public int Post([FromBody] AlergenRequest alergenRequest)
        {

            return _alergenItemService.InsertAlergen(alergenRequest);
        }

        ////[EndpointAuthorize(AllowedUserRols = "Administrador")]
        //[EndpointAuthorize(AllowsAnonymous = true)]
        //[HttpDelete(Name = "DeleteRecipe")]
        //public void Delete([FromQuery] int Id)
        //{

        //    _recipeItemService.DeleteRecipe(Id);

        //}

        //[EndpointAuthorize(AllowedUserRols = "Administrador")]
        [EndpointAuthorize(AllowsAnonymous = true)]
        [HttpGet(Name = "GetAllAlergens")]
        public List<AlergenItem> GetAlergens()
        {

            return _alergenItemService.GetAlergens();
        }

        //[HttpPatch(Name = "ModifyImage")]
        //public void Patch([FromBody] ImageItem imageItem)

        //{
        //    _imageService.UpdateImage(imageItem);

        //}


        //[HttpGet(Name = "GetAllImages")]
        //public List<ImageItem> GetAll()
        //{

        //    return _imageService.GetAll();

    }    //}
}