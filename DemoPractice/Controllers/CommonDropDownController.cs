using DemoPractice.Model;
using DemoPractice.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonDropDownController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly ICommonDropDownRepository commonDropDown;
        IConfiguration configuration;
        public CommonDropDownController(IConfiguration configuration, ILoggerFactory loggerFactory, ICommonDropDownRepository commonDropDown)
        {
            this.logger = loggerFactory.CreateLogger<CommonDropDownController>();
            this.commonDropDown = commonDropDown;
            this.configuration = configuration;
        }
        [HttpGet("GetAllDistrict")]
        public async Task<ActionResult> GetAllDistrict()
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                logger.LogDebug(string.Format("MasterController-getAll : Calling getAll"));
                var dist = await commonDropDown.GetAllDistrict();

                if (dist.Count == 0)
                {
                    var returnMsg = string.Format("There are not any records for getAll.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                var rtrMsg = string.Format("All records are fetched successfully.");
                logger.LogDebug("MasterController-getAll : Completed Get action all getAll records.");
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtrMsg;
                responseDetails.ResponseData = dist;
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
        [HttpGet("GetAllTaluka")]
        public async Task<ActionResult> GetAllTaluka()
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                logger.LogDebug(string.Format("MasterController-getAll : Calling getAll"));
                var taluka = await commonDropDown.GetAllTaluka();

                if (taluka.Count == 0)
                {
                    var returnMsg = string.Format("There are not any records for getAll.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                var rtrMsg = string.Format("All  records are fetched successfully.");
                logger.LogDebug("MasterController-getAll : Completed Get action all getAll records.");
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtrMsg;
                responseDetails.ResponseData = taluka;
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
        [HttpGet("GetAllVillage")]
        public async Task<ActionResult> GetAllVillage()
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                logger.LogDebug(string.Format("MasterController-getAll : Calling getAll"));
                var village = await commonDropDown.GetAllVillage();

                if (village.Count == 0)
                {
                    var returnMsg = string.Format("There are not any records for getAll.");
                    logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                var rtrMsg = string.Format("All  records are fetched successfully.");
                logger.LogDebug("MasterController-getAll : Completed Get action all getAll records.");
                responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                responseDetails.StatusMessage = rtrMsg;
                responseDetails.ResponseData = village;
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
