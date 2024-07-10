using AssessmentSystem.Service.Models.Enum;
using AssessmentSystem.Service.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Entities
{
    public class Question : EntityBase , IEntity
    {
        public string QuestionText { get; set; }
        public int Time { get; set; }
        public int Points { get; set; }
        public AnswerTypes AnswerType { get; set; }
        public virtual ICollection<Choice> Choices { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
