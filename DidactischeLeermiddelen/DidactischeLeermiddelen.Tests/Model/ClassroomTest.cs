using System;
using DidactischeLeermiddelen.Models.Domain.Locations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DidactischeLeermiddelen.Tests.Model
{
    [TestClass]
    public class ClassroomTest
    {
        private Classroom classroom;
        [TestMethod]
        public void ConstructorWithParamsCreatesClassroom()
        {
            classroom = new Classroom("be54");
            Assert.AreEqual("be54", classroom.Name);
        }
        public void DefaultConstructorCreatesCity()
        {
            classroom = new Classroom();
            Assert.IsNotNull(classroom);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ClassroomNameIsEmptyThrowsError()
        {
            classroom = new Classroom();
            classroom.Name = String.Empty;

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ClassroomNameIsNullThrowsError()
        {
            classroom = new Classroom();
            classroom.Name = null;

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ClassroomNameIs101LongThrowsError()
        {
            classroom = new Classroom();
            classroom.Name =
                "abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij1";

        }
        [TestMethod]
        public void ClassroomNameIs100LongCreates()
        {
            classroom = new Classroom("abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij");
            Assert.AreEqual("abcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghijabcdefghij", classroom.Name);
        }
    }
}
