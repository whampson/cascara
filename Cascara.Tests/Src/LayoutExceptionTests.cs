﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml.Linq;

namespace WHampson.Cascara.Tests
{
    [TestClass]
    public class LayoutExceptionTests
    {
        [TestMethod]
        public void Create_AllNull()
        {
            // Expected results
            string expectedMessage = "";
            int expectedLineNum = 0;
            int expectedLinePos = 0;
            Exception expectedInnerException = null;

            // Inputs
            BinaryLayout layout = null;
            XObject obj = null;
            string msg = null;
            Exception innerException = null;

            // Execution
            LayoutException ex = LayoutException.Create(layout, innerException, obj, msg);

            // Assertion
            Assert.AreEqual(expectedMessage, ex.Message);
            Assert.AreEqual(expectedLineNum, ex.LineNumber);
            Assert.AreEqual(expectedLinePos, ex.LinePosition);
            Assert.AreEqual(expectedInnerException, ex.InnerException);
        }

        [TestMethod]
        public void Create_Message()
        {
            // Expected results
            string expectedMessage = "Test.";
            int expectedLineNum = 0;
            int expectedLinePos = 0;
            Exception expectedInnerException = null;

            // Inputs
            BinaryLayout layout = null;
            XObject obj = null;
            string msg = "Test.";
            Exception innerException = null;

            // Execution
            LayoutException ex = LayoutException.Create(layout, innerException, obj, msg);

            // Assertion
            Assert.AreEqual(expectedMessage, ex.Message);
            Assert.AreEqual(expectedLineNum, ex.LineNumber);
            Assert.AreEqual(expectedLinePos, ex.LinePosition);
            Assert.AreEqual(expectedInnerException, ex.InnerException);
        }

        [TestMethod]
        public void Create_InnerExceptionWithMessage()
        {
            // Expected results
            string expectedMessage = @"Failed to process layout.
Caused by:
  ArgumentNullException: The parameter was null.
    Parameter name: foo";
            int expectedLineNum = 0;
            int expectedLinePos = 0;
            Exception expectedInnerException = new ArgumentNullException("foo", "The parameter was null.");

            // Inputs
            BinaryLayout layout = null;
            XObject obj = null;
            string msg = "Failed to process layout.";
            Exception innerException = expectedInnerException;

            // Execution
            LayoutException ex = LayoutException.Create(layout, innerException, obj, msg);

            // Assertion
            Assert.AreEqual(expectedMessage, ex.Message);
            Assert.AreEqual(expectedLineNum, ex.LineNumber);
            Assert.AreEqual(expectedLinePos, ex.LinePosition);
            Assert.AreEqual(expectedInnerException, ex.InnerException);
        }
    }
}