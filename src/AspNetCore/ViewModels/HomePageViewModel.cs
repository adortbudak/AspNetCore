using System.Collections.Generic;
using AspNetCore.Entities;

namespace AspNetCore.ViewModels
{
    public class HomePageViewModel
    {
        public IEnumerable<Restaurant> Restaurants { get; set; }

        public string CurrentGreeting { get; set; }
    }
}
