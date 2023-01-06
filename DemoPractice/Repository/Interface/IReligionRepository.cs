using DemoPractice.Model;

namespace DemoPractice.Repository.Interface
{
    public interface IReligionRepository
    {
        public Task<long> AddReligion(ReligionModel religionModel);
        public Task<long> UpdateReligion(ReligionModel religionModel);
        public Task<long> DeleteReligion(ReligionModel religionModel);
        public Task<ReligionModel> GetReligionById(long Id);
        public Task<List<ReligionModel>> GetAllReligion();


    }
}
