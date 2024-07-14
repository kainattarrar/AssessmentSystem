using AssessmentSystem.Service.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Models.Dto
{
    public class QuestionResultDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public AnswerTypes AnswerType { get; set; }
        public List<QuestionChoiceResultDto> Choices { get; set; }
        public int Time {  get; set; }
    }
}
