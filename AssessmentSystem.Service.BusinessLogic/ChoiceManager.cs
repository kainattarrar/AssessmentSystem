using AssessmentSystem.Service.Data.UnitOfWorks;
using AssessmentSystem.Service.Entities;
using AssessmentSystem.Service.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.BusinessLogic
{
    public class ChoiceManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChoiceManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ChoiceManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public List<QuestionChoiceResultDto> GetQuestionChoices(int questionId)
        {
            return _unitOfWork.Choices.Get(x=> x.QuestionId == questionId).Select(ToResult).ToList();
        }

        public CorrectChoiceDto GetCorrectChoice(int questionId)
        {

            var correctChoice = (from choice in _unitOfWork.Choices.Get()
                                  join question in _unitOfWork.Questions.Get()
                                  on choice.QuestionId equals question.Id
                                  where choice.QuestionId == questionId && choice.IsTrue
                                  select new CorrectChoiceDto
                                  {
                                      Answer = choice.Answer,
                                      Points = question.Points,
                                      AnswerType = question.AnswerType
                                  }).FirstOrDefault();

            return correctChoice;
        }

        private Expression<Func<Choice, QuestionChoiceResultDto>> ToResult => choices =>
new QuestionChoiceResultDto()
{
    Id = choices.Id,
    Answer = choices.Answer
};
    }

}
