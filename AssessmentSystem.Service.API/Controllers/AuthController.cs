using AssessmentSystem.Service.API.Controllers.Base;
using AssessmentSystem.Service.API.Services;
using AssessmentSystem.Service.API.Services.EmailService;
using AssessmentSystem.Service.API.Services.RedisCacheService;
using AssessmentSystem.Service.BusinessLogic;
using AssessmentSystem.Service.Data.Repositories;
using AssessmentSystem.Service.Entities;
using AssessmentSystem.Service.Models.Dto;
using AssessmentSystem.Service.Models.Enum;
using AssessmentSystem.Service.Models.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AssessmentSystem.Service.API.Controllers
{
    public class AuthController : BaseController
    {
        private readonly TokenService _tokenService;
        private readonly IRedisCache _redisCache;
        private readonly IEmailService _emailService;
        public AuthController(TokenService tokenService, IEmailService emailService, IRedisCache redisCache)
        {
            _tokenService = tokenService;
            _emailService = emailService;
            _redisCache = redisCache;
        }

        [AllowAnonymous]
        [HttpPost("GetToken")]
        public async Task<IActionResult> GetToken([FromBody] string email)
        {
            try
            {
                var token = _tokenService.GenerateToken(email);
                await _redisCache.Add(email, token);
                await _emailService.SendEmailAsync(email, "Your Token Information: ", token);
                EmailManager emailManager = new();
                emailManager.InsertEmailLog(token, EmailStatus.Sent);
                return Ok(new Result<string>() { Success = true, Data = token });
            }

            catch (Exception ex)
            {
                return BadRequest(new Result() { Success = false, Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("GetCache")]
        public async Task<string?> GetCache(string key)
        {
            try
            {
                var byteData = await _redisCache.Get(key);
                if (byteData != null)
                    return Encoding.UTF8.GetString(byteData);
                return default;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
