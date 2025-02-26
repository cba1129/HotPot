using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Microsoft.Data.SqlClient;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;



namespace Restaurant.Controllers
{
    

    public class CustomersController : Controller
    {
        Customer xa = new Customer();   //根據類別建立物件

        private readonly ILogger<CustomersController> _logger;
        private readonly HotPotContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();  // 哈希        

        public CustomersController(ILogger<CustomersController> logger, HotPotContext context,
            IHttpContextAccessor httpContextAccessor )
        {
            _logger = logger;
            _context = context;            
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerCustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerCustomerId,CustomerName,CustomerPhone,CustomerEmail,CustomerPassword,CustomerBirthDate,CustomerAccount,CustomerPoints,CustomerAddress,CustomerCreatedAt")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CustomerCustomerId,CustomerName,CustomerPhone,CustomerEmail,CustomerPassword,CustomerBirthDate,CustomerAccount,CustomerPoints,CustomerAddress,CustomerCreatedAt")] Customer customer)
        {
            if (id != customer.CustomerCustomerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.CustomerCustomerId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerCustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerCustomerId == id);
        }


        // 自己加的
        public async Task<IActionResult> Index_Member(int? id)
        {
            //ViewBag.CName = _context.Customers.EntityType.Name;
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerCustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult Member_Login()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Member_Login(Customer user) {
            
            if (ModelState.IsValid) {
                var result = (from a in _context.Customers
                              where a.CustomerAccount == user.CustomerAccount
                              select a).SingleOrDefault();
                if (result == null)
                {
                    return RedirectToAction(nameof(Create));  //若找不到傳回  (帳號不存在)
                }
                var valid = _passwordHasher.VerifyHashedPassword(null!, result.CustomerPassword , user.CustomerPassword);
                if (valid == PasswordVerificationResult.Success) {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,result.CustomerName),//帳號
                        new Claim("UserName",result.CustomerName),//帳號
                        //new Claim("UserStatus",result.status),//身分  (沒有身分)
                        new Claim("UserId",result.CustomerCustomerId.ToString()),//會員ID


                        //new Claim(ClaimTypes.Role,"Admin")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Login));
        }
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    // 假設這是從資料庫讀取的帳號密碼（實際應該從 DB 查詢）
        //    string correctUsername = "admin@example.com";
        //    string correctPassword = "123456";
        //    string customerName = "Admin user";

        //    if (model.CustomerAccount == correctUsername && model.CustomerPassword == correctPassword)
        //    {
        //        // 記錄登入狀態
        //        Session["User"] = model.CustomerAccount;
        //        return RedirectToAction("Index", "Home");  // 登入成功導向首頁
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "❌ 帳號或密碼錯誤";
        //        return View(model);
        //    } }        

        //// 登出功能
        //public ActionResult Logout()
        //{
        //    Session.Clear();  // 清除 Session
        //    return RedirectToAction("Member_Login");
        
    


        public IActionResult Member_Register()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Member_Register([Bind("CustomerCustomerId,CustomerName,CustomerPhone,CustomerEmail,CustomerPassword,CustomerBirthDate,CustomerAccount,CustomerPoints,CustomerAddress,CustomerCreatedAt")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(customer);
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Member_Login));
        }
        [HttpGet]
        public string noLogin()
        {
            return "未登入";
        }

    }
}
