using AssessmentSystem.Service.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text;
using System.Threading.Channels;
using RabbitMQ.Client;
using AssessmentSystem.Service.Models.Dto;
using AssessmentSystem.Service.BusinessLogic;
using Microsoft.Extensions.Primitives;
using AssessmentSystem.Service.Models.Shared;
using AssessmentSystem.Service.API.Controllers.Base;
using AssessmentSystem.Service.Data.Repositories;
using AssessmentSystem.Service.API.Services.RabbitMQService;

namespace AssessmentSystem.Service.API.Controllers
{
    public class AnswerController : BaseController
    {
        private readonly IRabbitMqPublisher _rabbitMqPublisher;
        private IConfiguration _configuration;

        public AnswerController(IRabbitMqPublisher rabbitMqPublisher, IConfiguration configuration)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
            _configuration = configuration;
        }

        [HttpPost("SubmitAnswer")]
        public IActionResult SubmitAnswer([FromBody] SubmitAnswerDto submittedAnswer)
        {
            try
            {
                Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
                string authToken = headerValue;
                Answer answer = new() { QuestionId = submittedAnswer.QuestionId, UserAnswer = submittedAnswer.UserAnswer, CreatedBy = authToken.Substring(7) };
                AnswerManager answerManager = new();
                answerManager.InsertAnswer(answer);
                return Ok(new Result() { Success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new Result() { Success = false, Message = ex.Message });
            }
        }

        [HttpPost("EndExam")]
        public IActionResult EndExam()
        {
            try
            {
                string queueName = _configuration.GetValue<string>("RabbitMqQueue:ExamScoringQueue");
                Request.Headers.TryGetValue("Authorization", out StringValues headerValue);
                string authToken = headerValue;
                _rabbitMqPublisher.Publish(authToken.Substring(7), queueName);

                return Ok(new Result() { Success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new Result() { Success = false, Message = ex.Message });
            }
        }
    }
}
