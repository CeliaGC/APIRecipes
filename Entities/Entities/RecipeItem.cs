using Entities.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class RecipeItem
    {
        public RecipeItem()
        {
            IsActive = true;
            IsPublic= true;
            InsertDate = DateTime.Now;

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }
        public string? Author { get; set; }
        public string? Observations { get; set; }
        public string? Materials { get; set; }
        public int PostedBy { get; set; }
        public string PosterName { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        public DateTime InsertDate { get; set; }
        public virtual ICollection<Recipe_Ingredient> Ingredients { get; set; }
        [ForeignKey("Recipe_AlergenId")]
        public virtual ICollection<Recipe_Alergen> Alergens { get; set; }
        [JsonIgnore]
        public virtual ICollection<CategoryItem> Categories { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserItem> Users { get; set; }

    }
}
