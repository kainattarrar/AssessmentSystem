using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Models.Dto
{
    public class SubmitAnswerDto
    {
        public int QuestionId { get; set; }
        public string UserAnswer { get; set; } = string.Empty;
    }
}
