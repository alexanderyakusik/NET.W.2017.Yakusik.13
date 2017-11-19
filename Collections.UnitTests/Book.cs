namespace Collections.UnitTests
{
    using System;

    public class Book : IComparable<Book>, IEquatable<Book>
    {
        public Book(string author, string title)
        {
            Author = author;
            Title = title;
        }

        public string Author { get; }

        public string Title { get; }

        public int CompareTo(Book other)
        {
            var result = Author.CompareTo(other.Author);

            if (result != 0)
            {
                return result;
            }

            return Title.CompareTo(other.Title);
        }

        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Author, other.Author) && string.Equals(Title, other.Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((Book)obj);
        }
    }
}
