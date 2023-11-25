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

            return bookList;
        }

        public BookModel GetBookModel(Guid ID)
        {
            return MapObjectToModel(dbContext.Books.FirstOrDefault(x => x.BookId == ID));
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

            if(book != null )
            {
                bookModel.BookID = book.BookId;
                bookModel.BookName = book.BookName;
                bookModel.BookGenreID = book.BookGenreID;
                bookModel.BookPublisherID = book.BookPublisherID;
                bookModel.BookAuthor = book.BookAuthor;
                bookModel.BookPublishYear = book.BookPublishYear;
                bookModel.BookCoverType = book.BookCoverType;
                bookModel.BookNumberOfUnits = book.BookNumberOfUnits;
                bookModel.BookNumberOfUnitsAvailable = bookModel.BookNumberOfUnitsAvailable;
            }

            return bookModel;
        }

    }
}
