using project.Data;
using project.Models.DBObjects;
using project.Models;

namespace project.Repository
{
    public class PublisherRepository
    {
        private ApplicationDbContext dbContext;

        public PublisherRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        public PublisherRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<PublisherModel> GetAllPublishers()
        {
            List<PublisherModel> PublisherList = new List<PublisherModel>();

            foreach (Publisher dbPublisher in dbContext.Publishers)
            {
                PublisherList.Add(MapObjectToModel(dbPublisher));
            }

            return PublisherList;
        }

        public PublisherModel GetPublisherModel(Guid ID)
        {
            return MapObjectToModel(dbContext.Publishers.FirstOrDefault(x => x.PublisherId == ID));
        }

        public void InsertPublisher(PublisherModel PublisherModel)
        {
            PublisherModel.PublisherID = Guid.NewGuid();

            dbContext.Publishers.Add(MapModelToObject(PublisherModel));
            dbContext.SaveChanges();
        }

        public void UpdatePublisher(PublisherModel PublisherModel)
        {
            Publisher existingPublisher = dbContext.Publishers.FirstOrDefault(x => x.PublisherId == PublisherModel.PublisherID);

            if (existingPublisher != null)
            {
                existingPublisher.PublisherId = PublisherModel.PublisherID;
                existingPublisher.PublisherName = PublisherModel.PublisherName;

                dbContext.SaveChanges();
            }
        }

        public void DeletePublisher(Guid id)
        {
            Publisher existingPublisher = dbContext.Publishers.FirstOrDefault(x => x.PublisherId == id);

            if (existingPublisher != null)
            {
                dbContext.Publishers.Remove(existingPublisher);
                dbContext.SaveChanges();
            }
        }

        private Publisher MapModelToObject(PublisherModel PublisherModel)
        {
            Publisher Publisher = new Publisher();

            if (PublisherModel != null)
            {
                Publisher.PublisherId = PublisherModel.PublisherID;
                Publisher.PublisherName = PublisherModel.PublisherName;
            }

            return Publisher;
        }

        private PublisherModel MapObjectToModel(Publisher Publisher)
        {
            PublisherModel PublisherModel = new PublisherModel();

            if (Publisher != null)
            {
                PublisherModel.PublisherID = Publisher.PublisherId;
                PublisherModel.PublisherName = Publisher.PublisherName;
            }

            return PublisherModel;
        }
    }
}
