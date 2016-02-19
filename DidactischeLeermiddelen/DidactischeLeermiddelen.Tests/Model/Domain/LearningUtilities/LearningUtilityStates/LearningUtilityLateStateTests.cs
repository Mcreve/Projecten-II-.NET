using System;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities.LearningUtilityStates;
using DidactischeLeermiddelen.Models.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities.LearningUtilityStates
{
    [TestClass]
    public class LearningUtilityLateStateTests
    {
        private LearningUtility learningUtility;
        private IUser student;
        private IUser lector;

        [TestInitialize]
        public void InitializeTest()
        {
            learningUtility = new LearningUtility();
            learningUtility.ToState(StateFactory.CreateState(StateType.Late, learningUtility));
            student = new Student();
            lector = new Lector();
            learningUtility.LendTo = student;
        }

        [TestMethod]
        public void CallToReserveSetsReservedByWhenReservedByIsNull()
        {
            //Act
            learningUtility.Reserve(student);

            //Assert
            Assert.AreEqual(student, learningUtility.ReservedBy);
            Assert.AreEqual(student, learningUtility.LendTo);
            Assert.IsInstanceOfType(learningUtility.CurrentState, typeof(Late));
        }

        [TestMethod]
        public void CallToBlockSetsReservedByWhenReservedByIsNull()
        {
            //Act
            learningUtility.Block(lector);

            //Assert
            Assert.AreEqual(lector, learningUtility.ReservedBy);
            Assert.AreEqual(student, learningUtility.LendTo);
            Assert.IsInstanceOfType(learningUtility.CurrentState, typeof(Late));
        }

        [TestMethod]
        public void CallToBlockUpdatesReservedByWhenReservedByIsStudent()
        {
            //Arrange
            learningUtility.ReservedBy = student;

            //Act
            learningUtility.Block(lector);

            //Assert
            Assert.AreEqual(lector, learningUtility.ReservedBy);
            Assert.AreEqual(student, learningUtility.LendTo);
            Assert.IsInstanceOfType(learningUtility.CurrentState, typeof(Late));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallToBlockThrowsExceptionWhenReservedByIsLector()
        {
            //Arrange
            learningUtility.ReservedBy = lector;

            //Act
            learningUtility.Block(lector);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallToReserveThrowsExceptionWhenReservedByIsNotNull()
        {
            //Arrange
            learningUtility.ReservedBy = student;

            //Act
            learningUtility.Reserve(student);
        }

        [TestMethod]
        public void CallToMakeAvailableSetsReservedByToNullWhenReservedByIsNotNull()
        {
            //Arrange
            learningUtility.ReservedBy = student;

            //Act
            learningUtility.MakeAvailable();

            //Assert
            Assert.IsNull(learningUtility.ReservedBy);
            Assert.AreEqual(student, learningUtility.LendTo);
            Assert.IsInstanceOfType(learningUtility.CurrentState, typeof(Late));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CallToMakeAvailableThrowsExceptionWhenReservedByIsNull()
        {
            //Act
            learningUtility.MakeAvailable();
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidOperationException))]
        public void CallToLateThrowsException()
        {
            //Act
            learningUtility.Late();
        }
    }
}
