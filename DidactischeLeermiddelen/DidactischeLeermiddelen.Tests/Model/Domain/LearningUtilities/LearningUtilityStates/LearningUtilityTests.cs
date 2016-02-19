using System;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities.LearningUtilityStates
{
    [TestClass]
    public class LearningUtilityTests
    {
        private LearningUtility learningUtility;
        private IUser student;
        private IUser lector;

        [TestInitialize]
        public void TestInitialize()
        {
            //Arrange
            learningUtility = new LearningUtility();
            learningUtility.ToState(StateFactory.CreateState(StateType.Available, learningUtility));
            student = new Student();
            lector = new Lector();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void CallToReserveAsLectorThrowsException()
        {
            //Act
            learningUtility.Reserve(lector);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void CallToBlockAsStudentThrowsException()
        {
            //Act
            learningUtility.Block(student);
        }
    }
}
