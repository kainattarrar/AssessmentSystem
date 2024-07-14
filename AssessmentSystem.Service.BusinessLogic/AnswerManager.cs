using AssessmentSystem.Service.Data.UnitOfWorks;
using AssessmentSystem.Service.Entities;
using AssessmentSystem.Service.Models.Dto;
using AssessmentSystem.Service.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.BusinessLogic
{
    public class AnswerManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnswerManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AnswerManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void InsertAnswer(Answer answer)
        {
            _unitOfWork.Answers.Add(answer);
            _unitOfWork.SaveChanges();
        }
        public List<Answer> GetAnswers(string tokenId)
        {
            return _unitOfWork.Answers.Get(x=>x.CreatedBy == tokenId).ToList();
        }
    }
}
