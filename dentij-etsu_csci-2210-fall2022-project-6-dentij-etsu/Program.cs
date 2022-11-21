///////////////////////////////////////////////////////////////////////////////
//
// Author: Jackson Denti, dentij@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: Driving class with main method for project
//              Reads data in from CSV and creates AVL Trees from data
//              Implements user driven menu using console and keyboard
//
///////////////////////////////////////////////////////////////////////////////

using LibraryApp;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.DataStructures;
using System.Linq;

namespace dentij_etsu_csci_2210_fall2022_project_6_dentij_etsu
{
    internal class Program
    {
        /// <summary>
        /// Main Method for program
        /// </summary>
        static void Main()
        {

            AvlTree<Book> titleSortedTree = new AvlTree<Book>();
            AvlTree<Book> authorSortedTree = new AvlTree<Book>();
            AvlTree<Book> publisherSortedTree = new AvlTree<Book>();
            AvlTree<Book> checkedOutTree = new AvlTree<Book>();
            StreamReader sr = new StreamReader(@".\books.csv");

            List<string> CSVFields = new List<string>();
            
            

            while (sr.Peek() > -1)
            {
                CSVFields = ProcessCSVLine(sr.ReadLine());
                titleSortedTree.Add(new Book(CSVFields[0], CSVFields[1], Int32.Parse(CSVFields[2]), CSVFields[3]));
                authorSortedTree.Add(new Book(CSVFields[0], CSVFields[1], Int32.Parse(CSVFields[2]), CSVFields[3], 2));
                publisherSortedTree.Add(new Book(CSVFields[0], CSVFields[1], Int32.Parse(CSVFields[2]), CSVFields[3], 2));
            }

            AvlTree<Book> currentTree = titleSortedTree;          

            int option = 1;
            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to the \"Insert Library Name here\" Library\nPlease Type an option corresponding to what you'd like to do.\n\n" +
                    "1: Diplay All Books\n2: Display Checked Out Books\n3: Change Sorting Key\n4: Check out book\n5: Check in book\n6: Quit");
                try   { option = Int32.Parse(Console.ReadLine()); }
                catch { option = -1; }

                // Code for user wanting to display all books
                if(option == 1)
                {
                    Console.Clear();
                    List<Book> books = currentTree.GetInorderEnumerator().ToList();
                    int count = 0;

                    foreach (Book book in books)
                    {
                        book.Print();
                    }

                    Console.WriteLine("\nPress Enter to continue");
                    Console.ReadLine();
                }

                //
                if (option == 2)
                {
                    Console.Clear();
                    List<Book> books = checkedOutTree.GetInorderEnumerator().ToList();
                    int count = 0;

                    foreach (Book book in books)
                    {
                        book.Print();
                    }

                    Console.WriteLine("\nPress Enter to continue");
                    Console.ReadLine();
                }

                //
                if(option == 3)
                {
                    int sortingOption = 0;
                    while (!(sortingOption == 1 || sortingOption == 2 || sortingOption == 3))
                    {
                        Console.WriteLine("Please type a digit corresponding with the field you would like to sort by:\n1 - Title, 2 - Author, 3 - Publisher");
                        sortingOption = Int32.Parse(Console.ReadLine());
                        if (sortingOption == 1) currentTree = titleSortedTree;
                        if (sortingOption == 2) currentTree = authorSortedTree;
                        if (sortingOption == 3) currentTree = publisherSortedTree;
                    }                   
                }

                //
                if(option == 4)
                {
                    Console.Clear();
                    Console.WriteLine("Please type in the title of the book to check out:\n");
                    CheckOutBook(Console.ReadLine());
                }

                //
                if(option == 5)
                {
                    Console.Clear();
                    Console.WriteLine("Please type in the title of the book to check in:\n");
                    CheckInBook(Console.ReadLine());
                }
                Console.WriteLine("\n\n");

                //
                if(option == 6)
                {
                    option = 0;
                }

                //
                if(option == -1)
                {
                    Console.WriteLine("ERROR: typed value could not be understood. Please type an appropriate digit.");
                    Console.WriteLine("\nPress Enter to continue");
                    Console.ReadLine();
                }
            }
            while (option != 0);

            Console.Clear();
            Console.WriteLine("Thank you for using this Library Application!");
            Thread.Sleep(4000);

            /// <summary>
            /// Private method to Remove books from tree by title
            /// </summary>
            /// <param name="titleToSearchFor"> Title of book to searched for (case insensitive) </param>
            void CheckOutBook(string titleToSearchFor)
            {
                List<Book> books = currentTree.GetInorderEnumerator().ToList();
                foreach (Book book in books)
                {
                    if (book.Title.ToLower() == titleToSearchFor.ToLower())
                    {
                        titleSortedTree.Remove(book);
                        authorSortedTree.Remove(book);
                        publisherSortedTree.Remove(book);
                        checkedOutTree.Add(book);
                    }
                }
            }

            /// <summary>
            /// Private method to add books from tree by title
            /// </summary>
            /// <param name="titleToSearchFor"> Title of book to searched for (case insensitive) </param>
            void CheckInBook(string titleToSearchFor)
            {
                List<Book> books = checkedOutTree.GetInorderEnumerator().ToList();
                foreach (Book book in books)
                {
                    if (book.Title.ToLower() == titleToSearchFor.ToLower())
                    {
                        titleSortedTree.Add(book);
                        authorSortedTree.Add(book);
                        publisherSortedTree.Add(book);
                        checkedOutTree.Remove(book);
                    }
                }
            }
        }

        /// <summary>
        /// Takes a comma delimited field with commas in the data and appropriately sanitizes it and splits it, then returns as list
        /// Borrowed from Jake Gillenwater
        /// </summary>
        /// <param name="lineFromCSV"> Line from CSV containing commas in data fields </param>
        /// <returns> List of strings, santized of commas </returns>
        public static List<string> ProcessCSVLine(string lineFromCSV)
        {
            // Split it based on a comma
            string[] rawBookParts = lineFromCSV.Split(",");
            // Create a list of book parts that represent the columns in the CSV
            // We can treat anything that goes into this list as sanitized data.
            List<string> sanitizedBookParts = new List<string>();
            // Iterate through each book part found by naively spliting on the comma alone.
            string cleanString = string.Empty;
            for (int i = 0; i < rawBookParts.Length; i++)
            {
                // Add the newest item split by the comma alone to a new string
                cleanString += rawBookParts[i];
                // If the string starts with a quote, but does not end with a quote,
                // then we know that the full text from the string hasn't been
                // read in yet, and that the rest of the text be in a future
                // element of rawBookParts.
                if (cleanString.StartsWith("\"") && !cleanString.EndsWith("\""))
                {
                    continue;
                }
                // Once it is verified that the clean string contains the entire
                // text of the column, we can add it to our list of sanitized
                // book parts. This is also a good time to remove the quotes
                // at the beginning and end of the string if they exist.
                sanitizedBookParts.Add(cleanString.Replace("\"", String.Empty));
                // Reset the clean string for the next iteration.
                cleanString = String.Empty;
            }
            return sanitizedBookParts;
        }
    }
}