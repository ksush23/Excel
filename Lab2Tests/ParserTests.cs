using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.Tests
{
    [TestClass()]
    public class ParserTests
    {
        [TestMethod()]
        public void ExpressionStartTest()
        {
            string expression = "5 + 5 * 4";
            string expected = "25";

            Parser parser = new Parser();
            string actual = parser.ExpressionStart(expression).ToString();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ExpressionStartTest1()
        {
            string expression = "5 | 5 - 4 * +1";
            string expected = "-7";

            Parser parser = new Parser();
            string actual = parser.ExpressionStart(expression).ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}