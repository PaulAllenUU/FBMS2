using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using FBMS2.Core.Models;
using FBMS2.Core.Services;
using FBMS2.Core.Security;
using FBMS2.Web.ViewModels;
using FBMS2.Data.Services;
using FBMS2.Data.Repositories;

namespace FBMS2.Web.Controllers
{
    [Authorize]
    //stock controller inherits the properties from the base controller
    public class StockController : BaseController
    {
        private readonly IUserService service;

        public StockController(IUserService ss)
        {
            service = ss;
        }

        public IActionResult Index()
        {
            var stock = service.GetAllStock();

            return View(stock);
        }

        //GET - get a stock item by using the id of the stock item
        public IActionResult Details(int id)
        {
            var stockitem = service.GetStockById(id);
            if (stockitem == null)
            {
                Alert ("Item Not Found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            return View(stockitem);
        }

        //display blank form to create the item of stock
        public IActionResult Create()
        {
            //return a blank form for the user to fill out and add stock
            return View();
        }

        //POST - Create the item of stock
        [HttpPost]
        public IActionResult Create([Bind("UserId, Description, Quantity, ExpiryDate")] Stock s)
        {
            if(ModelState.IsValid)
            {
                s = service.AddStock(s.UserId, s.Description, s.Quantity, s.ExpiryDate);

                Alert($"Stock item added successfully", AlertType.success);

                return RedirectToAction(nameof(Details));
            }

            return View(s);

        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            var s = service.GetStockById(id);

            if (s == null)
            {
                Alert($"Stock {id} not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            return View(s);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id, Description, Quantity, ExpiryDate")] Stock s)
        {
            if(ModelState.IsValid)
            {
                service.UpdateStock(s);
                Alert("Stock updated successfully", AlertType.info);

                return RedirectToAction(nameof(Details), new { Id = s.Id});
            }

            return View(s);
        }

        public IActionResult Delete(int id)
        {
            var s = service.GetStockById(id);

            if (s == null)
            {
                Alert($"Vehicle{id} not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            return View(s);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            service.DeleteStockById(id);

            Alert("Vehicle deleted successfully", AlertType.info);

            return RedirectToAction(nameof(Index));
        }
    }
}
