using AssessmentSystem.Service.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Models.Dto
{
    public class CandidateScoreDto
    {
        public int ScoreEarned { get; set; }
        public int TotalScore { get; set; }
        public Rank Rank { get; set; }
    }
}
