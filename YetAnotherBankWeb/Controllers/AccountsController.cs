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

namespace YetAnotherBankWeb.Controllers
{
    [Authorize]
    public class AccountsController : Controller
    {
        private readonly AccountsRepo _repo;
        private readonly Project1Context _context; //TODO Remove
        private readonly ILogger<AccountsController> _logger;

        public AccountsController(ILogger<AccountsController> logger, Project1Context context)
        {
            _logger = logger;
            _context = context;
            _repo = new AccountsRepo(context);
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetQueryable().ToListAsync());
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
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New([Bind("Name,Business,Type")] NewAccountViewModel account)
        {
            ViewBag.TypeName = account.Type.Name;
            //if (_context.AccountTypes.Contains(account.Type))
            //{
                ViewData["InterestId"] = new SelectList(_context.InterestRates.Where(i=>i.Type == account.Type), "Id", "Name");
                return View("Create", account);
            //}
            //return RedirectToAction(nameof(Index));
            //return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InterestId,LoanAmount,TermYears")] CreateAccountViewModel account)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index)); //TODO Indicate error

            Accounts a = new Accounts()
            {
                Balance = 0,
                Name = account.Name,
                Active = true,
                Business = account.Business,
                Created = DateTime.Now,
                LastUpdated = DateTime.Now,
                Type = account.Type,
                InterestId = account.InterestId
            };
            switch (account.Name)
            {
                case "Checking":
                    break;
                case "Investment":
                    a.TermAccounts.Add(new TermAccounts() { /*TODO*/ });
                    break;
                case "Loan":
                    a.DebtAccounts.Add(new DebtAccounts() { /*TODO*/ });
                    break;
                default:
                    _logger.LogWarning($"Invalid account type '{account.Name}'.");
                    return RedirectToAction(nameof(Index)); // TODO Error
            }

            _context.Accounts.Add(a);
            await _context.SaveChangesAsync();
            return View(account);
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
