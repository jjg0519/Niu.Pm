using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pm.V.PM_Comom
{

    /// <summary>
    /// 枚举管理类
    /// </summary>
    public class EHelper
    {
        /// <summary>
        /// 数据来源（建议声明时候使用1，2指定数字赋值，避免造成之前哪个博主写的一篇博客那种情况）
        /// </summary>
        public enum DataType
        {

            博客园 = 1,
            博客园NET技术 = 2

        }

        /// <summary>
        /// 对外暴露的异常信息
        /// </summary>
        public enum PmException
        {

            请求参数为空 = 101,
            参数不合法 = 102,
            获取数据为空 = 103,
            获取数据异常 = 104,
            账号不正确 = 105,

            Token验证失败 = 106
        }
    }
}
