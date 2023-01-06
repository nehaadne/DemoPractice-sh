using Dapper;
using DemoPractice.Model;
using DemoPractice.Repository.Interface;
using System.Data.Common;

namespace DemoPractice.Repository
{
    public class CommonDropDownRepository : BaseAsyncRepository, ICommonDropDownRepository
    {
        public CommonDropDownRepository(IConfiguration configuration) : base(configuration)
        { }

        public async Task<List<DistrictDropDown>> GetAllDistrict()
        {
            var query =  @"select Id, districtName from tblDistrinct where IsDeleted = 0 ";

            List<DistrictDropDown> districtDropDowns = new List<DistrictDropDown>();
                
            
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                var stateMasters = await dbConnection.QueryAsync<DistrictDropDown>(query);
                districtDropDowns = stateMasters.ToList();
                return districtDropDowns;
            }
        }


        public async Task<List<TalukaDropDown>> GetAllTaluka()
        {
            var query = "";
            List<TalukaDropDown> talukaMasters = new List<TalukaDropDown>();

            query = @"select Id,talukaName,districtId from tblTaluka where IsDeleted=0 ";

            using (DbConnection dbConnection = sqlreaderConnection)
            {
                var talukas = await dbConnection.QueryAsync<TalukaDropDown>(query);
                talukaMasters = talukas.ToList();
                return talukaMasters;
            }
        }

            public async Task<List<VillageDropDown>> GetAllVillage()
        {
            var query = "";
            List<VillageDropDown> villageMasters = new List<VillageDropDown>();
                query = @"select Id,villageName,districtId,talukaId from tblVillage where IsDeleted=0 ";
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                var villages = await dbConnection.QueryAsync<VillageDropDown>(query);
                villageMasters = villages.ToList();
                return villageMasters;
            }
        }
    }
}
