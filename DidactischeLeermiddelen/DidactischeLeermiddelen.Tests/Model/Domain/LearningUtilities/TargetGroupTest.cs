using System;
using System.ComponentModel.DataAnnotations;
using DidactischeLeermiddelen.Models.Domain.LearningUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model.Domain.LearningUtilities
{
    [TestClass]
    public class TargetGroupTest
    {
        private TargetGroup targetGroup;

        [TestMethod]
        public void TargetGroupDefaultConstructorCreatesATargetGroup()
        {
            #region Arrange
            const string targetGroupName = "GLEDE 1.011";
            #endregion

            #region Act
            targetGroup = new TargetGroup();
            #endregion

            #region Assert
            Assert.IsNotNull(targetGroup);
            Assert.IsInstanceOfType(targetGroup, typeof(TargetGroup)); 
            #endregion


        }
        [TestMethod]
        public void TargetGroupParameterConstructorCreatesATargetGroup()
        {
            #region Arrange
            const string targetGroupName = "Preparatie";
            #endregion

            #region Act
            targetGroup = new TargetGroup(targetGroupName);
            #endregion

            #region Assert
            Assert.AreEqual(targetGroupName, targetGroup.Name);
            #endregion
        }
        [TestMethod]
        public void targetGroupNameIs50CharactersLongSetsTheName()
        {
            #region Arrange
            const string targetGroupName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int targetGroupNameLength = targetGroupName.Length;
            #endregion

            #region Act
            targetGroup = new TargetGroup(targetGroupName);
            #endregion

            #region Assert
            Assert.AreEqual(targetGroupName, targetGroup.Name);
            Assert.AreEqual(50, targetGroupNameLength);
            #endregion
        }
        [TestMethod]
        public void targetGroupNameIs100CharactersLongSetsTheName()
        {
            #region Arrange
            const string targetGroupName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            int targetGroupNameLength = targetGroupName.Length;
            #endregion

            #region Act
            targetGroup = new TargetGroup(targetGroupName);
            #endregion

            #region Assert
            Assert.AreEqual(targetGroupName, targetGroup.Name);
            Assert.AreEqual(100, targetGroupNameLength);
            #endregion
        }
        
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void targetGroupNameIs101CharactersLongThrowsError()
        {
            #region Arrange
            const string targetGroupName = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaab";
            int targetGroupNameLength = targetGroupName.Length;
            #endregion

            #region Act
            targetGroup = new TargetGroup(targetGroupName);
            #endregion

            #region Assert
            Assert.AreEqual(101, targetGroupNameLength);
            #endregion
        }
        [TestMethod]
        public void targetGroupNameHasAlphaNumericNameCreatesIt()
        {
            #region Arrange
            const string targetGroupName = "GLEDE 1.011";
            #endregion

            #region Act
            targetGroup = new TargetGroup(targetGroupName);
            #endregion

            #region Assert
            Assert.AreEqual(targetGroupName, targetGroup.Name);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void targetGroupNameHasIsNullThrowsError()
        {
            #region Arrange
            string targetGroupName = null;
            #endregion

            #region Act
            targetGroup = new TargetGroup(targetGroupName);
            #endregion
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void targetGroupNameHasIsEmptyThrowsError()
        {
            #region Arrange
            string targetGroupName = string.Empty;
            #endregion

            #region Act
            targetGroup = new TargetGroup(targetGroupName);
            #endregion
        }
    }
}
