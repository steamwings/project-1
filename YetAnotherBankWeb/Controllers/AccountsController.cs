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
using Microsoft.AspNetCore.Identity;
using YetAnotherBankWeb.Areas.Identity.Data;
using YAB.BusinessLayer;

namespace YetAnotherBankWeb.Controllers
{

    [Authorize]
    public class AccountsController : Controller
    {
        private readonly AccountsRepo _arepo;
        private readonly TransactionsRepo _trepo;
        private readonly Project1Context _context; //TODO Remove
        private readonly ILogger<AccountsController> _logger;
        private readonly UserManager<YABUser> _userManager;
        private readonly BL _bl;

        // TODO Move to parent/static class
        private string UID()
        {
            return _userManager.GetUserId(User);
        }

        public AccountsController(Project1Context context, UserManager<YABUser> userManager, ILogger<AccountsController> logger)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _arepo = new AccountsRepo(context); //TODO Remove
            _trepo = new TransactionsRepo(context); //TODO Remove
            _bl = new BL(context);
        }

        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _arepo.GetAll(UID()));
        }

        //TODO move some of this to BL?
        private TransactionsViewModel TVM(Transactions t, string accName)
        {
            string formatAcc(Accounts a) => (a is null || a.Name is null || a.Name == accName) ? "--" : a.Name;
            return new TransactionsViewModel()
            {
                Timestamp = t.Timestamp,
                Amount = t.Amount ?? 0,
                TypeName = _trepo.GetTypeName(t.TypeId) ?? "Unknown",
                Source = formatAcc(t.FromAccount),
                Destination = formatAcc(t.ToAccount)
            };
        }

        private DetailsAccountViewModel CreateDetailsVM(Accounts a, WithdrawDepositViewModel wdvm = null)
        {
            List<TransactionsViewModel> list = new List<TransactionsViewModel>();
            list.AddRange(a.OutgoingTransactions.AsQueryable().Select(t => TVM(t, a.Name)));
            list.AddRange(a.IncomingTransactions.AsQueryable().Select(t => TVM(t, a.Name)));
            var wdViewModel = (wdvm is null || wdvm.AccountId == 0) ? new WithdrawDepositViewModel() { AccountId = a.Id } : wdvm;
            var dvm = new DetailsAccountViewModel
            {
                Accounts = a,
                TransactionsVMs = list.OrderByDescending(t => t.Timestamp),
                WDViewModel = wdViewModel
            };
            return dvm;
        }
        
        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounts = await _arepo.Get(UID(), id.Value);
            if (accounts == null)
            {
                return NotFound();
            }
            ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
            ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
            return View("Details", CreateDetailsVM(accounts));
        }

        // TODO Move to business logic
        private async Task<IActionResult> WithdrawDeposit(WithdrawDepositViewModel vm, bool Deposit)
        {
            Accounts a;
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    a = await _arepo.Get(UID(), vm.AccountId);
                    var newBal = a.Balance + (Deposit ? vm.Amount : -vm.Amount);
                    if (newBal < 0 && !a.Business)
                    {
                        ModelState.AddModelError("Amount", "Insufficient funds. Negative balance prohibited.");
                        ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
                        ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
                        return View("Details", CreateDetailsVM(await _arepo.Get(UID(), vm.AccountId)));
                    }
                    a.Balance = newBal;
                    _trepo.Add(Deposit ? "Deposit" : "Withdrawal", vm.Amount, null, vm.AccountId);
                    _context.Update(a);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError("Transfer failed.", e);
                    throw e; // TODO Not this
                }
            }
            if (a is null) return View("Index");
            else
            {
                ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
                ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
                return View("Details", CreateDetailsVM(await _arepo.Get(UID(), vm.AccountId)));
            }
        }

        // POST: Accounts/Withdraw
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdraw(WithdrawDepositViewModel vm)
        {
            if (ModelState.IsValid)
            {
                // TODO BL to verify
                return await WithdrawDeposit(vm, false);
            }
            ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
            ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
            return View("Details", CreateDetailsVM(await _arepo.Get(UID(), vm.AccountId), vm));
        }

        // POST: Accounts/Deposit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(WithdrawDepositViewModel vm)
        {
            if (ModelState.IsValid)
            {
                return await WithdrawDeposit(vm, true);
            }
            ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
            ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
            return View("Details", CreateDetailsVM(await _arepo.Get(UID(), vm.AccountId), vm));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(PaymentViewModel vm)
        {
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Accounts a = await _arepo.Get(UID(), vm.AccountId);
                        Accounts debt = await _arepo.Get(UID(), vm.DebtAccountId);
                        if (debt.Balance == 0)
                        {
                            ModelState.AddModelError("", "Debt has been paid off!");
                            ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
                            ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
                            return View("Details", CreateDetailsVM(await _arepo.Get(UID(), vm.AccountId)));
                        }
                        var paymentAmt = debt.DebtAccounts.Single().PaymentAmount;
                        var amt = (paymentAmt > debt.Balance) ? debt.Balance : paymentAmt; 
                        var newBal = a.Balance - amt;
                        if (newBal < 0 && !a.Business)
                        {
                            ModelState.AddModelError("AccountId", "Insufficient funds in this account.");
                            ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
                            ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
                            return View("Details", CreateDetailsVM(await _arepo.Get(UID(), vm.AccountId)));
                        }
                        a.Balance = newBal;
                        debt.Balance += amt;
                        if (debt.DebtAccounts.Single().PaymentsBehind > 0) debt.DebtAccounts.Single().PaymentsBehind--;
                        else if (debt.Balance != 0) debt.DebtAccounts.Single().NextPaymentDue = debt.DebtAccounts.Single().NextPaymentDue.AddMonths(1);
                        _trepo.Add("Payment", amt, vm.AccountId, vm.DebtAccountId);
                        _context.Update(a);
                        _context.Update(debt);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError("Payment failed.", e);
                        throw e; // TODO Not this
                    }
                }
            }
            ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
            ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
            return View("Details", CreateDetailsVM(await _arepo.Get(UID(), vm.DebtAccountId)));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewAccountViewModel account)
        {
            if (!ModelState.IsValid)
            {
                ViewData["AccountTypeId"] = new SelectList(_context.AccountTypes, "Id", "Name");
                ViewData["InterestId_Checking"] = new SelectList(_context.InterestRates.Where(i => i.Type.Name == "Checking"), "Id", "Name");
                ViewData["InterestId_Loan"] = new SelectList(_context.InterestRates.Where(i => i.Type.Name == "Loan"), "Id", "Name");
                ViewData["InterestId_Investment"] = new SelectList(_context.InterestRates.Where(i => i.Type.Name == "Investment"), "Id", "Name");
                return View("New");
            }

            var type = _context.AccountTypes.Find(account.TypeId);
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

            switch (type.Name)
            {
                case "Checking":
                    break;
                case "Investment":
                    a.Balance = account.Amount.Value;
                    var years = _context.InterestRates.Find(account.InterestId).Years.Value;
                    a.TermAccounts.Add(new TermAccounts()
                    {
                        MaturationDate = DateTime.Now.AddYears(years),
                    });
                    break;
                case "Loan":
                    a.Balance = -account.Amount.Value;
                    a.DebtAccounts.Add(new DebtAccounts()
                    {
                        PaymentAmount = 250.00M, //TODO Don't hardcode
                        PaymentsBehind = 1,
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
                    throw e; // Not this
                }
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: Accounts/Transfer
        [HttpGet]
        public async Task<IActionResult> Transfer()
        {
            if(await _arepo.Count(UID()) < 2) //TODO Notify user of what's wrong
                return RedirectToAction(nameof(Index));
            //GetModelState();
            ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
            ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
            return View();
        }
       
        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoTransfer(TransferViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
                ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
                return View("Transfer");
                //SaveModelState();
                //return RedirectToAction(nameof(Transfer));
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    //TODO Remove Business logic
                    Accounts into = _context.Accounts.Find(vm.AccountToId);
                    Accounts outof = _context.Accounts.Find(vm.AccountFromId);
                    var newBal = outof.Balance - vm.Amount;
                    if (vm.Amount < 0 || (newBal < 0 && !into.Business))
                    {
                        ModelState.AddModelError("Amount", "Insufficient funds.");
                        //SaveModelState();
                        //return RedirectToAction(nameof(Transfer));
                        ViewData["CheckingAccounts"] = new SelectList(await _bl.GetChecking(_context, UID()), "Id", "Name");
                        ViewData["Accounts"] = new SelectList(await _arepo.GetAll(UID()), "Id", "Name");
                        return View("Transfer");
                    }
                    outof.Balance = newBal;
                    into.Balance += vm.Amount;
                    _trepo.Add("Transfer", vm.Amount, vm.AccountFromId, vm.AccountToId);
                    _context.Update(into);
                    _context.Update(outof);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch(Exception e)
                {
                    _logger.LogError("Transfer failed.", e);
                    throw e; // TODO Not this
                }
            }

            return RedirectToAction(nameof(Index));
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
                    if (!_arepo.Exists(UID(),accounts.Id))
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


        private void ClearModelState()
        {
            TempData["ModelState"] = null;
        }
        //TODO Move to parent/static class
        private void SaveModelState()
        {
            if (ModelState.IsValid) return;
            //Dictionary<string, (object, string, string)> values = new Dictionary<string, (object,string,string)>();
            Dictionary<string, string> errors = new Dictionary<string, string>();
            foreach (var kv in ModelState)
            {
                //values[kv.Key] = (kv.Value.RawValue,kv.Value.AttemptedValue, kv.Value.Errors.FirstOrDefault()?.ErrorMessage);
                errors[kv.Key] = kv.Value.Errors.FirstOrDefault()?.ErrorMessage;
            }
            TempData["ModelState"] = errors;
        }

        // TODO Move to parent/static class
        private void GetModelState()
        {
            if (TempData["ModelState"] is null) return;
            //var d = (Dictionary<string,(object,string,string)>) TempData["ModelState"];
            var d = (Dictionary<string, string>)TempData["ModelState"];
            foreach (var key in d.Keys)
            {
                //(var raw, var attempt, var e) = d[key];
                //ModelState.SetModelValue(key, raw, attempt);
                if (!string.IsNullOrEmpty(d[key])) ModelState.AddModelError(key, d[key]);
            }
        }
    }
}
