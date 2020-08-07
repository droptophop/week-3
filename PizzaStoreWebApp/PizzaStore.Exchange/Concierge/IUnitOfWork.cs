using System;
using PizzaStore.Storing.Factories;

namespace PizzaStore.Exchange.Concierge
{
    public interface IUnitOfWork : IDisposable
    {
        IPizzaFactory Pizzas { get; }
        int Complete();
    }
}