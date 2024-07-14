using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentSystem.Service.Models.Shared
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }
    }

    public class ResultList<T> : Result
    {
        public List<T> Data { get; set; }
    }
}
