using Pm.V.PM_BLL;
using Pm.V.PM_Comom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pm.V
{
    public class Pm_Proxy
    {

        public BaseClass _DataType(int nType)
        {

            switch (nType)
            {
                case (int)EHelper.DataType.博客园:
                    return new CnblogsClass();

                case (int)EHelper.DataType.博客园NET技术:
                    return new HuJiangClass();
            }
            throw new Exception("未能找到数据来源");
        }
    }
}
