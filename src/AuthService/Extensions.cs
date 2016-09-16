using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthService
{
    public static class Extensions
    {
        public static T RunAsyncTask<T>(this Task<T> task)
        {
            return Helper.RunAsyncTask(task);
        }
    }
}
