using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebSalesMVC.Models;
using WebSalesMVC.Models.ViewModels;
using WebSalesMVC.Services;
using WebSalesMVC.Services.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace WebSalesMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            bool isNameValid = Validator.ValidateProperty(seller, nameof(Seller.Name));
            bool isEmailValid = Validator.ValidateProperty(seller, nameof(Seller.Email));
            bool isBirthDateValid = Validator.ValidateProperty(seller, nameof(Seller.BirthDate));
            bool isBaseSalaryValid = Validator.ValidateProperty(seller, nameof(Seller.BaseSalary));

            bool isValid = isNameValid && isEmailValid && isBirthDateValid && isBaseSalaryValid;

            if (!isValid) { return View(new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() }); }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var x = await _sellerService.FindByIdAsync(id.Value);
            if (x == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(x);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var x = await _sellerService.FindByIdAsync(id.Value);
            if (x == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }

            return View(x);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return RedirectToAction(nameof(Error), new { message = "Id not provided" });

            var x = await _sellerService.FindByIdAsync(id.Value);
            if (x == null) return RedirectToAction(nameof(Error), new { message = "Id not found" });

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel() { Seller = x, Departments = departments };
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (id != seller.Id) return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            try
            {
                bool isNameValid = Validator.ValidateProperty(seller, nameof(Seller.Name));
                bool isEmailValid = Validator.ValidateProperty(seller, nameof(Seller.Email));
                bool isBirthDateValid = Validator.ValidateProperty(seller, nameof(Seller.BirthDate));
                bool isBaseSalaryValid = Validator.ValidateProperty(seller, nameof(Seller.BaseSalary));

                bool isValid = isNameValid && isEmailValid && isBirthDateValid && isBaseSalaryValid;

                if (!isValid) { return View(new SellerFormViewModel { Seller = seller, Departments = await _departmentService.FindAllAsync() }); }

                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (NotFoundException x)
            {
                return RedirectToAction(nameof(Error), new { message = x.Message});
            }
            catch (DbConcurrencyException x)
            {
                return RedirectToAction(nameof(Error), new { message = x.Message }); 
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }

        

    }


}
