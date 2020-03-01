/**
 * Following the unit test guide from here:
 * https://docs.microsoft.com/en-us/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2019
 */

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RossQuotes;

namespace NotificationManagerTests
{
    [TestClass]
    public class NotificationManagerTests
    {
        /// <summary>
        /// Makes sure NotificationManager's Dictionaries and their contents don't reference the same objects.
        /// </summary>
        [TestMethod]
        public void NotificationManager_DoesntCopyByReference()
        {
            // Arrange
            // dict will serves as manager's Dictionary of all quotes Lists
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            string key = "key";
            List<string> value = new List<string>();
            value.Add("value1");
            value.Add("value2");
            value.Add("value3");
            dict.Add(key, value);

            NotificationManager manager = new NotificationManager(dict);

            // Act
            manager.GetARandomQuote(key);
            manager.GetARandomQuote(key);

            // Assert
            int expected = 3;
            int actual = value.Count;
            Assert.AreEqual(expected, actual, "NotificationManager touched value's things");

            expected = 1;
            actual = manager.GetUnusedQuotesDict()[key].Count;
            Assert.AreEqual(expected, actual, "NotificationManager not updated correctly");
        }

        /// <summary>
        /// Test whether GetRandomQuote() removes quotes from NotificationManager's Dictionary's Lists as expected.
        /// </summary>
        [TestMethod]
        public void GetRandomQuote_UpdatesUnusedQuotesDictionary()
        {
            // Arrange
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            string key = "key";
            List<string> value = new List<string>();
            value.Add("value1");
            value.Add("value2");
            value.Add("value3");
            dict.Add(key, value);

            NotificationManager manager = new NotificationManager(dict);

            int expectedCount = value.Count;
            int actualCount = manager.GetUnusedQuotesDict()[key].Count;
            Assert.AreEqual(expectedCount, actualCount, "NotificationManager not initialized correctly");

            // Act
            manager.GetARandomQuote(key);

            // Assert
            expectedCount = 2;
            actualCount = manager.GetUnusedQuotesDict()[key].Count;
            Assert.AreEqual(expectedCount, actualCount, "NotificationManager not updated correctly");
        }

        /// <summary>
        /// Tests whether GetRandomQuote() removes the quote it returns from NotificationManager's unused quotes Dictionary.
        /// </summary>
        [TestMethod]
        public void GetRandomQuote_UpdatesUnusedQuotesDictionaryCorrectly()
        {
            // Arrange
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            string key = "key";
            List<string> value = new List<string>();
            value.Add("value1");
            value.Add("value2");
            value.Add("value3");
            dict.Add(key, value);

            NotificationManager manager = new NotificationManager(dict);

            string quote;
            string valuesString;

            // Act
            for (int i = 0; i < value.Count * 2; i++)
            {
                quote = manager.GetARandomQuote(key);
                List<string> values = manager.GetUnusedQuotesDict()[key];
                valuesString = "manager's List of unused quotes: { ";
                foreach (string s in values) { valuesString += s + ", "; }
                valuesString += "}";
                Console.WriteLine("quote: " + quote);
                Console.WriteLine(valuesString);
            }

            // Nothing to assert; view test's output to verify correctness
        }

        /// <summary>
        /// Tests whether GetRandomQuote() re-initializes NotificationManager's unused quotes Dictionary's List
        /// when it returns the last element in a quotes List.
        /// </summary>
        [TestMethod]
        public void GetRandomQuote_ReInitsEmptyList()
        {
            // Arrange
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            string key = "key";
            List<string> value = new List<string>();
            value.Add("value1");
            value.Add("value2");
            value.Add("value3");
            dict.Add(key, value);

            NotificationManager manager = new NotificationManager(dict);

            // Act
            manager.GetARandomQuote(key);
            manager.GetARandomQuote(key);
            manager.GetARandomQuote(key);

            // Assert
            int expected = 3;
            int actual= manager.GetUnusedQuotesDict()[key].Count;
            Assert.AreEqual(expected, actual, "NotificationManager not updated correctly");
        }

        


    }
}
