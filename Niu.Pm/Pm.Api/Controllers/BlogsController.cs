using Newtonsoft.Json;
using Pm.M.PM_Model;
using Pm.V;
using Pm.V.PM_Comom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using TaskPlugin;

namespace Pm.Api.Controllers
{
    public class BlogsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "欢迎使用-神牛步行3开源框架" };
        }

        // POST api/<controller>
        public async Task<HttpResponseMessage> Post()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var baseResponse = new BaseResponse();
            try
            {
                #region 验证

                //获取post数据
                HttpContent content = Request.Content;
                var param = await content.ReadAsStringAsync();
                var baseRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseRequest>(param);
                if (string.IsNullOrEmpty(baseRequest.FunName))
                {
                    baseResponse.ErrorMsg = EHelper.PmException.请求参数为空.ToString();
                    baseResponse.ErrorCode = (int)EHelper.PmException.请求参数为空;
                    response.Content = new StringContent(await JsonConvert.SerializeObjectAsync(baseResponse));
                    return response;
                }
                else if (string.IsNullOrEmpty(baseRequest.UserName))
                {

                    baseResponse.ErrorMsg = EHelper.PmException.账号不正确.ToString();
                    baseResponse.ErrorCode = (int)EHelper.PmException.账号不正确;
                    response.Content = new StringContent(await JsonConvert.SerializeObjectAsync(baseResponse));
                    return response;
                }
                else if (!Enum.IsDefined(typeof(EHelper.DataType), baseRequest.DataType))
                {
                    baseResponse.ErrorMsg = EHelper.PmException.参数不合法.ToString();
                    baseResponse.ErrorCode = (int)EHelper.PmException.参数不合法;
                    response.Content = new StringContent(await JsonConvert.SerializeObjectAsync(baseResponse));
                    return response;
                }
                //验证账号及token

                #endregion

                #region 业务

                var dataTypes = Enum.GetValues(typeof(EHelper.DataType));
                switch (baseRequest.FunName)
                {
                    //获取文章集合信息
                    case "_GetArticles":

                        //json反序列获取数据
                        var r_GetArticles = Newtonsoft.Json.JsonConvert.DeserializeObject<MoArticlesRequest>(param);

                        //初始化任务量
                        var tasks = new Task<MoArticlesResponse>[baseRequest.DataType == 0 ? dataTypes.Length : 1];
                        var proxy = new Pm_Proxy();
                        var j = 0;  //真实任务坐标
                        for (int i = 0; i < dataTypes.Length; i++)
                        {
                            var item = dataTypes.GetValue(i);
                            var nType = Convert.ToInt32(item);
                            if (nType != baseRequest.DataType && 0 != baseRequest.DataType) { continue; }

                            //使用任务做并行
                            var dataType = proxy._DataType(nType);
                            var task = Task.Factory.StartNew<MoArticlesResponse>(dataType._GetArticles, r_GetArticles);
                            tasks[j] = task;
                            j++;
                        }
                        //30s等待
                        Task.WaitAll(tasks, 1000 * 1 * 30);

                        //获取任务执行的结果（整合数据）
                        var articles = new MoArticlesResponse();
                        foreach (var task in tasks)
                        {
                            if (!task.IsCompleted) { continue; }
                            articles.MoArticles.AddRange(task.Result.MoArticles);
                        }

                        articles.IsSuccess = articles.MoArticles.Count > 0;
                        baseResponse = articles;
                        break;

                    default:
                        break;
                }
                response.Content = new StringContent(await JsonConvert.SerializeObjectAsync(baseResponse));
                #endregion
            }
            catch (Exception ex)
            {
                baseResponse.ErrorMsg = EHelper.PmException.获取数据异常.ToString();
                baseResponse.ErrorCode = (int)EHelper.PmException.获取数据异常;
            }
            return response;
        }
    }
}