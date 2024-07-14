using AssessmentSystem.Service.Data.UnitOfWorks;
using AssessmentSystem.Service.Entities;
using AssessmentSystem.Service.Models.Dto;
using AssessmentSystem.Service.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.BusinessLogic
{
    public class QuestionManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public QuestionManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public QuestionManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<QuestionResultDto> GetQuestionList()
        {
            Random rnd = new Random();
            int count = _unitOfWork.Questions.Get().Count();
            int num = rnd.Next(0, _unitOfWork.Questions.Get().Count());
            var result = _unitOfWork.Questions.Get().Skip(num).Take(20).Select(ToResult).ToList();
            ChoiceManager cm = new();
            result.ForEach(x =>
            {
                if (x.AnswerType == AnswerTypes.MultipleChoice)
                    x.Choices = cm.GetQuestionChoices(x.Id);
            });
            return result;
        }

        private Expression<Func<Question, QuestionResultDto>> ToResult => question =>
new QuestionResultDto()
{
    AnswerType = question.AnswerType,
    Id = question.Id,
    QuestionText = question.QuestionText,
    Time = question.Time
};

    }
}
