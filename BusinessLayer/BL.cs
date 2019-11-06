using System;
using System.Collections.Generic;
using System.Text;
using YAB.Models;

namespace YAB.BusinessLayer
{
    public static partial class BL
    {
        private static Project1Context _context = null;

        public static void Initialize(Project1Context c)
        {
            _context = c;
        }

        public static Accounts CreateAccount(NewAccountViewModel vm)
        {
            if (_context is null) return null;
            Accounts a = new Accounts();
            
            return a;
        }
    }
}
