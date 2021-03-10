using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TinjureTea.Helper;
using TinjureTea.Models;

namespace TinjureTea.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizationFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Login()
        {
            

            return View();
        }


        //public JsonResult CheckLogin(string username, string password)
        //{
        //    TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
        //    string md5StringPassword = AppHelper.GetMd5Hash(password);
        //    var dataItem = db.Users.Where(x => x.Username == username && x.Password == md5StringPassword).SingleOrDefault();
        //    bool isLogged = true;
        //    if (dataItem != null)
        //    {
        //        Session["Username"] = dataItem.Username;
        //        Session["Role"] = dataItem.Role;
        //        isLogged = true;
        //    }
        //    else
        //    {
        //        isLogged = false;
        //    }
        //    return Json(isLogged, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult CheckLogin(string username, string password)
       {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataItem = db.Users.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
            bool isLogged = true;
            if (dataItem != null)
            {
                Session["Username"] = dataItem.Username;
                Session["Role"] = dataItem.Role;
                isLogged = true;
            }
            else
            {
                isLogged = false;
            }
            return Json(isLogged, JsonRequestBehavior.AllowGet);
        }

        [AuthorizationFilter]
        public ActionResult AccessDenied()
        {
            return View();
        }


        [AuthorizationFilter]
        public ActionResult UserCreate()
        {
            return View();
        }


        [HttpPost]
        public JsonResult SaveUser(User user)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            bool isSuccess = true;
            if (user.UserId > 0)
            {
                db.Entry(user).State = EntityState.Modified;
            }
            else
            {
                user.Status = 1;
                user.Password = AppHelper.GetMd5Hash(user.Password);
                db.Users.Add(user);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }
        


        [HttpGet]
        public JsonResult GetAllUser()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.Users.Where(x => x.Status == 1).ToList();
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Delete(int UserId)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.Users.Where(x => x.UserId == UserId).FirstOrDefault();
            db.Users.Remove(dataList);
            db.SaveChanges();
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Role"] = null;
            return RedirectToAction("Login");
        }


        [AuthorizationFilter]
        public ActionResult Category()
        {
            return View();
        }


        //save category
        [HttpPost]
        public JsonResult SaveCategory(Category cat)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            bool isSuccess = true;
            if (cat.CategoryId > 0)
            {
                db.Entry(cat).State = EntityState.Modified;
            }
            else
            {
                cat.Status = 1;
                db.Categories.Add(cat);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }
        //to list data
        [HttpGet]
        public JsonResult GetAllCategory()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.Categories.Where(x => x.Status == 1).ToList();
            var data = dataList.Select(x => new {
                CategoryId = x.CategoryId,
                Name = x.Name,
                Status = x.Status
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //product
        [AuthorizationFilter]
        public ActionResult Product()
        {
            return View();
        }

        //saveproduct
        [HttpPost]
        public JsonResult SaveProduct(Product product)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            bool isSuccess = true;

            if (product.ProductId > 0)
            {
                db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                db.Products.Add(product);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllProduct()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.Products.Where(x => x.Status == 1).ToList();
            var data = dataList.Select(x => new {
                ProductId = x.ProductId,
                Name = x.Name,
                CategoryId = x.CategoryId,
                Status = x.Status
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
       

        //Sales
        [AuthorizationFilter]
        public ActionResult Sales()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveSales(Sale sales)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            bool isSuccess = true;
            if (sales.SalesId > 0)
            {
                db.Entry(sales).State = EntityState.Modified;
            }
            else
            {
                sales.Status = 1;
                db.Sales.Add(sales);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllSales()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.Sales.Where(x => x.Status == 1).ToList();
            var data = dataList.Select(x => new {
                SalesId = x.SalesId,
                OrderNo = x.OrderNo,
                CustomerName = x.CustomerName,
                CustomerPhone = x.CustomerPhone,
                CustomerAddress = x.CustomerAddress,
                OrderDate = x.OrderDate,
                PaymentMethod = x.PaymentMethod,
                TotalAmount = x.TotalAmout,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                Status = x.Status
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [AuthorizationFilter]
        public ActionResult Batch()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveBatch(Batch batch)
        {
            TinjureTea.Helper.AppHelper.ReturnMessage retMessage = new AppHelper.ReturnMessage();
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            retMessage.IsSuccess = true;

            if (batch.BatchId > 0)
            {
                db.Entry(batch).State = EntityState.Modified;
                retMessage.Messagae = "Update Success!";
            }
            else
            {
                batch.BatchName = batch.BatchName + db.Batches.Count();
                var batchData = db.Batches.Where(x => x.BatchName.Equals(batch.BatchName)).SingleOrDefault();
                if (batchData == null)
                {
                    db.Batches.Add(batch);
                    retMessage.Messagae = "Save Success!";
                }
                else
                {
                    retMessage.IsSuccess = false;
                    retMessage.Messagae = "This batch already exist!Please refresh and again try!";
                }
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                retMessage.IsSuccess = false;
            }

            return Json(retMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllBatch()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.Batches.ToList();
            var modefiedData = dataList.Select(x => new
            {
                BatchId = x.BatchId,
                BatchName = x.BatchName,
            }).ToList();
            return Json(modefiedData, JsonRequestBehavior.AllowGet);
        }
        [AuthorizationFilter]
        public ActionResult ProductStock()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveProductStock(ProductStock stock)
        {
            TinjureTea.Helper.AppHelper.ReturnMessage retMessage = new AppHelper.ReturnMessage();
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            retMessage.IsSuccess = true;

            if (stock.ProductQtyId > 0)
            {
                db.Entry(stock).State = EntityState.Modified;
                retMessage.Messagae = "Update Success!";
            }
            else
            {
                db.ProductStocks.Add(stock);
                retMessage.Messagae = "Save Success!";
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                retMessage.IsSuccess = false;
            }

            return Json(retMessage, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetAllProductStocks()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.ProductStocks.Include("Product").Include("Batch").ToList();
            var modefiedData = dataList.Select(x => new
            {
                ProductQtyId = x.ProductQtyId,
                ProductId = x.ProductId,
                ProductName = x.Product.Name,
                Quantity = x.Quantity,
                BatchId = x.BatchId,
                BatchName = x.Batch.BatchName,
                PurchasePrice = x.PurchasePrice,
                SalesPrice = x.SalesPrice
            }).ToList();
            return Json(modefiedData, JsonRequestBehavior.AllowGet);
        }

        //distributor
        [AuthorizationFilter]
        public ActionResult Distributor()
        {
            return View();
        }
        [HttpPost]
        public JsonResult SaveDistributor(Distributor dist)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            bool isSuccess = true;
            if (dist.DistributorId > 0)
            {
                db.Entry(dist).State = EntityState.Modified;
            }
            else
            {
                dist.Status = 1;
                db.Distributors.Add(dist);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllDistributor()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.Distributors.Where(x => x.Status == 1).ToList();
            var data = dataList.Select(x => new {
                DistributorId = x.DistributorId,
                Name = x.Name,
                PhoneNo =x.PhoneNo,
                Email=x.Email,
                State=x.State,
                District=x.District,
                Address=x.Address,
                Status = x.Status
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //retailer
        [AuthorizationFilter]
        public ActionResult Retailer()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveRetailer(Retailer reta)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            bool isSuccess = true;
            if (reta.RetailerId > 0)
            {
                db.Entry(reta).State = EntityState.Modified;
            }
            else
            {
                reta.Status = 1;
                db.Retailers.Add(reta);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllRetailer()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.Retailers.Where(x => x.Status == 1).ToList();
            var data = dataList.Select(x => new {
                RetailerId = x.RetailerId,
                Name = x.Name,
                PhoneNo = x.PhoneNo,
                Email = x.Email,
                State = x.State,
                District = x.District,
                Address = x.Address,
                Status = x.Status
               
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [AuthorizationFilter]
        public ActionResult Transaction()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveTransaction(Transaction t)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            bool isSuccess = true;
            if (t.TransactionId > 0)
            {
                db.Entry(t).State = EntityState.Modified;
            }
            else
            {
                t.Status = 1;
                db.Transactions.Add(t);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllTransaction()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            
            var dataList = db.Transactions.Where(x => x.Status == 1).ToList();
            var data = dataList.Select(x => new {
                TransactionId = x.TransactionId,
                DistributorId = x.DistributorId,
                RetailerId=x.RetailerId,
                Date=x.Date,
                Balance=x.Balance,
                ProductId=x.ProductId,
                SalesId=x.SalesId,
                Status = x.Status
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [AuthorizationFilter]
        public ActionResult SalesDetail()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveDetails(SalesDetail sd)
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            bool isSuccess = true;
            if (sd.SalesDetailId > 0)
            {
                db.Entry(sd).State = EntityState.Modified;
            }
            else
            {
                sd.Status = 1;
                db.SalesDetails.Add(sd);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllDetails()
        {
            TrinjureTeaEntities1 db = new TrinjureTeaEntities1();
            var dataList = db.SalesDetails.Where(x => x.Status == 1).ToList();
            var data = dataList.Select(x => new {
                SalesDetailId = x.SalesDetailId,
                SalesId = x.SalesId,
                ProductId = x.ProductId,
                UnitPrice = x.UnitPrice,
                Quantity = x.Quantity,
                Total=x.Total,
                CreatedBy=x.CreatedBy,
                CreatedOn=x.CreatedOn,
                Status = x.Status
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        

    }
}