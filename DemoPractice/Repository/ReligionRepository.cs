using DemoPractice.Model;
using DemoPractice.Repository.Interface;
using System.Data.Common;
using Dapper;

namespace DemoPractice.Repository
{
    public class ReligionRepository : BaseAsyncRepository, IReligionRepository
    {
        public ReligionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<long> AddReligion(ReligionModel religionModel)
        {
            religionModel.IsDeleted = false;
            religionModel.CreatedDate = DateTime.Now;
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                var query = @"INSERT INTO tblReligion (Religion,CreatedBy,CreatedDate,IsDeleted)
                             VALUES(@Religion,@CreatedBy,@CreatedDate,@IsDeleted)   
                             SELECT CAST(SCOPE_IDENTITY() as bigint)";
                var duplicateemailid = await dbConnection.QueryAsync(@"SELECT Religion FROM tblReligion WHERE Religion=@Religion AND  IsDeleted=0",
                                                                        new { Religion = religionModel.Religion });
                var FirstuserByEmailid = duplicateemailid.FirstOrDefault();
                if (FirstuserByEmailid != null)
                {
                    return -1; //for duplicate religion
                }
                var result = await dbConnection.QuerySingleAsync<int>(query, religionModel);

                return result;
            }
        }

        public async Task<long> DeleteReligion(ReligionModel religionModel)
        {

            int result = 0;

            using (DbConnection dbConnection = sqlreaderConnection)
            {

                var query1 = "UPDATE tblReligion SET isDeleted = 1, ModifiedBy = @ModifiedBy," +
                             "ModifiedDate = GETDATE() WHERE Id = @Id AND IsDeleted = 0";
                result = await dbConnection.ExecuteAsync(query1, religionModel);

            }

            return result;
        }

        public async Task<List<ReligionModel>> GetAllReligion()
        {
            var query1 = "select * from tblReligion WHERE  IsDeleted = 0";
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                await dbConnection.OpenAsync();
                var result = await dbConnection.QueryAsync<ReligionModel>(query1);
                return result.ToList();




            }

        }

        public async Task<ReligionModel> GetReligionById(long Id)
        {
            ReligionModel religionModel = null;

            using (DbConnection dbConnection = sqlreaderConnection)
            {
                await dbConnection.OpenAsync();
                var user = await dbConnection.QueryAsync<ReligionModel>(@"select Id,Religion from tblReligion 
                                                             where Id=@Id  and IsDeleted=0 ", new { Id = Id });


                religionModel = user.FirstOrDefault();
            }
            return religionModel;
        }

        public async Task<long> UpdateReligion(ReligionModel religionModel)
        {
            int result = 0;
            var query = @"update tblReligion set Religion=@Religion,ModifiedBy=@ModifiedBy,ModifiedDate=getdate()
                          where Id=@Id and IsDeleted=0";


            using (DbConnection dbConnection = sqlreaderConnection)
            {

                var duplicateoffice = await dbConnection.QueryAsync(@"SELECT Religion FROM tblReligion WHERE Religion=@Religion AND IsDeleted=0 and Id != @Id",
                                                                       new { Religion = religionModel.Religion, Id = religionModel.Id});

                var FirstuserByemailId = duplicateoffice.FirstOrDefault();
                if (FirstuserByemailId != null)
                {
                    return -1; //for duplicate religion
                }
                result = await dbConnection.ExecuteAsync(query, religionModel);
                return result;
            }
        }
    }
}
