using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DidactischeLeermiddelen.Models.Domain.Users
{
    public class UserFactory
    {
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
                                                       "Error: Parameter is not a type of the UserType Enum" );
            }
        }
        public static UserType DetermineUserTypeByEmailAddress(string emailAddress)
        {
            //Match Student
            Regex regex = new Regex(@"(?i)student\.hogent\.be$");
            Match match = regex.Match(emailAddress);
            if (match.Success)
                return  UserType.Student;

            //Match Lector
            regex = new Regex(@"(?i)hogent\.be$");
            match = regex.Match(emailAddress);
            if (match.Success)
                return UserType.Lector;

            //match nothing
                return UserType.Invalid;
        }
    }
}