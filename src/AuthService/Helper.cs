using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService
{
    internal class Helper
    {
        internal static T RunAsyncTask<T>(Task<T> task)
        {
            try
            {
                return task.Result;
            }
            catch (AggregateException ex)
            {
                Exception innerException = ex.InnerExceptions[0];
                if (innerException is AggregateException)
                    innerException = ((AggregateException)innerException).InnerExceptions[0];
                throw innerException;
            }
        }

        internal static string DecodeBase64String(string base64)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(base64));
        }
    }
}
