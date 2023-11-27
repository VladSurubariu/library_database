using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;
using project.Models.DBObjects;

namespace project.Repository
{
    public class BookRepository
    {
        private ApplicationDbContext dbContext;

        public BookRepository() 
        {
            dbContext = new ApplicationDbContext();
        }

        public BookRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<BookModel> GetAllBooks()
        {
            List<BookModel> bookList = new List<BookModel>();

            foreach (Book dbBook in dbContext.Books)
            {
                bookList.Add(MapObjectToModel(dbBook));
            }

            foreach (BookModel model in bookList)
            {
                Genre genre = new Genre();
                Publisher publisher= new Publisher();

                if(genre != null)
                {
                    genre = dbContext.Genres.FirstOrDefault(x => x.GenreId == model.BookGenreID);
                }

                model.bookGenreName = genre.GenreName;
            }

            foreach (BookModel model in bookList)
            {
                Publisher publisher = new Publisher();
                publisher = dbContext.Publishers.FirstOrDefault(x => x.PublisherId == model.BookPublisherID);

                if(publisher!= null)
                {
                    model.bookPublisherName = publisher.PublisherName;
                }

            }

            return bookList;
        }

        public BookModel GetBookModel(Guid ID)
        {
            Book book = dbContext.Books.FirstOrDefault(x => x.BookId == ID);

            return MapObjectToModel(book);
        }

        public void InsertBook(BookModel bookModel) 
        {
            bookModel.BookID = Guid.NewGuid();

            dbContext.Books.Add(MapModelToObject(bookModel));
            dbContext.SaveChanges();
        }

        public void UpdateBook(BookModel bookModel)
        {
            Book existingBook = dbContext.Books.FirstOrDefault(x => x.BookId == bookModel.BookID);

            if(existingBook != null)
            {
                existingBook.BookId = bookModel.BookID;
                existingBook.BookName = bookModel.BookName;
                existingBook.BookAuthor = bookModel.BookAuthor;
                existingBook.BookGenreID = bookModel.BookGenreID;
                existingBook.BookPublishYear= bookModel.BookPublishYear;
                existingBook.BookPublisherID= bookModel.BookPublisherID;
                existingBook.BookCoverType = bookModel.BookCoverType;
                existingBook.BookNumberOfUnits= bookModel.BookNumberOfUnits;
                existingBook.BookNumberOfUnitsAvailable= bookModel.BookNumberOfUnitsAvailable;
                dbContext.SaveChanges();
            }
        }

        public void DeleteBook(Guid id)
        {
            Book existingBook = dbContext.Books.FirstOrDefault(x => x.BookId == id);

            if(existingBook != null)
            {
                dbContext.Books.Remove(existingBook);
                dbContext.SaveChanges();
            }
        }

        private Book MapModelToObject(BookModel bookModel)
        {
            Book book = new Book(); 

            if(bookModel != null)
            {
                book.BookId = bookModel.BookID;
                book.BookName = bookModel.BookName;
                book.BookGenreID = bookModel.BookGenreID;
                book.BookPublisherID = bookModel.BookPublisherID;
                book.BookAuthor = bookModel.BookAuthor;
                book.BookPublishYear= bookModel.BookPublishYear;
                book.BookCoverType = bookModel.BookCoverType;
                book.BookNumberOfUnits= bookModel.BookNumberOfUnits;
                book.BookNumberOfUnitsAvailable= bookModel.BookNumberOfUnitsAvailable;
            }
            
            return book;
        }
        
        private BookModel MapObjectToModel(Book book)
        {
            BookModel bookModel = new BookModel();

            if (book != null )
            {
                bookModel.BookID = book.BookId;
                bookModel.BookName = book.BookName;
                bookModel.BookGenreID = book.BookGenreID;
                bookModel.BookPublisherID = book.BookPublisherID;
                bookModel.BookAuthor = book.BookAuthor;
                bookModel.BookPublishYear = book.BookPublishYear;
                bookModel.BookCoverType = book.BookCoverType;
                bookModel.BookNumberOfUnits = book.BookNumberOfUnits;
                bookModel.BookNumberOfUnitsAvailable = book.BookNumberOfUnitsAvailable;
                
            }

            return bookModel;
        }

        public BookModel UpdateGenreName(BookModel model)
        {
            Genre genre = new Genre();
            genre = dbContext.Genres.FirstOrDefault(x => x.GenreId == model.BookGenreID);

            if (genre != null)
            {
                model.bookGenreName = genre.GenreName;
            }

            return model;
        }

        public BookModel UpdatePublisherName(BookModel model)
        {
            Publisher publisher = new Publisher();
            publisher = dbContext.Publishers.FirstOrDefault(x => x.PublisherId == model.BookPublisherID);

            if (publisher != null)
            {
                model.bookPublisherName = publisher.PublisherName;
            }

            return model;
        }

        public List<BookModel> getBookFromName(string searchTerm)
        {
            List<BookModel> bookListModel = new List<BookModel>();

            var foundBooks = dbContext.Books
                .Where(book => book.BookName.Contains(searchTerm))
                .ToList();

            foreach (Book book in foundBooks)
            {
                bookListModel.Add(MapObjectToModel(book));
            }

            return bookListModel;
        }

    }
}
