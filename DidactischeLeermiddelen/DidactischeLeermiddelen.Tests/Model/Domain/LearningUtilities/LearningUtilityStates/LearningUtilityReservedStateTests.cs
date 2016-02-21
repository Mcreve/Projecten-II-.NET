using System;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities.LearningUtilityStates
{
    [TestClass]
    public class LearningUtilityReservedStateTests
    {
        private LearningUtility learningUtility;
        private User student;
        private User lector;

        [TestInitialize]
        public void InitializeTest()
        {
            learningUtility = new LearningUtility();
            learningUtility.ToState(StateFactory.CreateState(StateType.Reserved, learningUtility));
            student = new Student();
            lector = new Lector();
            learningUtility.ReservedBy = student;
        }

        [TestMethod]
        public void CallToBlockUpdatesReservedByAndSetsStateToBlocked()
        {
            //Act
            learningUtility.Block(lector);

            //Assert
            Assert.AreEqual(lector, learningUtility.ReservedBy);
            Assert.IsNull(learningUtility.LendTo);
            Assert.IsInstanceOfType(learningUtility.CurrentState, typeof(Blocked));
        }

        [TestMethod]
        public void CallToMakeAvailableSetsReservedByToNullAndStateToAvailable()
        {
            //Act
            learningUtility.MakeAvailable();

            //Assert
            Assert.IsNull(learningUtility.ReservedBy);
            Assert.IsNull(learningUtility.LendTo);
            Assert.IsInstanceOfType(learningUtility.CurrentState, typeof(Available));
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CallToLateThrowsException()
        {
            //Act
            learningUtility.Late();
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CallToReserveThrowsException()
        {
            //Act
            learningUtility.Reserve(student);
        }
    }
}
