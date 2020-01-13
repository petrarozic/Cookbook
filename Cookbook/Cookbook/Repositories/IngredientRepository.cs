using Cookbook.Interfaces;
using Cookbook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cookbook.Repositories
{
    public class IngredientRepository: IIngredientRepository
    {
        private readonly AppDbContext _appDbContext;

        public IngredientRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<string> GetAllIngredientName()
        {
            return _appDbContext.Ingredients
                                 .Select(i => i.Name);
        }

        public Ingredient GetIngredientByName(string name)
        {
            return _appDbContext.Ingredients
                               .Where(i => i.Name == name)
                               .First();
        }
    }
}
