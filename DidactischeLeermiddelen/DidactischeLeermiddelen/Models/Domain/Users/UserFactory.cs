using System;
using System.Text.RegularExpressions;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class UserFactory
    {
        #region Methods

        /// <summary>
        ///     Creates a user based on the user type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>User object (Student/Lector)</returns>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static User CreateUser(UserType type)
        {
            switch (type)
            {
                case UserType.Student:
                    return new Student();
                case UserType.Lector:
                    return new Lector();
                default:
                    throw new IndexOutOfRangeException("Class: UserFactory \n" +
                                                       "Function: CreateUser \n " +
                                                       "Error: Parameter is not a type of the UserType Enum");
            }
        }

        /// <summary>
        ///     Determines the type of the user based on his/hers e-mailaddress
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <returns>the usertype of the possible user</returns>
        public static UserType DetermineUserTypeByEmailAddress(string emailAddress)
        {
            //Match Student
            var regex = new Regex(@"(?i)student\.hogent\.be$");
            var match = regex.Match(emailAddress);
            if (match.Success)
                return UserType.Student;

            //Match Lector
            regex = new Regex(@"(?i)hogent\.be$");
            match = regex.Match(emailAddress);
            if (match.Success)
                return UserType.Lector;

            //match nothing
            return UserType.Invalid;
        }

        #endregion
    }
}