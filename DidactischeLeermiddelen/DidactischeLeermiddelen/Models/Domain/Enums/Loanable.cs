using System.ComponentModel.DataAnnotations;

namespace DidactischeLeermiddelen.Models.Domain.Enums
{
    public enum Loanable
    {
        [Display(Name = "Uitleenbaar")]
        Loanable,
        [Display(Name = "Niet-uitleenbaar")]
        UnLoanable
    }
}