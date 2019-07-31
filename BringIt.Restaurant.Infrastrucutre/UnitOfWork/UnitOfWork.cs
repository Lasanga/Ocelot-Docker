using System;
using System.Collections.Generic;
using System.Text;
using BringIt.Restaurant.Infrastrucutre.Repositories.MenuRepository;
using BringIt.Restaurant.Infrastrucutre.Repositories.RestaurantRepository;

namespace BringIt.Restaurant.Infrastrucutre.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BringItRestuarantDbContext _dbContext;

        public UnitOfWork(BringItRestuarantDbContext dbContext)
        {
            _dbContext = dbContext;
            Restaurants = new RestaurantRepository(_dbContext);
            Menus = new MenuRepository(_dbContext);
        }

        public IRestaurantRepository Restaurants { get; private set; }
        public IMenuRepository Menus { get; private set; }

        public int Complete()
        {
            return _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
