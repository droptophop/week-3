using System.Collections.Generic;
using PizzaStore.Domain.Models;
using PizzaStore.Domain.Factories;

namespace PizzaStore.Client.Models
{
    public class PizzaViewModel
    {
        public List<CrustModel> Crusts { get; set; }
        public List<SizeModel> Sizes { get; set; }
        public List<ToppingModel> Toppings { get; set; }
    }
}