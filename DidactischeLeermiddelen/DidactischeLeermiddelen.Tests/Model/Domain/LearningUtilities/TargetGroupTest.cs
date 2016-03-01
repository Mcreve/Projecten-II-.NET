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
        public void TargetGroupNameIs50CharactersLongSetsTheName()
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
        public void TargetGroupNameIs100CharactersLongSetsTheName()
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
        public void TargetGroupNameIs101CharactersLongThrowsError()
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
        public void TargetGroupNameHasAlphaNumericNameCreatesIt()
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
        public void TargetGroupNameHasIsNullThrowsError()
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
        public void TargetGroupNameHasIsEmptyThrowsError()
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
