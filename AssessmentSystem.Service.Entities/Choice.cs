using AssessmentSystem.Service.Shared.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Entities
{
    public class Choice : EntityBase, IEntity
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
        public bool IsTrue { get; set; }
        public virtual Question Question { get; set; }
    }
}
