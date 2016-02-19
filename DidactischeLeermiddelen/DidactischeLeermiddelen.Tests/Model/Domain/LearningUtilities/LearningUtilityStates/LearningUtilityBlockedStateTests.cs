using System;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities.LearningUtilityStates
{
    [TestClass]
    public class LearningUtilityBlockedStateTests
    {
        private LearningUtility learningUtility;
        private IUser student;
        private IUser lector;

        [TestInitialize]
        public void InitializeTest()
        {
            learningUtility = new LearningUtility();
            learningUtility.ToState(StateFactory.CreateState(StateType.Blocked, learningUtility));
            student = new Student();
            lector = new Lector();
            learningUtility.ReservedBy = lector;
        }

        [TestMethod]
        public void CallToMakeAvailableSetsReservedByToNullAndStateToAvailable()
        {
            //Act
            learningUtility.MakeAvailable();

            //Assert
            Assert.IsNull(learningUtility.ReservedBy);
            Assert.IsNull(learningUtility.LendTo);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CallToReserveThrowsException()
        {
            //Act
            learningUtility.Reserve(student);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CallToLateThrowsException()
        {
            //Act
            learningUtility.Late();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallToBlockThrowsException()
        {
            //Act
            learningUtility.Block(lector);
        }
    }
}
