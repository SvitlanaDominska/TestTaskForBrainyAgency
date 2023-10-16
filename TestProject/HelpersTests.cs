using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProjectSD_withDatabase.Helpers;

namespace TestProject
{
    internal class HelpersTests
    {

        [Test]
        [TestCase ("sdomins@example.com")]
        [TestCase("sdomins@example.net")]
        [TestCase("sdomins@example.org")]
        public void VerifyCorrectEmailTest(string email) 
        {
            Assert.IsTrue(EmailAddress.IsEmailAddress(email));
        }

        [Test]
        [TestCase("sdomins@dddd@example.com")]
        [TestCase("HelloThisIsEmail")]
        [TestCase("sdomins@example")]
        public void VerifyInCorrectEmailTest(string email)
        {
            Assert.IsFalse(EmailAddress.IsEmailAddress(email));
        }

        [Test]
        [TestCase("0123456789")]
        [TestCase("012-345-6789")]
        [TestCase("(012)-345-6789")]
        public void VerifyCorrectPhoneNumberTest(string phoneNumber)
        {
            Assert.IsTrue(PhoneNumber.IsPhoneNbr(phoneNumber));
        }

        [Test]
        [TestCase("$^54hnj(*")]
        [TestCase("HelloThisIsPhoneNumber")]
        [TestCase("33454fff444")]
        public void VerifyInCorrectPhoneNumberTest(string phoneNumber)
        {
            Assert.IsFalse(PhoneNumber.IsPhoneNbr(phoneNumber));
        }
    }
}
