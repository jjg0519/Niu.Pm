using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pm.M.PM_Model
{

    [Serializable]
    public class BaseRequest
    {
        /// <summary>
        /// 分配的账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Token验证（MD5（UserName + UserKey））
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 数据来源 0:全部汇总到一起 1：博客园信息  2.沪江网信息 3.....
        /// </summary>
        public int DataType { get; set; }

        /// <summary>
        /// 对应方法名称
        /// </summary>
        public string FunName { get; set; }
    }
}
