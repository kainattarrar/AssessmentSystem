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
    public class CandidateScoreManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public CandidateScoreManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CandidateScoreManager()
        {
            _unitOfWork = new UnitOfWork();
        }

        public CandidateScoreDto GetCandidateScore(string token)
        {
            return _unitOfWork.CandidateScores.Get(x => x.CreatedBy == token).Select(ToResult).FirstOrDefault();
        }
        public CandidateScore InsertCandidateScore(CandidateScore candidateScore)
        {
            _unitOfWork.CandidateScores.Add(candidateScore);
            _unitOfWork.SaveChanges();
            return candidateScore;
        }

        private Expression<Func<CandidateScore, CandidateScoreDto>> ToResult => candidatescores =>
new CandidateScoreDto()
{
    Rank = candidatescores.Rank,
    ScoreEarned = candidatescores.ScoreEarned,
    TotalScore = candidatescores.TotalScore
};
    }
}
