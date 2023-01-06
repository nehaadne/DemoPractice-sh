using DemoPractice.Model;
using DemoPractice.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegistrationController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IUserRegistration userRegistration;
        IConfiguration configuration;
        public UserRegistrationController(IConfiguration configuration, ILoggerFactory loggerFactory, IUserRegistration userRegistration)
        {
            this.logger = loggerFactory.CreateLogger<CommonDropDownController>();
            this.userRegistration = userRegistration;
            this.configuration = configuration;
        }
        [HttpPost("AddUsers")]
        public async Task<IActionResult> AddRecord(UserRegistrationModel userRegistrationModel)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var createduser = await userRegistration.AddUsers(userRegistrationModel);
                if (createduser > 0)
                {

                    var returnStr = string.Format("User Register Successfully.");
                    logger.LogInformation(returnStr);
                    logger.LogDebug(string.Format("UserRegistrationController-AddRecord : User Register Successfully"));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnStr;
                    responseDetails.ResponseData = createduser;
                    return Ok(responseDetails);
                }
                else if (createduser == -1)
                {
                    var returnStr = string.Format("Record is not aaded as there is already a record with same  emailId {0}.", userRegistrationModel.emailId);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnStr;
                    return Ok(responseDetails);
                }
                else if (createduser == -1)
                {
                    var returnStr = string.Format("Record is not aaded as there is already a record with same  mobileNo {0}.", userRegistrationModel.mobileNo);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnStr;
                    return Ok(responseDetails);
                }

                else
                {
                    var msgStr = string.Format("Error while adding User Registration record.");
                    logger.LogInformation(msgStr);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = msgStr;
                    return Ok(responseDetails);
                }
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }
        [HttpDelete("DeleteRecord")]
        public async Task<IActionResult> DeleteOffice(UserRegistrationModel userRegistrationModel)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var deletedept = await userRegistration.DeleteRecord(userRegistrationModel);
                if (deletedept > 0)
                {
                    var returnmsg = string.Format("record Deleted Successfully!");
                    logger.LogDebug(string.Format("UserRegistrationController-Delete : Completed Delete action for Office)"));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);

                }

                else
                {
                    var rtnmsg = string.Format("UserRegistration record with  ID  not found");
                    logger.LogInformation(rtnmsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = rtnmsg;
                    return Ok(responseDetails);

                }
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(string? textSearch, int pageno, int pagesize)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {

                var page = await userRegistration.GetAllUsers(pageno, pagesize, textSearch);
                List<UserRegistrationModel> OfficeModels = (List<UserRegistrationModel>)page.ResponseData1;
                if (OfficeModels.Count == 0)
                {
                    var returnMsg = string.Format("OfficeController-GetAll : Office Record is not available.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                else
                {
                    var rtrMsg = string.Format("All getAll records are fetched successfully.");
                    logger.LogDebug(rtrMsg);
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtrMsg;
                    responseDetails.ResponseData = page;
                    return Ok(responseDetails);
                }

            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }
        [HttpPut("UpdateUsers")]
        public async Task<IActionResult> UpdateUsers(UserRegistrationModel userRegistrationModel)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {

                var updatedata = await userRegistration.UpdateUsers(userRegistrationModel);
                if (updatedata > 0)
                {
                    var returnmsg = string.Format("record Updated Successfully!");
                    logger.LogDebug(string.Format("UserRegistrationController-Update : Completed Update action for UserReg)"));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);

                }


                else if (updatedata == -1)
                {
                    var returnStr = string.Format("Record is not updated as there is already a record with same UserRegistration {0}.", userRegistrationModel.userName);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnStr;
                    return Ok(responseDetails);
                }
                else
                {
                    var msgStr = string.Format("Error while updating Office record.");
                    logger.LogInformation(msgStr);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = msgStr;
                    return Ok(responseDetails);
                }
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {

                logger.LogDebug(string.Format("UserRegistrationController-GetReligionById : Calling GetById"));
                var result = await userRegistration.GetById(Id);
                ReligionModel office = new ReligionModel();

                if (result == null)
                {
                    var returnMsg = string.Format($"No records Found With Id:{Id}");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                var rtrMsg = string.Format(" records are fetched successfully.");
                logger.LogDebug("UserRegistrationController-GetById : Completed Get action all getAll records.");
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtrMsg;
                responseDetails.ResponseData = result;
            }
            catch (Exception ex)
            {
                //log error
                logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
            return Ok(responseDetails);

        }





    }
}

