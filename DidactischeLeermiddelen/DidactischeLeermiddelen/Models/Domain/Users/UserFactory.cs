using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class UserFactory
    {
        public static IUser CreateUser(UserType type)
        {
            switch (type)
            {
                case UserType.Student:
                    return new Student();
                case UserType.Lector:
                    return new Lector();
                default:
                    return null;
            }
        }
    }
}