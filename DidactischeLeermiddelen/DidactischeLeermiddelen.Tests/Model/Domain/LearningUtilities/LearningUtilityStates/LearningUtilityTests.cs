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
        private User student;
        private User lector;

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

        [TestMethod]
        public void SettingTimeHandedOutNotLateDoesntChangeState()
        {
            //Arrange
            learningUtility.ToState(StateFactory.CreateState(StateType.HandedOut, learningUtility));

            //Act
            learningUtility.TimeHandedOut = DateTime.Now;

            //Assert
            Assert.AreEqual(StateType.HandedOut, learningUtility.StateType);
        }

        [TestMethod]
        public void SettingTimeHandedOutLateSetsStateToLate()
        {
            //Arrange
            learningUtility.ToState(StateFactory.CreateState(StateType.HandedOut, learningUtility));

            //Act
            learningUtility.TimeHandedOut = DateTime.Now.AddDays(-8);

            //Assert
            Assert.AreEqual(StateType.Late, learningUtility.StateType);
        }
    }
}
