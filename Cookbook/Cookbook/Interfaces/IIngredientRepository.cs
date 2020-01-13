using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cookbook.Models;

namespace Cookbook.Interfaces
{
    public interface IIngredientRepository
    {
        IEnumerable<String> GetAllIngredientName();
        Ingredient GetIngredientByName(string name);
    }
}
