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
            return View();
        }
        public IActionResult PopularProduct()
        {
            SHOPContext db = new SHOPContext();
            var poplist1 = (from p in db.Products
                            join d in db.OrderDetails
                            on p.ProductId equals d.ProductId
                            select new { p.ProductId, p.Name, d.Amount })
                            .GroupBy(x => new { x.ProductId, x.Name })
                            .Select(g => new { Id = g.Key.ProductId, Nazwa = g.Key.Name, Ilość = g.Sum(x => x.Amount) }).ToList();

            var poplist2 = poplist1.Select(x => new PopularProduct()
            {
                ProductId = x.Id,
                Name = x.Nazwa,
                Amount = x.Ilość
            }).OrderByDescending(x => x.Amount).ToList();

            return PartialView("_PopularProduct", poplist2);
        }
        public IActionResult BestOrder()
        {
            SHOPContext db = new SHOPContext();
            var list1 = (from o in db.OrderDetails
                        join p in db.Products
                        on o.ProductId equals p.ProductId
                        select new { o.OrderId, p.Name }).ToList();
            var list2 = list1.Select(x => new ProductName()
            {
                OrderId = x.OrderId,
                Name = x.Name
            }).ToList();
            var list3 = list2.GroupBy(x => x.OrderId).Select(g => new { OrderId = g.Key, Names = g.ToList() });
            var list4 = (from o in db.Orders
                         join x in list3
                         on o.OrderId equals x.OrderId
                         select new { o.OrderId, o.ClientId, o.TotalPrice, x.Names }).ToList();
            var list5 = (from y in list4
                         join c in db.Clients
                         on y.ClientId equals c.ClientId
                         select new { y.OrderId, y.TotalPrice, y.Names, c.FirstName, c.LastName }).ToList();
            var list6 = list5.Select(x => new BestOrder()
            {
                OrderId = x.OrderId,
                TotalPrice = x.TotalPrice,
                Products = x.Names,
                FirstName = x.FirstName,
                LastName = x.LastName
            }).OrderByDescending(x => x.TotalPrice).ToList();
            return PartialView("_BestOrder", list6);
        }
        public IActionResult BestClient()
        {
            SHOPContext db = new SHOPContext();
            var list1 = (from c in db.Clients
                         join o in db.Orders
                         on c.ClientId equals o.ClientId
                         select new { c.FirstName, c.LastName, c.ClientId, o.OrderId }).ToList();
            var list2 = list1.GroupBy(x => new { x.FirstName, x.LastName, x.ClientId })
                .Select(g => new { FirstName = g.Key.FirstName, LastName = g.Key.LastName, ClientId = g.Key.ClientId, Count = g.Count() });
            var list3 = list2.Select(x => new BestClient()
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                ClientId = x.ClientId,
                Count = x.Count
            }).OrderByDescending(x => x.Count).ToList();
            return PartialView("_BestClient", list3);
        }
        public IActionResult BestPayment()
        {
            SHOPContext db = new SHOPContext();
            var list1 = (from p in db.Payment
                         join o in db.Orders
                         on p.Id equals o.Payment
                         select new { p.Payment1, o.OrderId }).ToList();
            var list2 = list1.GroupBy(x => x.Payment1).Select(g => new { Payment = g.Key, Count = g.Count() });
            var list3 = list2.Select(x => new BestPD()
            {
                NamePD = x.Payment,
                Count = x.Count
            }).OrderByDescending(x => x.Count).ToList();
            return PartialView("_BestPayment", list3);
        }
        public IActionResult BestDelivery()
        {
            SHOPContext db = new SHOPContext();
            var list1 = (from d in db.Delivery
                         join o in db.Orders
                         on d.Id equals o.Delivery
                         select new { d.Delivery1, o.OrderId }).ToList();
            var list2 = list1.GroupBy(x => x.Delivery1).Select(g => new { Delivery = g.Key, Count = g.Count() });
            var list3 = list2.Select(x => new BestPD()
            {
                NamePD = x.Delivery,
                Count = x.Count
            }).OrderByDescending(x => x.Count).ToList();
            return PartialView("_BestDelivery", list3);
        }
    }
}