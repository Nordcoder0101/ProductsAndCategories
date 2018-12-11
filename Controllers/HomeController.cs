using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProdsAndCats.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
// using System.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.Controllers
{
  public class HomeController : Controller
  {
    private ProdsAndCatsContext dbContext;
    public HomeController(ProdsAndCatsContext context)
    {
      dbContext = context;
    }
        
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult ShowProducts()
        {
            List<Product> AllProducts = dbContext.Product.ToList(); 
            ViewBag.AllProducts = AllProducts;
            return View();
        }

        [HttpPost("PostProduct")]
        public IActionResult PostProduct(Product NewProduct)
        {
            if(ModelState.IsValid)
            {
            dbContext.Add(NewProduct);
            dbContext.SaveChanges();
            return RedirectToAction("ShowProducts");
            }
            return View("Index");

        }
    [HttpGet]
    [Route("/categories")]
    public IActionResult ShowCategories()
    {
      List<Category> AllCategories = dbContext.Category.ToList();
      ViewBag.AllCategories = AllCategories;
      return View();
    }

    [HttpPost("PostCategory")]
    public IActionResult PostCategory(Category NewCategory)
    {
      if (ModelState.IsValid)
      {
        dbContext.Add(NewCategory);
        dbContext.SaveChanges();
        return RedirectToAction("ShowCategories");
      }
      return View("ShowCategory");

    }

    [HttpGet("product/{id}")]
    public IActionResult ShowProduct(int id)
    {
        
        
        var ProductWithCategoryAndAssoc = dbContext.Product
        .Include(product => product.Associations)
        .ThenInclude(assoc => assoc.CategoryAssociated).FirstOrDefault(p => p.ProductId == id);

        var AllAssociations = dbContext.Association.ToList();
        var AllProducts = dbContext.Product.ToList();
        var AllCategories = dbContext.Category.ToList();

        List<Category> CategoriesProductDoesNotHave = new List<Category>();
        

        if (ProductWithCategoryAndAssoc.Associations.Count() == 0)
        {
          foreach (var c in AllCategories)
          {
            CategoriesProductDoesNotHave.Add(c);
          }
        }
        else
        {
        foreach (var p in ProductWithCategoryAndAssoc.Associations)
        {
        foreach (var a in AllCategories)
          {
            if (p.CategoryId != a.CategoryId)
            {
              CategoriesProductDoesNotHave.Add(a);
            }
          }
        }
        }
        ViewBag.CategoriesProductDoesNotHave = CategoriesProductDoesNotHave;
        ViewBag.ProductToShow = ProductWithCategoryAndAssoc;  
      
        return View();
    }
    [HttpPost("/PostAssociationForProduct")]
    public IActionResult CreateAssociation(Association NewAssociation)
    {
      dbContext.Add(NewAssociation);
      dbContext.SaveChanges();

      return RedirectToAction("ShowProduct", new {id = NewAssociation.ProductId});
    }
    

    [HttpGet("category/{id}")]
    public IActionResult ShowCategory(int id)
      {


        var CategoryWithProductAndAssoc = dbContext.Category
        .Include(category => category.Associations)
        .ThenInclude(assoc => assoc.CategoryAssociated).FirstOrDefault(p => p.CategoryId == id);

        var AllAssociations = dbContext.Association.ToList();
        var AllProducts = dbContext.Product.ToList();
        var AllCategories = dbContext.Category.ToList();

        List<Product>ProductsCategoryDoesNotHave = new List<Product>();


        if (CategoryWithProductAndAssoc.Associations.Count() == 0)
        {
          foreach (var c in AllProducts)
          {
            ProductsCategoryDoesNotHave.Add(c);
          }
        }
        else
        {
          foreach (var p in CategoryWithProductAndAssoc.Associations)
          {
            foreach (var a in AllProducts)
            {
              if (p.ProductId != a.ProductId)
              {
                ProductsCategoryDoesNotHave.Add(a);
              }
            }
          }
        }
        ViewBag.CategoriesProductDoesNotHave = ProductsCategoryDoesNotHave;
        ViewBag.CategoryToShow = CategoryWithProductAndAssoc;

        return View();
    }
    [HttpPost("/PostAssociationForCategory")]
    public IActionResult CreateAssociationForCategory(Association NewAssociation)
    {
      dbContext.Add(NewAssociation);
      dbContext.SaveChanges();

      return RedirectToAction("ShowCategory", new { id = NewAssociation.CategoryId });
    }
  }
}