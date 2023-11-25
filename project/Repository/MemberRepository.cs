using project.Data;
using project.Models.DBObjects;
using project.Models;

namespace project.Repository
{
    public class MemberRepository
    {
        private ApplicationDbContext dbContext;

        public MemberRepository()
        {
            dbContext = new ApplicationDbContext();
        }

        public MemberRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<MemberModel> GetAllMembers()
        {
            List<MemberModel> MemberList = new List<MemberModel>();

            foreach (Member dbMember in dbContext.Members)
            {
                MemberList.Add(MapObjectToModel(dbMember));
            }

            return MemberList;
        }

        public MemberModel GetMemberModel(Guid ID)
        {
            return MapObjectToModel(dbContext.Members.FirstOrDefault(x => x.MemberId == ID));
        }

        public void InsertMember(MemberModel MemberModel)
        {
            MemberModel.MemberID = Guid.NewGuid();

            dbContext.Members.Add(MapModelToObject(MemberModel));
            dbContext.SaveChanges();
        }

        public void UpdateMember(MemberModel MemberModel)
        {
            Member existingMember = dbContext.Members.FirstOrDefault(x => x.MemberId == MemberModel.MemberID);

            if (existingMember != null)
            {
                existingMember.MemberId = MemberModel.MemberID;
                existingMember.MemberName = MemberModel.MemberName;

                dbContext.SaveChanges();
            }
        }

        public void DeleteMember(Guid id)
        {
            Member existingMember = dbContext.Members.FirstOrDefault(x => x.MemberId == id);

            if (existingMember != null)
            {
                dbContext.Members.Remove(existingMember);
                dbContext.SaveChanges();
            }
        }

        private Member MapModelToObject(MemberModel MemberModel)
        {
            Member Member = new Member();

            if (MemberModel != null)
            {
                Member.MemberId = MemberModel.MemberID;
                Member.MemberName = MemberModel.MemberName;
            }

            return Member;
        }

        private MemberModel MapObjectToModel(Member Member)
        {
            MemberModel MemberModel = new MemberModel();

            if (Member != null)
            {
                MemberModel.MemberID = Member.MemberId;
                MemberModel.MemberName = Member.MemberName;
            }

            return MemberModel;
        }
    }
}
