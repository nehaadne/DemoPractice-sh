using DemoPractice.Model;
using DemoPractice.Repository.Interface;
using System.Data.Common;
using Dapper;

namespace DemoPractice.Repository
{
    public class MahaMiningRepo : BaseAsyncRepository, IMahaMiningRepo
    {
        public MahaMiningRepo(IConfiguration configuration) : base(configuration)
        { }
        public async Task<BaseResponseModel> GetAll(long PlotId, long OwnerId ,int pageno, int pagesize)
        {
            BaseResponseModel baseResponse = new BaseResponseModel();
            Pagination paginations = new Pagination();
            List<Mahamining> userlist = new List<Mahamining>();



            var query = @"select pr.Id as PlotId, od.Id as OwnerId, pr.PlotName, pr.Address as PlotAddress,
                            pa.AuctionOrderNo,pa.Quantity,od.Name as Ownername,od.Address as OwnerAdd, od.MobileNo1, inv.InvoiceNo
                            from tblplotregister pr
                             Inner JOIN  tblplotassign pa on  pr.Id=pa.PlotId
                             Inner JOIN tblownerdetails od on pr.OwnerId=od.Id
                             Inner JOIN tblinvoice inv on pr.Id=inv.PlotId and pa.OwnerId=inv.OwnerId
                            where (@PlotId=0 or pr.Id=@PlotId) and (@OwnerId=0 or pr.OwnerId=@OwnerId)

                            order By pr.Id desc

                            offset(@pageno - 1) * @pagesize rows fetch next @pagesize rows only; 

                            select @pageno as PageNo, Count(distinct (pr.Id)) as TotalPages 

                            from tblplotregister pr
                            Inner JOIN  tblplotassign pa on  pr.Id=pa.PlotId
                            Inner JOIN tblownerdetails od on pr.OwnerId=od.Id
                            Inner JOIN  tblinvoice inv on pr.Id=inv.PlotId and pa.OwnerId=inv.OwnerId
                            where (@PlotId=0 or pr.Id=@PlotId) and (@OwnerId=0 or pr.OwnerId=@OwnerId)";

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
                //if (textSearch == null)
                //{
                //    textSearch = "";
                //}

                var result = await dbConnection.QueryMultipleAsync(query, new {PlotId,OwnerId, pageno = pageno, pagesize = pagesize});
                var dataList = await result.ReadAsync<Mahamining>();
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
    }
}

