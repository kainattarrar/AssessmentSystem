using AssessmentSystem.Service.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Models.Dto
{
    public class CorrectChoiceDto
    {
        public string Answer { get; set; }
        public int Points { get; set; }
        public AnswerTypes AnswerType { get; set; }
    }
}
