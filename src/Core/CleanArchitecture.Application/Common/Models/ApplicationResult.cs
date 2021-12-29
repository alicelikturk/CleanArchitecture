using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Models
{
    public class ApplicationResult
    {
        internal ApplicationResult(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }

        public static ApplicationResult Success()
        {
            return new ApplicationResult(true, new string[] { });
        }

        public static ApplicationResult Failure(IEnumerable<string> errors)
        {
            return new ApplicationResult(false, errors);
        }

    }
}
