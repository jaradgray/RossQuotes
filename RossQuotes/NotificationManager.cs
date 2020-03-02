using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RossQuotes
{
    /// <summary>
    /// This class manages providing data for NotificationModel objects.
    /// </summary>
    public class NotificationManager
    {
        // Instance variables

        /// <summary>
        /// The Dictionary representation of the quotes.json file data
        /// </summary>
        private Dictionary<string, List<string>> mAllQuotesDictionary = new Dictionary<string, List<string>>();

        /// <summary>
        /// The Dictionary containing all quotes that haven't been used as a NotificationModel's Message
        /// </summary>
        private Dictionary<string, List<string>> mUnusedQuotesDictionary;

        private Random mRandy = new Random();


        // Constructor

        /// <summary>
        /// Constructor used by NotificationManagerTests class. DO NOT USE this constructor outside of NotificationManagerTests.
        /// </summary>
        public NotificationManager(Dictionary<string, List<string>> allQuotesDictionary)
        {
            // Initialize Dictionaries based on parameter
            mAllQuotesDictionary = CopyDictionary(allQuotesDictionary);
            mUnusedQuotesDictionary = CopyDictionary(mAllQuotesDictionary);
        }

        public NotificationManager()
        {
            // Build mAllQuotesDictionary from quotes.json file

            // Read file data
            string jsonFileData;
            using (StreamReader streamReader = new StreamReader("./quotes.json", Encoding.UTF8))
            {
                jsonFileData = streamReader.ReadToEnd();
            }

            // Convert file data to JSON object
            JObject jObject = JObject.Parse(jsonFileData);

            // For each of jObject's properties...
            foreach (JProperty property in jObject.Properties())
            {
                // Get the property's name and value
                string propertyName = property.Name;
                JArray propertyValue = (JArray)property.Value;

                // Add the property's name and value data to the Dictionary of all quotes
                mAllQuotesDictionary.Add(propertyName, propertyValue.ToObject<List<string>>());
            }

            // Initialize mUnusedQuotesDictionary as a copy of mAllQuotesDictionary
            mUnusedQuotesDictionary = CopyDictionary(mAllQuotesDictionary);
        }


        // Public methods

        public NotificationModel GetRandomNotificationModel()
        {
            NotificationModel notification = new NotificationModel();

            // Get a random Dictionary key to be used as the notification's Sender
            string key = GetRandomKey();
            notification.Sender = key;

            // Get a random quote from key's List in the Dictionary
            notification.Message = GetRandomQuote(key);

            // Set notification.Icon based on sender
            System.Drawing.Icon icon = GetIconBySender(key);
            notification.Icon = icon;

            return notification;
        }

        /// <summary>
        /// For unit testing
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<string>> GetUnusedQuotesDict() { return mUnusedQuotesDictionary; }

        /// <summary>
        /// For unit testing
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetARandomQuote(string key) { return GetRandomQuote(key); }


        // Private methods


        /// <summary>
        /// Returns a random key from the mAllQuotesDictionary
        /// </summary>
        /// <returns></returns>
        private string GetRandomKey()
        {
            int index = mRandy.Next(mAllQuotesDictionary.Count);
            return mAllQuotesDictionary.Keys.ElementAt(index);
        }

        /// <summary>
        /// Returns a random quote from the List of quotes from mUnusedQuotesDictionary[key] and
        /// removes the quote. Re-initializes the list of quotes if we remove the only element.
        /// </summary>
        /// <param name="key">the key of the key, value pair in the Dictionary of unused quotes</param>
        /// <returns></returns>
        private string GetRandomQuote(string key)
        {
            // Get a random quote from the List of quotes from mUnusedQuotesDictionary[key]
            List<string> unusedQuotes = mUnusedQuotesDictionary[key];
            int index = mRandy.Next(unusedQuotes.Count);
            string result = unusedQuotes[index];

            // Remove the selected quote from the list, and re-init the List in mUnusedQuotesDictionary if it's empty
            unusedQuotes.RemoveAt(index);
            if (unusedQuotes.Count == 0)
            {
                mUnusedQuotesDictionary[key] = new List<string>(mAllQuotesDictionary[key]);
            }

            return result;
        }

        /// <summary>
        /// Returns the notification Icon associated with @sender.
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        private System.Drawing.Icon GetIconBySender(string sender)
        {
            switch (sender)
            {
                case "Bob Ross":
                    return Properties.Resources.icon_ross_128;
                case "Rick Steves":
                    return Properties.Resources.icon_steves_128;
                default:
                    return null;
            }
        }


        // Helpers

        /// <summary>
        /// Returns a copy of @toCopy whose values don't reference toCopy's values.
        /// This is necessary because Dictionary's values aren't primitives, so they're not copied via Dictionary constructor.
        /// </summary>
        /// <param name="toCopy"></param>
        /// <returns></returns>
        private Dictionary<string, List<string>> CopyDictionary(Dictionary<string, List<string>> toCopy)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

            // For each key in toCopy...
            foreach (string key in toCopy.Keys)
            {
                // Get a copy of key's values, and add the key,value pair to result
                List<string> copyOfValues = new List<string>(toCopy[key]);
                result.Add(key, copyOfValues);
            }

            return result;
        }

        
    }
}
