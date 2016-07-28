using System.ComponentModel.DataAnnotations;
using AspNetCore.Entities;

namespace AspNetCore.ViewModels
{
    public class RestaurantEditViewModel
    {
        [Required, MaxLength(80)]
        public string Name { get; set; }

        public CuisineType Cuisine { get; set; }


    }
}
