using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Management_Common.Common
{
    public class PasswordHelper
    {
        public static string HashPassword(string hashedPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(hashedPassword);
        }

        public static bool VerifiedPassword(string password, string hasedPassword)
        {
            if (password == null)
            {
                throw new ArgumentNullException("password is wrong");
            }
            return BCrypt.Net.BCrypt.Verify(password, hasedPassword);
        }
    }
}
