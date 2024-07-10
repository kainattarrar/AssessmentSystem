using AssessmentSystem.Service.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Entities
{
    public class Answer : EntityBase, IEntity
    {
        public int QuestionId { get; set; }
        public string UserAnswer { get; set; }
        public virtual Question Question { get; set; }
    }
}
