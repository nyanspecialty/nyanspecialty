using System;
using System.Collections.Generic;

namespace NyanSpecialty.Assistance.Web.Data.Utility
{
    public class RandomNameGeneratorUtility
    {
        private static readonly string[] firstNames =
        {
            "John", "Jane", "Michael", "Emily", "Chris", "Jessica", "David", "Sarah", "Daniel", "Laura",
            "Matthew", "Ashley", "James", "Amanda", "Joshua", "Megan", "Joseph", "Kimberly", "Ryan", "Michelle",
            "William", "Linda", "Brian", "Elizabeth", "Jason", "Barbara", "Alexander", "Patricia", "Andrew", "Nancy",
            "Christopher", "Betty", "Jacob", "Sandra", "Nicholas", "Dorothy", "Matthew", "Helen", "Tyler", "Deborah"
        };

        private static readonly string[] lastNames =
        {
            "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
            "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson",
            "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King"
        };

        private static List<string> allNames = new List<string>();
        private static Random random = new Random();

        // Static constructor to initialize names once
        static RandomNameGeneratorUtility()
        {
            InitializeNames();
        }

        // Method to initialize and shuffle the names
        private static void InitializeNames()
        {
            allNames.Clear(); // Clear any existing names
            foreach (var firstName in firstNames)
            {
                foreach (var lastName in lastNames)
                {
                    allNames.Add($"{firstName} {lastName}");
                }
            }

            // Shuffle the list
            Shuffle(allNames);
        }

        // Method to shuffle a list
        private static void Shuffle(List<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n--);
                var temp = list[n];
                list[n] = list[k];
                list[k] = temp;
            }
        }

        // Method to generate a random name (with possible duplicates)
        public static string GenerateRandomName()
        {
            if (allNames.Count == 0)
            {
                InitializeNames(); // Reinitialize if all names have been used
            }

            // Randomly select a name from the list
            int index = random.Next(allNames.Count);
            return allNames[index];
        }
    }
}