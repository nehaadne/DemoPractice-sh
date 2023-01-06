using DemoPractice.Model;
using DemoPractice.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReligionController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IReligionRepository religionRepository;
        IConfiguration configuration;
        public ReligionController(IConfiguration configuration, ILoggerFactory loggerFactory, IReligionRepository religionRepository)
        {
            this.logger = loggerFactory.CreateLogger<CommonDropDownController>();
            this.religionRepository = religionRepository;
            this.configuration = configuration;
        }
        [HttpPost("AddReligion")]
        public async Task<IActionResult> AddReligion(ReligionModel religionModel)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var createduser = await religionRepository.AddReligion(religionModel);
                if (createduser > 0)
                {

                    var returnStr = string.Format("Religion added Successfully.");
                    logger.LogInformation(returnStr);
                    logger.LogDebug(string.Format("ReligionController-AddRecord : Religion Successfully"));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnStr;
                    responseDetails.ResponseData = createduser;
                    return Ok(responseDetails);
                }
                else if (createduser == -1)
                {
                    var returnStr = string.Format("Record is not aaded as there is already a record with same  religion {0}.", religionModel.Religion);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnStr;
                    return Ok(responseDetails);
                }

                else
                {
                    var msgStr = string.Format("Error while adding  Religion record.");
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
        [HttpDelete("DeleteReligion")]
        public async Task<IActionResult> DeleteReligion(ReligionModel religionModel)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                var deletedept = await religionRepository.DeleteReligion(religionModel);
                if (deletedept > 0)
                {
                    var returnmsg = string.Format("ReligionRecord Deleted Successfully!");
                    logger.LogDebug(string.Format("ReligionController-Delete : Completed Delete action for Religion)"));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);

                }

                else
                {
                    var rtnmsg = string.Format("Religion record with  ID  not found");
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
        [HttpPut("UpdateReligion")]
        public async Task<IActionResult> UpdateReligion(ReligionModel religionModel)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {

                var updatedata = await religionRepository.UpdateReligion(religionModel);
                if (updatedata > 0)
                {
                    var returnmsg = string.Format("record Updated Successfully!");
                    logger.LogDebug(string.Format("ReligionController-Update : Completed Update action for UserReg)"));
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = returnmsg;
                    return Ok(responseDetails);

                }


                else if (updatedata == -1)
                {
                    var returnStr = string.Format("Record is not updated as there is already a record with same Religion {0}.", religionModel.Religion);
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

        [HttpGet("GetReligionById")]
        public async Task<IActionResult> GetReligionById(int Id)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {

                logger.LogDebug(string.Format("ReligionController-GetReligionById : Calling GetReligionById"));
                var result = await religionRepository.GetReligionById(Id);
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
                logger.LogDebug("ReligionController-GetReligionById : Completed Get action all getAll records.");
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
        [HttpGet("GetAllReligion")]
        public async Task<IActionResult> GetAllReligion()
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                //logger.LogDebug(string.Format("OfficeController-Delete : Completed Delete action for Office)"));

                var Result = await religionRepository.GetAllReligion();
               


                if (Result.Count == 0)
                {
                    var returnMsg = string.Format("ReligionController-GetAllReligion : Religion Record is not available.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    return Ok(responseDetails);
                }
                else
                {
                    var rtrMsg = string.Format("All getAll records are fetched successfully.");
                    logger.LogDebug(rtrMsg);
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtrMsg;
                    responseDetails.ResponseData = Result;

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



    }
}

