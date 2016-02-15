using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        /// <summary>
        /// Fetches the role of the person who is registering. 
        /// </summary>
        /// <param name="emailAdres"></param>
        /// <returns>
        /// Depends on how the email adres ends:
        /// student : student.hogent.be
        /// lector  : hogent.be
        /// null    : if none matches (anonymous)
        /// </returns>
        public string GetRole(string emailAdres)
        {
            //Match Student
            Regex regex = new Regex(@"(?i)student\.hogent\.be$");
            Match match = regex.Match(emailAdres);

            if (match.Success)
                return "student";
            //Match Lector
            regex = new Regex(@"(?i)hogent\.be$");
            match = regex.Match(emailAdres);
            if (match.Success)
                return "lector";
            //match nothing
            return null;

        }
}
    
}