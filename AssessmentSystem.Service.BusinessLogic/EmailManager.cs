using AssessmentSystem.Service.Data.UnitOfWorks;
using AssessmentSystem.Service.Entities;
using AssessmentSystem.Service.Models.Dto;
using AssessmentSystem.Service.Models.Enum;
using AssessmentSystem.Service.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.BusinessLogic
{
    public class EmailManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmailManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public EmailManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void InsertEmailLog(string token, EmailStatus status)
        {
            Email email = new() { CreatedBy = token, Status = status };
            _unitOfWork.Emails.Add(email);
            _unitOfWork.SaveChanges();
        }
    }
}
