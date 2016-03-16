using DidactischeLeermiddelen.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using System.Data.Entity;

namespace DidactischeLeermiddelen.Models.DAL
{
    public class ReservationRepository : IReservationRepository
    {
        private LeermiddelenContext context;
        private DbSet<Reservation> reservations;

        public ReservationRepository(LeermiddelenContext context)
        {
            this.context = context;
            this.reservations = context.Reservations;
        }

        public void Delete(Reservation reservation)
        {
            reservations.Remove(reservation);
        }

        public IQueryable<Reservation> FindAll()
        {
            return reservations;
        }

        public IQueryable<Reservation> FindAllForUser(string userId)
        {
            return reservations.Where(res => res.User.EmailAddress == userId);
        }

        public Reservation FindBy(int id)
        {
            return reservations.FirstOrDefault(res => res.Id == id);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}