using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ResponseFeatures
{
    public class BaseResponse<T, TErrors>
    {

        public T data { get; set; }

        public TErrors Errors { get; set; }

        public Boolean result { get; set; }

        public string errorMessage { get; set; }

        public string successMessage { get; set; }

        public int StatusCode { get; set; }


    }
}
