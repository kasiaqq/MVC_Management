using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProduct(Products product)
        {
            SHOPContext db = new SHOPContext();
            db.Products.Add(product);
            db.SaveChanges();
            return RedirectToAction("/Index");
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

    }
}