using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_Management.Models;

namespace MVC_Management.Controllers
{
    public class StatisticController : Controller
    {
        public IActionResult Index()
        {
            SHOPContext db = new SHOPContext();
            var lista = (from p in db.Products
                         join s in db.Store
                         on p.ProductId equals s.ProductId
                         select new { p.ProductId, p.Name, s.Amount}).ToList();
            var lista2 = lista.Select(x => new ProductInStore()
            {
                ProductId = x.ProductId, Name = x.Name, Amount = x.Amount
            }).ToList();
            return View(lista2);
        }
    }
}