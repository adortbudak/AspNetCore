using System.Collections.Generic;
using System.Linq;
using AspNetCore.Entities;

namespace AspNetCore.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        void Add(Restaurant newRestaurant);
        int Commit();
    }

    public class SqlRestaurantData : IRestaurantData
    {
        private RestaurantDbContext _context;

        public SqlRestaurantData(RestaurantDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public void Add(Restaurant newRestaurant)
        {
            _context.Add(newRestaurant);
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
    public class InMemoryRestaurantData : IRestaurantData
    {
        static InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant {Id=1,Name = "Tulip"},
                new Restaurant {Id = 2,Name = "Aksoy Pita"},
                new Restaurant {Id=3,Name="Turquise" }
            };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants;
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = _restaurants.Max(r => r.Id) + 1;
            _restaurants.Add(newRestaurant);

        }

        public int Commit()
        {
            return 0;
        }

        static List<Restaurant> _restaurants;
    }
}
