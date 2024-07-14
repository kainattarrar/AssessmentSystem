using AssessmentSystem.Service.Data.Contexts;
using AssessmentSystem.Service.Data.Repositories;
using AssessmentSystem.Service.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDbContextTransaction _transaction;
        private readonly DataContext _dataContext;
        private IGenericRepository<Question> _questions { get; set; }
        private IGenericRepository<Answer> _answers { get; set; }
        private IGenericRepository<Choice> _choices { get; set; }
        private IGenericRepository<CandidateScore> _candidatescores { get; set; }
        private IGenericRepository<Email> _emails { get; set; }


        public IGenericRepository<Question> Questions => _questions ?? new GenericReposity<Question>(_dataContext);

        public IGenericRepository<Answer> Answers => _answers ?? new GenericReposity<Answer>(_dataContext);

        public IGenericRepository<Choice> Choices => _choices ?? new GenericReposity<Choice>(_dataContext);
        public IGenericRepository<CandidateScore> CandidateScores => _candidatescores ?? new GenericReposity<CandidateScore>(_dataContext);
        public IGenericRepository<Email> Emails => _emails ?? new GenericReposity<Email>(_dataContext);

        public UnitOfWork()
        {
            _dataContext = new DataContext();
        }

        public void BeginTransaction()
        {
            if(_transaction == null)
            {
                _transaction = _dataContext.Database.BeginTransaction();
            }
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public int SaveChanges()
        {
            return _dataContext.SaveChanges();
        }
    }
}
