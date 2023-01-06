using DemoPractice.Model;

namespace DemoPractice.Repository.Interface
{
    public interface ICommonDropDownRepository
    {
        public Task<List<DistrictDropDown>> GetAllDistrict();
        public Task<List<TalukaDropDown>> GetAllTaluka();
        public Task<List<VillageDropDown>> GetAllVillage();
        

    }
}
