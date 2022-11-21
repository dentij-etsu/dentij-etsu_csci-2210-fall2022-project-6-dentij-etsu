///////////////////////////////////////////////////////////////////////////////
//
// Author: Jackson Denti, dentij@etsu.edu
// Course: CSCI-2210-001 - Data Structures
// Assignment: Project 6
// Description: Class to manage objects depicting books
//
///////////////////////////////////////////////////////////////////////////////

namespace LibraryApp
{
    public class Book : IComparable<Book>
    {   
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string Publisher { get; set; }
        public int sortKey;
        
        /// <summary>
        /// Base Constructor
        /// </summary>
        public Book()
        {
        }

        /// <summary>
        /// Overloaded Constructor with inputs for all fields
        /// </summary>
        /// <param name="title"> Title of Book </param>
        /// <param name="author"> Name of Book Writer </param>
        /// <param name="pages"> Number of pages in book </param>
        /// <param name="publisher"> Name of publisher, if any </param>
        public Book(string title, string author, int pages, string publisher)
        {
            this.Title = title;
            this.Author = author;
            this.Pages = pages;
            this.Publisher = publisher;
            this.sortKey = 1;
        }

        /// <summary>
        /// Overloaded Constructor with inputs for all fields and sorting key
        /// </summary>
        /// <param name="title"> Title of Book </param>
        /// <param name="author"> Name of Book Writer </param>
        /// <param name="pages"> Number of pages in book </param>
        /// <param name="publisher"> Name of publisher, if any </param>
        /// <param name="sortKey"> Sorting key for this particular book, used for CompareTo overloaded method </param>
        public Book(string title, string author, int pages, string publisher, int sortKey)
        {
            this.Title = title;
            this.Author = author;
            this.Pages = pages;
            this.Publisher = publisher;
            this.sortKey = sortKey;
        }

        /// <summary>
        /// Method to printed all book details to console
        /// </summary>
        public void Print()
        {
            if (this.Publisher.Length > 1)
            {
                Console.WriteLine($"{Title} ~ {Author} ~  {Pages} Pages ~ {Publisher}");
            }
            else
            {
                Console.WriteLine($"{Title} ~ {Author} ~ {Pages} Pages");
            }

        }

        /// <summary>
        /// Implementation of CompareTo method for IComparable Interface
        /// </summary>
        /// <param name="other"> Second Object used for comparison </param>
        /// <returns> Returns -1, 0, 1 denoting compared likeness </returns>
        public int CompareTo(Book? other)
        {
            if (sortKey == 1) return this.Title.CompareTo(other.Title);
            if (sortKey == 2) return this.Author.CompareTo(other.Author);
            if (sortKey == 3) return this.Publisher.CompareTo(other.Publisher);
            return 0;
        }
    }
}
