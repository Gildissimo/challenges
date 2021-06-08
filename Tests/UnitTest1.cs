using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ScolloLib;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Step1()
        {
            Assert.AreEqual(CalcLib.Add(""),0);
            Assert.AreEqual(CalcLib.Add("1"), 1);
            Assert.AreEqual(CalcLib.Add("1,2"), 3);
        }

        [TestMethod]
        public void Step2()
        {
            Assert.AreEqual(CalcLib.Add("1,1,1,1,1,1,1,1,1,1"), 10);
        }

        [TestMethod]
        public void Step3()
        {
            Assert.ThrowsException<NegativeException>(() => CalcLib.Add("1,-1"));
        }

        [TestMethod]
        public void Step4()
        {
            Assert.AreEqual(CalcLib.Add("1000,2"), 2);
            Assert.AreEqual(CalcLib.Add("1,2,1000,2"), 5);
        }

        /// <summary>
        /// //[delimiter]//
        /// </summary>
        [TestMethod]
        public void Step5()
        {
            Assert.AreEqual(CalcLib.Add("//[***]//1***2***3"), 6);
        }

        /// <summary>
        /// //[delim1][delim2]//
        /// </summary>
        [TestMethod]
        public void Step6()
        {
            Assert.AreEqual(CalcLib.Add("//[*][%]//1*2%3"), 6);
        }

        /// <summary>
        /// //[delim1][delim2]//
        /// </summary>
        [TestMethod]
        public void Step7()
        {
            Assert.AreEqual(CalcLib.Add("//[**][%%]//1**2%%3"), 6);
            Assert.AreEqual(CalcLib.Add("//[***][%%]//1***2%%3"), 6);
        }
    }
}
