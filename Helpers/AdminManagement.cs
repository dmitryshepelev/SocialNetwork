using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SocialNetwork.Helpers
{
    public static class AdminManagement
    {
        public static bool UserLockoutChange(IdentityUser applicationUser, string lockoutEndDateString)
        {
            bool isLockoutEndDateChanged = false;
            DateTime? lockoutEndDate = DateConventor.ConvertFromString(lockoutEndDateString);
            if (applicationUser.LockoutEndDateUtc != lockoutEndDate)
            {
                if (lockoutEndDate == null)
                {
                    applicationUser.LockoutEndDateUtc = null;
                    applicationUser.LockoutEnabled = false;
                }
                else
                {
                    applicationUser.LockoutEndDateUtc = lockoutEndDate;
                    applicationUser.LockoutEnabled = true;
                }
                isLockoutEndDateChanged = true;
            }
            return isLockoutEndDateChanged;
        }
    }
}