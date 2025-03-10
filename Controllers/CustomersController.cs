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
using Umbraco.Core.Models.Membership;
using Microsoft.AspNet.Identity;
using System.Diagnostics;





namespace Restaurant.Controllers
{


    public class CustomersController : Controller
    {
        //CustomerView xa = new CustomerView();   //根據類別建立物件

        private readonly ILogger<CustomersController> _logger;
        private readonly MyDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly PasswordHasher<string> _passwordHasher = new PasswordHasher<string>();  // 哈希        

        public CustomersController(ILogger<CustomersController> logger, MyDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        #region 註冊
        #endregion
        [AllowAnonymous] //允許匿名
        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // 註冊密碼位隱碼 //
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("CustomerId,CustomerName,CustomerPhone,CustomerEmail,CustomerPassword,CustomerAccount,CustomerAddress,CustomerCreatedAt")] CustomerView customer)
        {

            if (ModelState.IsValid)
            {
                // 判斷是否已經註冊過了
                bool isUserExists = _context.Customers.Any(m => m.CustomerEmail == customer.CustomerEmail);
                if (isUserExists)
                {
                    ModelState.AddModelError("CustomerEmail", "❌ 該 Email 已經被註冊過了");
                    return View(customer);  // 保留輸入的資料並返回表單
                    // return RedirectToAction(nameof(Create));  // 要告訴使用者帳好已存在
                }
                string? hashedPassword = _passwordHasher.HashPassword(null!, customer.CustomerPassword); // 加密
                Debug.WriteLine("註冊時雜湊密碼: " + hashedPassword);
                CustomerView customer1 = new CustomerView
                {
                    CustomerName = customer.CustomerName,   // 姓名 
                    CustomerPhone = customer.CustomerPhone,  // 電話
                    CustomerEmail = customer.CustomerEmail,  // email
                    CustomerAddress = customer.CustomerAddress, // 地址
                    //CustomerBirthDate = customer.CustomerBirthDate,  //  生日還沒寫進資料庫
                    CustomerAccount = customer.CustomerAccount, // 帳號
                    CustomerPassword = hashedPassword,       // 加密密碼

                };

                _context.Add(customer1);
                await _context.SaveChangesAsync();
                return RedirectToAction(actionName: "Member_Login");
            }
            return View(customer);
        }

        #region 登入
        #endregion
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Member_Login()
        {
            return View();
        }

        
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Member_Login(string CustomerAccount, string CustomerPassword)  // 參數問題
        {

            if (ModelState.IsValid)
            {
                CustomerView? result = (from a in _context.Customers
                                    where a.CustomerAccount == CustomerAccount
                                    select a).SingleOrDefault();
                if (result == null)
                {
                    ViewBag.noMember = "去註冊啦";      // 這裡還沒弄到頁面
                    return RedirectToAction(nameof(Create));  //若找不到傳回  (帳號不存在)
                }
                var valid = _passwordHasher.VerifyHashedPassword(null!, result.CustomerPassword, CustomerPassword.Trim());
                if (valid == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name,result.CustomerAccount),//帳號
                        new Claim("UserName",result.CustomerName),//帳號
                        //new Claim("UserStatus",result.status),//身分  (沒有身分)
                        new Claim("UserId",result.CustomerId.ToString()),//會員ID
                        //new Claim(ClaimTypes.Role,"Admin")
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true, // 記住登入狀態
                        ExpiresUtc = DateTime.UtcNow.AddSeconds(20) // 設定cookie 過期時間
                    };
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    //Debug.WriteLine("資料庫密碼: " + result.CustomerPassword);
                    return RedirectToAction("Index", "Customers");


                }
                else
                {
                    ViewBag.Error = "密碼錯誤，請重新輸入！";
                    Debug.WriteLine("資料庫密碼: " + result.CustomerPassword);
                    Debug.WriteLine("密碼雜湊: " + _passwordHasher.HashPassword(null!, CustomerPassword));
                    Debug.WriteLine("資料庫密碼: " + result.CustomerPassword);
                    Debug.WriteLine("資料庫密碼 (hashed): " + result.CustomerPassword);
                    Debug.WriteLine("使用者輸入密碼 (plain): " + CustomerPassword);
                    return View("Member_Login");

                }

            }
            ViewBag.Error = "請檢查輸入欄位是否正確！";
            return View("Member_Login");
        }

        #region 其他動作
        #endregion
        public IActionResult SomeAction()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Member_Login");
            }

            // 正常邏輯
            return View();
        }


        #region 會員專區
        #endregion
        // GET: Customers
        [Authorize]
        public async Task<IActionResult> Index()   // 會員專區葉面
        {
            // 使用 User.Identity.Name 來獲取當前登入的使用者帳號
            var customerAccount = User.Identity.Name;
            if (string.IsNullOrEmpty(customerAccount))
            {
                return RedirectToAction(nameof(Login));  // 如果沒有登入，則重導向登入頁面
            }
            CustomerView? member = await _context.Customers
        .Include(m => m.Orders)
        .FirstOrDefaultAsync(m => m.CustomerAccount == customerAccount);   // 假設有登入機制



            if (member == null)
            {
                return RedirectToAction(nameof(Create));
            }

            var viewModel = new CustomerView
            {
                CustomerAccount = member.CustomerAccount,
                CustomerName = member.CustomerName,
                CustomerPhone = member.CustomerPhone,
                CustomerEmail = member.CustomerEmail,
                CustomerAddress = member.CustomerAddress,
                Orders = member.Orders.Select(o => new OrderView
                {
                    OrderId = o.OrderId,
                    OrderDate = o.OrderDate,
                    OrderTotalAmount = o.OrderTotalAmount,
                   // Status = o.Status
                }).ToList()
            };
            //return View(await _context.Customers.ToListAsync());
            return View(member);
        }      
        






        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        

        [Authorize]
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
        public async Task<IActionResult> Edit(int id, [Bind("Customer_Id,CustomerName,CustomerPhone,CustomerEmail,CustomerPassword,CustomerBirthDate,CustomerAccount,CustomerPoints,CustomerAddress,CustomerCreatedAt")] CustomerView customer)
        {
            if (id != customer.CustomerId)
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
                    if (!CustomerExists(customer.CustomerId))
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
                .FirstOrDefaultAsync(m => m.CustomerId == id);
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
            return _context.Customers.Any(e => e.CustomerId == id);
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
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        #region Member_Register 沒在用
        #endregion 
        public IActionResult Member_Register()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Member_Register([Bind("Customer_Id,CustomerName,CustomerPhone,CustomerEmail,CustomerPassword,CustomerBirthDate,CustomerAccount,CustomerPoints,CustomerAddress,CustomerCreatedAt")] CustomerView customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        #region 登出
        #endregion
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Member_Login");
        }
        [HttpGet]
        public string noLogin()
        {
            return "未登入";
        }

    }
}
