
using System.ComponentModel.DataAnnotations;

namespace DidactischeLeermiddelen.Models.Domain.Enums
{
    public enum Availability
    {
        [Display(Name = "Beschikbaar")]
        Available,
        [Display(Name = "Onbeschikbaar")]
        Unavailable,
        [Display(Name = "Gereserveerd")]
        Reserved,
        [Display(Name = "Geblockeerd")]
        Blocked

    }
}