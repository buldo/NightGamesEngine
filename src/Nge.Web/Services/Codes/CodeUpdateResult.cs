using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nge.Web.Services.Codes
{
    public class CodeUpdateResult
    {
        private CodeUpdateResult(bool isSuccess, Error? error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public bool IsSuccess { get; }

        public Error? Error { get; }

        public static CodeUpdateResult CreateSuccess()
        {
            return new CodeUpdateResult(true, null);
        }

        public static CodeUpdateResult CreateExisted()
        {
            return new CodeUpdateResult(false, Codes.Error.CodeExists);
        }
    }
}
