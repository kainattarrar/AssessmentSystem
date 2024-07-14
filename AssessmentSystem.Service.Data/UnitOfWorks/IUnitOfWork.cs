using AssessmentSystem.Service.Data.Repositories;
using AssessmentSystem.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Data.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IGenericRepository<Question> Questions { get; }
        IGenericRepository<Answer> Answers { get; }
        IGenericRepository<Choice> Choices { get; }
        IGenericRepository<CandidateScore> CandidateScores { get; }
        IGenericRepository<Email> Emails { get; }

        int SaveChanges();
        void BeginTransaction();
        void Commit();
        void Rollback();
    }
}
