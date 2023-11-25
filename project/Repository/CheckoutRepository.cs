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
                Checkout.CheckoutId = CheckoutModel.CheckoutID;
                Checkout.CheckoutEmployeeName = CheckoutModel.CheckoutEmployeeName;
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
            }

            return CheckoutModel;
        }
    }
}
