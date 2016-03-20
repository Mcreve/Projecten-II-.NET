using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using DidactischeLeermiddelen.Models.Domain.Users;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidactischeLeermiddelen.Models.Domain
{
    public interface IReservationRepository
    {
        IQueryable<Reservation> FindAll();
        Reservation FindBy(int id);
        IQueryable<Reservation> FindAllForUser(string userId);
        void Delete(Reservation reservation);
        void SaveChanges();

    }
}
