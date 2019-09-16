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
    public class FormExcelTests
    {
        [TestMethod()]
        public void IsExpressionTest()
        {
            string expressionToCheck = "A25";

            bool expected = true;

            FormExcel formExcel = new FormExcel();
            bool actual = formExcel.IsExpression(expressionToCheck);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsExpressionTest1()
        {
            string expressionToCheck = "Af343";

            bool expected = false;

            FormExcel formExcel = new FormExcel();
            bool actual = formExcel.IsExpression(expressionToCheck);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToFormulaTest()
        {
            string expressionToFormula = "A0 + 5";

            string expected = "0 + 5";

            FormExcel formExcel = new FormExcel();
            string actual = formExcel.ToFormula(expressionToFormula, "0");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ToFormulaTest1()
        {
            string expressionToFormula = "A0 + A0 + A0*6 + 10^10";

            string expected = "0 + 0 + 0*6 + 10^10";

            FormExcel formExcel = new FormExcel();
            string actual = formExcel.ToFormula(expressionToFormula, "0");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsCorrectExpressionTest()
        {
            string expression = "5 + A0";

            bool expected = true;

            FormExcel formExcel = new FormExcel();
            bool actual = formExcel.IsCorrectExpression(expression);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsCorrectExpressionTest1()
        {
            string expression = "5hg6jf + + +";

            bool expected = false;

            FormExcel formExcel = new FormExcel();
            bool actual = formExcel.IsCorrectExpression(expression);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsCorrectExpressionTest2()
        {
            string expression = "A0 + AB3 + 44";

            bool expected = false;

            FormExcel formExcel = new FormExcel();
            bool actual = formExcel.IsCorrectExpression(expression);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsCorrectExpressionTest3()
        {
            string expression = "5 + A0*3 + A1000 + 44^2 + 5";

            bool expected = true;

            FormExcel formExcel = new FormExcel();
            bool actual = formExcel.IsCorrectExpression(expression);

            Assert.AreEqual(expected, actual);
        }
    }
}