using DemoPractice.Model;

namespace DemoPractice.Repository.Interface
{
    public interface IUserRegistration
    {
        public Task<long> AddUsers(UserRegistrationModel userRegistrationModel);
        public Task<long> UpdateUsers(UserRegistrationModel userRegistrationModel);
        public Task<long> DeleteRecord(UserRegistrationModel userRegistrationModel);
        public Task<UserRegistrationModel> GetById(long Id);

        public Task<BaseResponseModel> GetAllUsers(int pageno, int pagesize, string? textSearch);




    }
}
