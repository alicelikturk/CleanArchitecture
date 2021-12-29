using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Models
{
    public class ServiceResult<T> : ApplicationResult
    {
        public ServiceResult(bool succeeded, IEnumerable<string> errors)
            : base(succeeded, errors)
        {

        }
        public ServiceResult(bool succeeded, IEnumerable<string> errors, T value)
             : base(succeeded, errors)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
