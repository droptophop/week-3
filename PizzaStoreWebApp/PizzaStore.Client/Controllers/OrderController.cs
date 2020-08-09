using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using PizzaStore.Storing;
using PizzaStore.Domain.Models;
using PizzaStore.Client.Models;
using PizzaStore.Storing.Factories;
using PizzaStore.Exchange.Concierge;

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
            if (ModelState.IsValid)
            {
                return RedirectToAction("Orders");
            }

            UnitOfWork unitOfWork = new UnitOfWork(_db);

            ViewBag.MenuItems = unitOfWork.MenuItems.GetAll();
            ViewBag.Sizes = unitOfWork.Sizes.GetAll();
            ViewBag.Crusts = unitOfWork.Crusts.GetAll();
            ViewBag.Toppings = unitOfWork.Toppings.GetAll();
            
            return View("AddPizza", model);
        }
    }
}