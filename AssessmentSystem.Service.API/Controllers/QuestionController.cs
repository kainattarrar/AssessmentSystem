using AssessmentSystem.Service.API.Controllers.Base;
using AssessmentSystem.Service.BusinessLogic;
using AssessmentSystem.Service.Models.Dto;
using AssessmentSystem.Service.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AssessmentSystem.Service.API.Controllers
{
    public class QuestionController : BaseController
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        [HttpGet("GetQuestionList")]
        public ActionResult<ResultList<QuestionResultDto>> GetQuestionsList()
        {
            try
            {
                QuestionManager questionManager = new();
                var result = questionManager.GetQuestionList();
                return Ok(new ResultList<QuestionResultDto>() { Success = true, Data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultList<QuestionResultDto>() { Success = false, Message = ex.Message });
            }
        }
    }
}
