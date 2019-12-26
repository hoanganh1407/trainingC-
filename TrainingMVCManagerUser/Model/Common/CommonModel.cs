using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingMVCManagerUser.Common
{
    /// <summary>
    /// các phương thức common
    /// create by AnhNH3 date: 25/12/2019
    /// </summary>
    public static class CommonModel
    {
        /// <summary>
        /// kiểm tra rỗng or null
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="vali"></param>
        /// <returns></returns>
        public static bool CheckEmpty(string vali)
        {
            if(vali == null || vali.Equals(""))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// kiểm tra sort
        /// create by AnhNH3 date: 25/12/2019
        /// </summary>
        /// <param name="sort"></param>
        /// <returns></returns>
        public static string ValidateSort(string sort)
        {
            if(sort.Equals("ascending ") || sort.Equals("descending"))
            {
                return sort;
            }
            return "ascending ";
        }
    }
}