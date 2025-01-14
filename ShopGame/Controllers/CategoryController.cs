﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopGame.Models;
using System.Net;
using System.Data.Entity;
using PagedList;

namespace ShopGame.Controllers
{
    public class CategoryController : Controller
    {
        GameShopEntities db = new GameShopEntities();
        // GET: Category
        public ActionResult Index(int ? page)
        {
            int pageNumber = page ?? 1;
            int pageSize = 5;
            var catList = db.Categories.OrderBy(x => x.Name).ToPagedList(pageNumber, pageSize);
            return View(catList);
            //return View(db.Categories.OrderBy(x => x.Name).ToList());
        }

        public PartialViewResult CategoryPartial()
        {
            var categoryList = db.Categories.OrderBy(x => x.Name).ToList();
            return PartialView(categoryList);
        }


        //Get: Category/Create
        public ActionResult Create()
        {
            return View();
        }

        //Post: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include ="CategoryID,Name")] Category category)
        {
            if(ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //Get: Category/Edit/1
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if(category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        //Post: Category/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="CategoryId,Name")] Category category)
        {
            if(ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }


        // Get: Category/Details/1
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                  return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if(category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }


        //POST: Category/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeteleConfirmed(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}