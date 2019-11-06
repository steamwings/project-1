using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YAB.Models;
using YAB.Models.Repos;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using YetAnotherBankWeb.Areas.Identity.Data;

namespace YetAnotherBankWeb.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly AccountsRepo _repo;
        private readonly Project1Context _context; //TODO Remove
        private readonly ILogger<AccountsController> _logger;
        private readonly UserManager<YABUser> _userManager;

        private string UID()
        {
            return _userManager.GetUserId(User);
        }

        public AccountsController(Project1Context context, UserManager<YABUser> userManager, ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _repo = new AccountsRepo(context);
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetQueryable(UID()).ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts
                .Include(a => a.Interest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // GET: Accounts/Create
        public IActionResult New()
        {
            ViewData["AccountTypeId"] = new SelectList(_context.AccountTypes, "Id", "Name");
            ViewData["InterestId_Checking"] = new SelectList(_context.InterestRates.Where(i => i.Type.Name == "Checking"), "Id", "Name");
            ViewData["InterestId_Loan"] = new SelectList(_context.InterestRates.Where(i => i.Type.Name == "Loan"), "Id", "Name");
            ViewData["InterestId_Investment"] = new SelectList(_context.InterestRates.Where(i => i.Type.Name == "Investment"), "Id", "Name");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewAccountViewModel account)
        {
            //ViewData["InterestId"] = new SelectList(_context.InterestRates.Where(i=>i.Type == account.Type), "Id", "Name");
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index)); //TODO Indicate error

            var type = _context.AccountTypes.Find(account.TypeId);
            if (type is null) return NotFound("type");
            Accounts a = new Accounts()
            {
                Balance = 0,
                Name = account.Name,
                Active = true,
                Business = account.Business,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Type = type,
                InterestId = account.InterestId
            };

            if (type.Name is null) return NotFound("name");
            switch (type.Name)
            {
                case "Checking":
                    break;
                case "Investment":
                    var years = _context.InterestRates.Find(account.InterestId).Years.Value;
                    a.TermAccounts.Add(new TermAccounts()
                    {
                        MaturationDate = DateTime.Now.AddYears(years)
                    });
                    break;
                case "Loan":
                    a.DebtAccounts.Add(new DebtAccounts()
                    {
                        PaymentAmount = 250.00M, //TODO Don't hardcode
                        PaymentsBehind = 0,
                        NextPaymentDue = DateTime.Now.AddMonths(1)
                    }) ;
                    break;
                default:
                    _logger.LogWarning($"Invalid account type '{account.Name}'.");
                    return RedirectToAction(nameof(Index)); // TODO Error
            }
            using(var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var userId = UID();
                    if (userId is null) return NotFound("uid null");
                    if (_context.Customers.Find(userId) is null) return NotFound("user not found");
                    Accounts added = _context.Accounts.Add(a).Entity;
                    await _context.SaveChangesAsync();
                    if (_context.Accounts.Find(added.Id) is null) return NotFound("no acct");
                    _context.CustomersToAccounts.Add(new CustomersToAccounts()
                    {
                        AccountId = added.Id,
                        CustomerId = UID()
                    });
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch(Exception e)
                {
                    _logger.LogError("Create account failed.",e);
                    throw e;
                }
            }
            return RedirectToAction(nameof(Index)); //View("Details", account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts.FindAsync(id);
            if (accounts == null)
            {
                return NotFound();
            }
            ViewData["InterestId"] = new SelectList(_context.InterestRates, "Id", "Name", accounts.InterestId);
            return View(accounts);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Balance,InterestId,Created,LastUpdated,Active,Business")] Accounts accounts)
        {
            if (id != accounts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accounts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountsExists(accounts.Id))
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
            ViewData["InterestId"] = new SelectList(_context.InterestRates, "Id", "Name", accounts.InterestId);
            return View(accounts);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _context.Accounts
                .Include(a => a.Interest)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounts == null)
            {
                return NotFound();
            }

            return View(accounts);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var accounts = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(accounts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountsExists(long id)
        {
            return _context.Accounts.Any(e => e.Id == id);
        }
    }
}
