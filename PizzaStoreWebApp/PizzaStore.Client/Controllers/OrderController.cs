using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PizzaStore.Storing;
using PizzaStore.Domain.Models;
using PizzaStore.Client.Models;
using PizzaStore.Storing.Factories;
using PizzaStore.Exchange.Concierge;
using System;

namespace PizzaStore.Client.Controllers
{
    // [Route("/Order/{id=1}")] // controller routing
    // [EnableCors("private")]
    public class OrderController : Controller
    {
        private readonly PizzaStoreDbContext _db;




        public OrderController(PizzaStoreDbContext _dbContext)
        {
            _db = _dbContext;
        }



        
        // [HttpGet()]
        public IActionResult Orders()
        {
            UnitOfWork unitOfWork = new UnitOfWork(_db);

            // ViewBag.MenuItems = unitOfWork.Orders.GetAll();

            return View("Orders", new OrderViewModel());
        
        }

        // [HttpGet()]
        public IActionResult AddPizza()
        {
            UnitOfWork unitOfWork = new UnitOfWork(_db);

            ViewBag.MenuItems = unitOfWork.MenuItems.GetAll();
            ViewBag.Sizes = unitOfWork.Sizes.GetAll();
            ViewBag.Crusts = unitOfWork.Crusts.GetAll();
            ViewBag.Toppings = unitOfWork.Toppings.GetAll();

            return View("AddPizza", new PizzaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PlaceOrder(PizzaViewModel model) // model binding
        {
            UnitOfWork unitOfWork = new UnitOfWork(_db);

            ViewBag.MenuItems = unitOfWork.MenuItems.GetAll();
            ViewBag.Sizes = unitOfWork.Sizes.GetAll();
            ViewBag.Crusts = unitOfWork.Crusts.GetAll();
            ViewBag.Toppings = unitOfWork.Toppings.GetAll();

            if (ModelState.IsValid)
            {
                // List<PizzaModel> pizzas = new List<PizzaModel>();
                // pizzas.Add( new PizzaModel{ Name = model.MenuItems } );

                // PizzaModel pizza = new PizzaModel{ Name = model.MenuItems };

                // OrderModel order = new OrderModel{ Date = DateTime.Now, Pizzas = pizzas/*, Details = "", OrderTotal = 10.99m*/ };

                unitOfWork.Pizzas.Add( new PizzaModel{ Name = model.MenuItems } ); 
                unitOfWork.Complete();
                unitOfWork.Dispose();

                return RedirectToAction("Orders");
            }
            
            return View("AddPizza", model);
        }
    }
}