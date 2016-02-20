using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public interface IUser
    {
        int Id { get; set; }
        [DisplayName("Voornaam")]
        [RegularExpression(@"(?i)[a-z]")]
        [StringLength(1,ErrorMessage = "Voornaam, mag maximum 100 karakters lang zijn.")]
        [Required]
        string FirstName { get; set; }
        [DisplayName("Achternaam")]
        [Required]
        //[RegularExpression(@"(?i)[a-z]")]
        [RegularExpression(@"[^a-zA-Z]")]
        string LastName { get; set; }
        [DisplayName("E-mail")]
        [RegularExpression(@"(?i)hogent\.be$")]
        [DataType(DataType.EmailAddress)]
        [Required]
        string EmailAddress { get; set; }
        IList<LearningUtilityDetails> GetLearningUtilities();
    }
}
