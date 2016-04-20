using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pm.M.PM_Model
{

    /// <summary>
    /// 文章信息返回类型
    /// </summary>
    public class MoArticlesResponse : BaseResponse
    {

        public MoArticlesResponse()
        {

            MoArticles = new List<MoArticle>();
        }

        public List<MoArticle> MoArticles;

    }

    /// <summary>
    /// 文章实体类
    /// </summary>
    public class MoArticle
    {

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public string PublishTime { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Des { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public int DataType { get; set; }
    }
}
