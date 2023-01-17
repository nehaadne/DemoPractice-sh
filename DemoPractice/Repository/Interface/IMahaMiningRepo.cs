using DemoPractice.Model;

namespace DemoPractice.Repository.Interface
{
    public interface IMahaMiningRepo
    {
        public Task<BaseResponseModel> GetAll(long PlotId,long OwnerId, int pageno, int pagesize);

    }
}
