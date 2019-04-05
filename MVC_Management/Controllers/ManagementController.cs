using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC_Management.Models;

namespace MVC_Management.Controllers
{
    public class ManagementController : Controller
    {

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult SeeAllProducts()
        {
            SHOPContext db = new SHOPContext();
            List<Products> products = new List<Products>();
            products = db.Products.ToList();
            return PartialView("_SeeAllProducts", products);
        }
        public IActionResult ProductsSortPrice()
        {
            SHOPContext db = new SHOPContext();
            List<Products> products = new List<Products>();
            products = db.Products.OrderBy(x => x.Price).ToList();
            return PartialView("_ProductsSortPrice", products);
        }
        public IActionResult ProductsSortName()
        {
            SHOPContext db = new SHOPContext();
            List<Products> products = new List<Products>();
            products = db.Products.OrderBy(x => x.Name).ToList();
            return PartialView("_ProductsSortName", products);
        }
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Products product)
        {
            SHOPContext db = new SHOPContext();
            var list = db.Products.Where(x => x.Name == product.Name).ToList();
            if (list.Count == 0)
            {
                db.Products.Add(product);
                db.SaveChanges();
                var newProduct = db.Products.Where(x => x.Name == product.Name).FirstOrDefault();
                db.Store.Add(new Store() { ProductId = newProduct.ProductId, Amount = 0 });
                db.SaveChanges();
                return RedirectToAction("/Confirmation");
            }
            else
            {
                return Content("Produkt o takiej nazwie już istnieje w bazie danych.\n Wpisz inną nazwę.");
            }
           
        }
        public IActionResult Confirmation()
        {
            return View();
        }
            public IActionResult EditProduct(int id)
        {
            SHOPContext db = new SHOPContext();
            Products product = db.Products.Where(x => x.ProductId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Products product)
        {
            SHOPContext db = new SHOPContext();
            Products prod = db.Products.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
            prod.Name = product.Name;
            prod.ImgUrl = product.ImgUrl;
            prod.Price = product.Price;
            db.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("/Index");

        }
        public IActionResult SeeAllClients()
        {
            SHOPContext db = new SHOPContext();
            List<Clients> clients = new List<Clients>();
            clients = db.Clients.ToList();
            return PartialView("_SeeAllClients", clients);
        }
        public IActionResult SearchClient(string text)
        {
            SHOPContext db = new SHOPContext();
            List<Clients> clients = new List<Clients>();
            clients = db.Clients.Where(x => x.LastName.StartsWith(text)).ToList();
            return PartialView("_SeeAllClients", clients);
        }
        public IActionResult Store()
        {
            SHOPContext db = new SHOPContext();
            var storeNew = (from p in db.Products
                         join s in db.Store
                         on p.ProductId equals s.ProductId
                         select new { p.ProductId, p.Name, s.Amount }).ToList();
            var store = storeNew.Select(x => new ProductInStore()
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Amount = x.Amount
            }).ToList();
            return PartialView("_Store", store);
        }
        public IActionResult EditStore(int id)
        {
            SHOPContext db = new SHOPContext();
            Store product = db.Store.Where(x => x.ProductId == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public IActionResult EditStore(Store product)
        {
            SHOPContext db = new SHOPContext();
            Store prod = db.Store.Where(x => x.ProductId == product.ProductId).FirstOrDefault();
            prod.Amount = product.Amount;
            db.Entry(prod).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("/Index");
        }
        public IActionResult StoreSort()
        {
            SHOPContext db = new SHOPContext();
            var storeNew = (from p in db.Products
                            join s in db.Store
                            on p.ProductId equals s.ProductId
                            select new { p.ProductId, p.Name, s.Amount }).Where(x=>x.Amount<=10).ToList();
            var store = storeNew.Select(x => new ProductInStore()
            {
                ProductId = x.ProductId,
                Name = x.Name,
                Amount = x.Amount
            }).OrderBy(x=>x.Amount).ToList();
            return PartialView("_Store", store);
        }
    }
}