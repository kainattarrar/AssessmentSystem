using AssessmentSystem.Service.API.Controllers.Base;
using AssessmentSystem.Service.BusinessLogic;
using AssessmentSystem.Service.Models.Dto;
using AssessmentSystem.Service.Models.Enum;
using AssessmentSystem.Service.Models.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.OpenApi.Extensions;

namespace AssessmentSystem.Service.API.Controllers
{
    public class ResultController : BaseController
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
                string authToken = headerValue;
                CandidateScoreManager candidateScoreManager = new();
                CandidateScoreDto candidateScore = candidateScoreManager.GetCandidateScore(authToken.Substring(7));
                return Ok(new Result<CandidateScoreDto>() { Success = true, Data = candidateScore });
            }
            catch (Exception ex)
            {
                return BadRequest(new Result() { Success = false, Message = ex.Message });
            }
        }
    }
}
