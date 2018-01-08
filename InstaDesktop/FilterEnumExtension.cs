using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaDesktop
{
    public  static class FilterEnumExtension
    {
        public static string ToString(this FilterEnum filterEnum)
        {
            switch (filterEnum)
            {
                case FilterEnum.Nashville:
                    return "nashville";
                case FilterEnum.Clarendon:
                    return "clarendon";
                case FilterEnum.Moon:
                    return "moon";
                case FilterEnum.Toaster:
                    return "toaster";
                case FilterEnum.XPro2:
                    return "xpro2";
                default:
                    throw new ArgumentOutOfRangeException(nameof(filterEnum), filterEnum, null);
            }
        }
    }
}
