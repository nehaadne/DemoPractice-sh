using DemoPractice.Model;
using DemoPractice.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MahaMiningController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IMahaMiningRepo mahaMiningRepo;
        IConfiguration configuration;
        public MahaMiningController(IConfiguration configuration, ILoggerFactory loggerFactory, IMahaMiningRepo mahaMiningRepo)
        {
            this.logger = loggerFactory.CreateLogger<Mahamining>();
            this.mahaMiningRepo = mahaMiningRepo;
            this.configuration = configuration;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll(long PlotId, long OwnerId, int pageno, int pagesize)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {

                var page = await mahaMiningRepo.GetAll(PlotId, OwnerId,pageno, pagesize);
                List<Mahamining> OfficeModels = (List<Mahamining>)page.ResponseData1;
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
    }
}
