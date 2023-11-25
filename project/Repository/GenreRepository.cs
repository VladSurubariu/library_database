using project.Data;
using project.Models.DBObjects;
using project.Models;

namespace project.Repository
{
    public class GenreRepository
    {
        private ApplicationDbContext dbContext;

        public GenreRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        public GenreRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<GenreModel> GetAllGenres()
        {
            List<GenreModel> GenreList = new List<GenreModel>();

            foreach (Genre dbGenre in dbContext.Genres)
            {
                GenreList.Add(MapObjectToModel(dbGenre));
            }

            return GenreList;
        }

        public GenreModel GetGenreModel(Guid ID)
        {
            return MapObjectToModel(dbContext.Genres.FirstOrDefault(x => x.GenreId == ID));
        }

        public void InsertGenre(GenreModel GenreModel)
        {
            GenreModel.GenreID = Guid.NewGuid();

            dbContext.Genres.Add(MapModelToObject(GenreModel));
            dbContext.SaveChanges();
        }

        public void UpdateGenre(GenreModel GenreModel)
        {
            Genre existingGenre = dbContext.Genres.FirstOrDefault(x => x.GenreId == GenreModel.GenreID);

            if (existingGenre != null)
            {
                existingGenre.GenreId = GenreModel.GenreID;
                existingGenre.GenreName = GenreModel.GenreName;

                dbContext.SaveChanges();
            }
        }

        public void DeleteGenre(Guid id)
        {
            Genre existingGenre = dbContext.Genres.FirstOrDefault(x => x.GenreId == id);

            if (existingGenre != null)
            {
                dbContext.Genres.Remove(existingGenre);
                dbContext.SaveChanges();
            }
        }

        private Genre MapModelToObject(GenreModel GenreModel)
        {
            Genre Genre = new Genre();

            if (GenreModel != null)
            {
                Genre.GenreId = GenreModel.GenreID;
                Genre.GenreName = GenreModel.GenreName;
            }

            return Genre;
        }

        private GenreModel MapObjectToModel(Genre Genre)
        {
            GenreModel GenreModel = new GenreModel();

            if (Genre != null)
            {
                GenreModel.GenreID = Genre.GenreId;
                GenreModel.GenreName = Genre.GenreName;
            }

            return GenreModel;
        }
    }
}
