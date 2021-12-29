using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException()
           : base()
        {
        }
        //public ForbiddenAccessException(string message) 
        //    : base(message)
        //{
        //}
        //public ForbiddenAccessException(string message, Exception innerException)
        //    : base(message, innerException)
        //{
        //}
        //public ForbiddenAccessException(string requestType,string authRequirement)
        //   : base($"İşlem ({requestType}) için yetkiniz \"{authRequirement}\" bulunmamaktadır.")
        //{
        //}
    }
}
