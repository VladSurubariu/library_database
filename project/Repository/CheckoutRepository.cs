using project.Data;
using project.Models.DBObjects;
using project.Models;

namespace project.Repository
{
    public class CheckoutRepository
    {
        private ApplicationDbContext dbContext;

        public CheckoutRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        public CheckoutRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<CheckoutModel> GetAllCheckouts()
        {
            List<CheckoutModel> CheckoutList = new List<CheckoutModel>();

            foreach (Checkout dbCheckout in dbContext.Checkouts)
            {
                CheckoutList.Add(MapObjectToModel(dbCheckout));
            }

            foreach (CheckoutModel model in CheckoutList)
            {
                Book item = new Book();
                item = dbContext.Books.FirstOrDefault(x => x.BookId == model.CheckoutBookID);

                if(item != null)
                {
                    model.bookName = item.BookName;
                }
            }

            foreach (CheckoutModel model in CheckoutList)
            {
                Member item = new Member();
                item = dbContext.Members.FirstOrDefault(x => x.MemberId == model.CheckoutMemberID);

                if (item != null)
                {
                    model.memberName = item.MemberName;
                }
            }

            return CheckoutList;
        }

        public CheckoutModel GetCheckoutModel(Guid ID)
        {
            return MapObjectToModel(dbContext.Checkouts.FirstOrDefault(x => x.CheckoutId == ID));
        }

        public void InsertCheckout(CheckoutModel CheckoutModel)
        {
            CheckoutModel.CheckoutID = Guid.NewGuid();

            dbContext.Checkouts.Add(MapModelToObject(CheckoutModel));
            dbContext.SaveChanges();
        }

        public void UpdateCheckout(CheckoutModel CheckoutModel)
        {
            Checkout existingCheckout = dbContext.Checkouts.FirstOrDefault(x => x.CheckoutId == CheckoutModel.CheckoutID);

            if (existingCheckout != null)
            {
                existingCheckout.CheckoutId = CheckoutModel.CheckoutID;
                existingCheckout.CheckoutEmployeeName = CheckoutModel.CheckoutEmployeeName;
                existingCheckout.CheckoutMemberId = CheckoutModel.CheckoutMemberID;
                existingCheckout.CheckoutBookId = CheckoutModel.CheckoutBookID;
                existingCheckout.CheckoutCheckoutDate = CheckoutModel.CheckoutCheckoutDate;
                existingCheckout.CheckoutDueDate = CheckoutModel.CheckoutDueDate;
                existingCheckout.CheckoutReturnDate = CheckoutModel.CheckoutReturnDate;

                dbContext.SaveChanges();
            }
        }

        public void DeleteCheckout(Guid id)
        {
            Checkout existingCheckout = dbContext.Checkouts.FirstOrDefault(x => x.CheckoutId == id);

            if (existingCheckout != null)
            {
                dbContext.Checkouts.Remove(existingCheckout);
                dbContext.SaveChanges();
            }
        }

        private Checkout MapModelToObject(CheckoutModel CheckoutModel)
        {
            Checkout Checkout = new Checkout();

            if (CheckoutModel != null)
            {
                Checkout.CheckoutId             = CheckoutModel.CheckoutID;
                Checkout.CheckoutEmployeeName   = CheckoutModel.CheckoutEmployeeName;
                Checkout.CheckoutMemberId       = CheckoutModel.CheckoutMemberID;
                Checkout.CheckoutBookId         = CheckoutModel.CheckoutBookID;
                Checkout.CheckoutCheckoutDate   = CheckoutModel.CheckoutCheckoutDate; 
                Checkout.CheckoutDueDate        = CheckoutModel.CheckoutDueDate;
                Checkout.CheckoutReturnDate     = CheckoutModel.CheckoutReturnDate;
            }

            return Checkout;
        }

        private CheckoutModel MapObjectToModel(Checkout Checkout)
        {
            CheckoutModel CheckoutModel = new CheckoutModel();

            if (Checkout != null)
            {
                CheckoutModel.CheckoutID = Checkout.CheckoutId;
                CheckoutModel.CheckoutEmployeeName = Checkout.CheckoutEmployeeName;
                CheckoutModel.CheckoutMemberID = Checkout.CheckoutMemberId;
                CheckoutModel.CheckoutBookID = Checkout.CheckoutBookId;
                CheckoutModel.CheckoutCheckoutDate = Checkout.CheckoutCheckoutDate;
                CheckoutModel.CheckoutDueDate = Checkout.CheckoutDueDate;
                CheckoutModel.CheckoutReturnDate = Checkout.CheckoutReturnDate;
            }

            return CheckoutModel;
        }

        public CheckoutModel UpdateBookName(CheckoutModel model)
        {
            Book item = new Book();
            item = dbContext.Books.FirstOrDefault(x => x.BookId == model.CheckoutBookID);

            if(item != null)
            {
                model.bookName = item.BookName;
            }

            return model;
        }

        public CheckoutModel UpdateMemberName(CheckoutModel model)
        {
            Member item = new Member();
            item = dbContext.Members.FirstOrDefault(x => x.MemberId == model.CheckoutMemberID);

            if (item != null)
            {
                model.memberName = item.MemberName;
            }

            return model;
        }
    }
}
