using DemoPractice.Model;
using DemoPractice.Repository.Interface;
using System.Data.Common;
using Dapper;

using System.Data;

namespace DemoPractice.Repository
{
    public class UserRegistration : BaseAsyncRepository, IUserRegistration
    {
        public UserRegistration(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<long> AddUsers(UserRegistrationModel userRegistrationModel)
        {
            userRegistrationModel.isDeleted = false;
            userRegistrationModel.createDate = DateTime.Now;
            using (DbConnection dbConnection = sqlreaderConnection)
            {
                var query = @"INSERT INTO tblUserRegistration (Name,userName,mobileNo,emailId,districtId,talukaId,
                             vilageId,password,createdBy,createDate,isDeleted)
                             VALUES(@Name,@userName,@mobileNo,@emailId,@districtId,@talukaId,@vilageId,@password,
                             @createdBy,@createDate,@isDeleted)   
                             SELECT CAST(SCOPE_IDENTITY() as bigint)";
                var duplicateemailid = await dbConnection.QueryAsync(@"SELECT emailId FROM tblUserRegistration WHERE emailId=@emailId AND  isDeleted=0",
                                                                        new { emailId = userRegistrationModel.emailId });
                var FirstuserByEmailid = duplicateemailid.FirstOrDefault();
                if (FirstuserByEmailid != null)
                {
                    return -1; //for duplicate emailid
                }
                var userListBymobileno = await dbConnection.QueryAsync(@"SELECT mobileNo FROM tblUserRegistration WHERE  mobileNo=@mobileNo  AND isDeleted=0",
                                                               new { mobileNo = userRegistrationModel.mobileNo });


                var FirstuserByMobileNo = userListBymobileno.FirstOrDefault();
                if (FirstuserByMobileNo != null)
                {
                    return -2; //for duplicate Value with same mobile no 
                }


                var result = await dbConnection.QuerySingleAsync<int>(query, userRegistrationModel);

                return result;
            }
        }





        public async Task<long> DeleteRecord(UserRegistrationModel userRegistrationModel)
        {
            int result = 0;

            using (DbConnection dbConnection = sqlreaderConnection)
            {

                var query1 = "UPDATE tblUserRegistration SET isDeleted = 1, modifiedBy = @modifiedBy, modifiedDate = GETDATE() WHERE Id = @Id AND isDeleted = 0";
                result = await dbConnection.ExecuteAsync(query1, userRegistrationModel);

            }

            return result;
        }

        public async Task<BaseResponseModel> GetAllUsers(int pageno, int pagesize, string? textSearch)
        {
            BaseResponseModel baseResponse = new BaseResponseModel();
            Pagination paginations = new Pagination();
            List<UserRegistrationModel> userlist = new List<UserRegistrationModel>();



            var query = @"select u.Id,u.Name,u.userName,u.mobileNo,u.emailId,

               u.password,d.districtName,t.talukaName,v.villageName,

               u.createdBy,u.createDate,u.modifiedBy,u.modifiedDate
              
               from  tblUserRegistration u 

              left join tblDistrinct d on u.districtId=d.Id

              left join tblTaluka t on u.talukaId=t.Id

              left join tblVillage v on u.vilageId=v.Id
                      
              where (u.userName  LIKE '%' + @textSearch + '%' or @textSearch='')

               or (u.emailId LIKE '%' + @textSearch + '%' or @textSearch='')

               or (u.mobileNo LIKE '%' + @textSearch + '%' or @textSearch='')

               and u.isDeleted=0

              order By u.Id desc

              offset(@pageno - 1) * @pagesize rows fetch next @pagesize rows only; 

              select @pageno as PageNo, Count(distinct (u.Id)) as TotalPages 

              from  tblUserRegistration u 

              left join tblDistrinct d on u.districtId=d.Id

              left join tblTaluka t on u.talukaId=t.Id 

              left join tblVillage v on u.vilageId=v.Id

              where (u.userName  LIKE '%' + @textSearch + '%' or @textSearch='')

               or (u.emailId LIKE '%' + @textSearch + '%' or @textSearch='')

               or (u.mobileNo LIKE '%' + @textSearch + '%' or @textSearch='')

               and u.isDeleted=0";

            using (DbConnection dbConnection = sqlwriterConnection)
            {

                if (pageno == 0)
                {
                    pageno = 1;
                }
                if (pagesize == 0)
                {
                    pagesize = 10;
                }
                if (textSearch == null)
                {
                    textSearch = "";
                }

                var result = await dbConnection.QueryMultipleAsync(query, new { pageno = pageno, pagesize = pagesize, textSearch = textSearch });
                var dataList = await result.ReadAsync<UserRegistrationModel>();
                var pagination = await result.ReadAsync<Pagination>();
                userlist = dataList.ToList();
                paginations = pagination.FirstOrDefault();
                int PageCount = 0;
                int last = 0;
                int cnt = 0;
                cnt = paginations.TotalPages;
                last = paginations.TotalPages % pagesize;
                PageCount = paginations.TotalPages / pagesize;
                paginations.TotalPages = PageCount;
                paginations.PageCount = cnt;
                if (last > 0)
                {
                    paginations.TotalPages = PageCount + 1;
                }
                baseResponse.ResponseData1 = userlist;
                baseResponse.ResponseData2 = paginations;
                return baseResponse;
            }
        }

        public async Task<UserRegistrationModel> GetById(long Id)
        {
            UserRegistrationModel userModel = null;

            using (DbConnection dbConnection = sqlreaderConnection)
            {
                await dbConnection.OpenAsync();
                var result = await dbConnection.QueryAsync<UserRegistrationModel>(@"select Id,Name,userName,mobileNo,emailId,
                                                            districtId,talukaId,vilageId,password,createdBy,createDate,
                                                                           modifiedDate, modifiedBy from tblUserRegistration
                                                             where Id=@Id  and IsDeleted=0 ", new { Id = Id });

                userModel = result.FirstOrDefault();
            }
            return userModel;
        }


        public async Task<long> UpdateUsers(UserRegistrationModel userRegistrationModel)
        {
            int result = 0;
            var query = @"update tblUserRegistration set Name=@Name,userName=@userName,mobileNo=@mobileNo,
                      emailId=@emailId,districtId=@districtId,talukaId=@talukaId,vilageId=@vilageId,password=@password,modifiedBy=@modifiedBy,modifiedDate=getdate()
                          where Id=@Id and isDeleted=0";

            using (DbConnection dbConnection = sqlreaderConnection)
            {

                var duplicateoffice = await dbConnection.QueryAsync(@"SELECT userName FROM tblUserRegistration WHERE userName=@userName AND isDeleted=0 and Id != @Id",
                                                                       new { userName = userRegistrationModel.userName, Id = userRegistrationModel.Id });

                var FirstuserByemailId = duplicateoffice.FirstOrDefault();
                if (FirstuserByemailId != null)
                {
                    return -1; //for duplicate religion
                }
                result = await dbConnection.ExecuteAsync(query, userRegistrationModel);
                return result;
            }
        }
    }
}   

   

