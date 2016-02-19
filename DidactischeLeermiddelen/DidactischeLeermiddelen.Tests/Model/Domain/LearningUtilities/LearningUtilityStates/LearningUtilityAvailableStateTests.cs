using System;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities.LearningUtilityStates
{
    [TestClass]
    public class LearningUtilityAvailableStateTests
    {
        private LearningUtility learningUtility;
        private IUser student;
        private IUser lector;

        [TestInitialize]
        public void InitializeTest()
        {
            learningUtility = new LearningUtility();
            learningUtility.ToState(StateFactory.CreateState(StateType.Available, learningUtility));
            student = new Student();
            lector = new Lector();
        }

        [TestMethod]
        public void CallReserveSetsReservedByAndSetsStateToReserverd()
        {
            //Act
            learningUtility.Reserve(student);

            //Assert
            Assert.AreEqual(student, learningUtility.ReservedBy);
            Assert.IsNull(learningUtility.LendTo);
            Assert.IsInstanceOfType(learningUtility.CurrentState, typeof(Reserved));
        }

        [TestMethod]
        public void CallBlockSetsReservedByAndSetsStateToReserved()
        {
            //Act
            learningUtility.Block(lector);

            //Assert
            Assert.AreEqual(lector, learningUtility.ReservedBy);
            Assert.IsNull(learningUtility.LendTo);
            Assert.IsInstanceOfType(learningUtility.CurrentState, typeof(Blocked));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallLateThrowsException()
        {
            //Act
            learningUtility.Late();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallMakeAvailableThrowsException()
        {
            //Act
            learningUtility.MakeAvailable();
        }
    }
}
