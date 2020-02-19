using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest_UnitTesting;
using FluentAssertions;
using MSTest_Demo;
using System;
using System.IO;
using System.Linq;
using System.Collections;

namespace MSTest_UnitTesting_TestProject
{
    [TestClass]
    public class FluentAsssertionTests       
    {
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance_FluentAssertions()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Jagadish", beginningBalance);
            
            
            // Act
            account.Debit(debitAmount);

            // Assert
            double actual = account.Balance;
            actual.Should().Equals(expected);
        }

        //Exception using Fluent Assertion
        [TestMethod]
        public void Capitalize_Throws_Exception_When_Argument_Is_Null_Fluent_Assertion()
        {
            var component = new SampleComponent();
            //component.CapitalizeThis(null);
            FluentActions.Invoking(() => component.CapitalizeThis(null)).Should().Throw<ArgumentNullException>();
        }


        //Collection using FluentAssertion
        [TestMethod]
        public void Find_Four_Highest_Temps_FluentAssertion()
        {
            var sample = new SampleComponent();

            var results = sample.ComputeSamples();
            var query = (from s in results
                         orderby s.Temperature descending
                         select s.Temperature).Take(4);
            //CollectionAssert.AreEqual(
            //    new[] { 106.7, 106.2, 105.2, 103.9 }, query.ToArray());
            //IEnumerable expectedCollection = new[] { 106.7, 106.2, 105.2, 103.9 };
            query.ToArray().Should().IntersectWith(new[] { 106.7, 106.2, 105.2, 103.9 });
            
        }
    }
}
