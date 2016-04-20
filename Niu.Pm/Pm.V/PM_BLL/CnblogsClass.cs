using Pm.V.PM_Comom;
using Pm.M.PM_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaskPlugin;

namespace Pm.V.PM_BLL
{

    /// <summary>
    /// 博客园信息（如果涉及到信息来源问题，请及时联系开源作者，谢谢）
    /// </summary>
    public class CnblogsClass : BaseClass
    {
        /// <summary>
        /// 初始化xml配置
        /// </summary>
        public CnblogsClass() : base("") { }

        #region  获取文章信息 _GetArticles  +BaseResponse

        /// <summary>
        /// 获取文章信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override MoArticlesResponse _GetArticles(object obj)
        {
            #region 初始化信息

            var request = new MoArticlesRequest();
            var response = new MoArticlesResponse();
            var sbLog = new StringBuilder(string.Empty);
            var url = this.Config.Url;   //这里获取配置文件的url
            #endregion

            try
            {

                #region 接口验证数据

                request = obj as MoArticlesRequest;
                if (request == null)
                {

                    response.ErrorCode = (int)EHelper.PmException.获取数据为空;
                    response.ErrorMsg = EHelper.PmException.获取数据为空.ToString();
                    return response;
                }

                #endregion

                #region 请求数据


                sbLog.AppendFormat("请求地址：{0}\n", url);
                var result = PublicClass._HttpGet(url);   //这里一般都是post数据到第三方接口，测试没有第三方可以使用，所以使用抓取博客园首页数据
                sbLog.AppendFormat("返回信息：{0}\n", result);
                #endregion

                #region 解析

                //使用正则解析数据
                var rgs = Regex.Matches(result, "class=\"titlelnk\"\\s+href=\"(?<link>http://www(\\w|\\.|\\/)+\\.html)\"[^>]+>(?<title>[^<]+)<\\/a>[^D]+a>(?<des>[^<]+)[^D]+lightblue\">(?<author>\\w+)<\\/a>[^D]+发布于(?<publishtime>\\s+(\\d|-|\\s|:)+)[^<]+");

                if (rgs.Count <= 0)
                {

                    response.ErrorCode = (int)EHelper.PmException.获取数据为空;
                    response.ErrorMsg = EHelper.PmException.获取数据为空.ToString();
                    return response;
                }

                foreach (Match item in rgs)
                {

                    var article = new MoArticle();

                    article.Author = item.Groups["author"].Value;
                    article.LinkUrl = item.Groups["link"].Value;
                    article.Title = item.Groups["title"].Value;
                    article.PublishTime = item.Groups["publishtime"].Value;
                  //  article.Des = item.Groups["des"].Value;
                    article.DataType = (int)EHelper.DataType.博客园;

                    if (response.MoArticles.Count > 5) { continue; }
                    response.MoArticles.Add(article);
                }

                response.IsSuccess = true;
                #endregion
            }
            catch (Exception ex)
            {

                sbLog.AppendFormat("异常信息：{0}\n", ex.Message);
                response.ErrorCode = (int)EHelper.PmException.获取数据异常;
                response.ErrorMsg = EHelper.PmException.获取数据异常.ToString();
            }
            finally
            {

                #region 第三方原始信息-日志

                //PublicClass._WriteLog(sbLog.ToString(), "Cnblogs");

                #endregion
            }
            return response;
        }
        #endregion
    }
}
