using Pm.M.PM_Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Pm.V.PM_BLL
{
    public abstract class BaseClass
    {

        #region 初始化xml配置信息 BaseClass

        /// <summary>
        /// 初始化xml配置信息
        /// </summary>
        /// <param name="xmlConf"></param>
        public BaseClass(string xmlConfigPath)
        {

            try
            {

                if (string.IsNullOrEmpty(xmlConfigPath))
                {

                    //默认各个Xml配置
                    var defaultConfigFolder = "PluginXml";
                    var baseAddr = AppDomain.CurrentDomain.BaseDirectory;
                    xmlConfigPath = Path.Combine(baseAddr, defaultConfigFolder, this.GetType().Name + ".xml");
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(xmlConfigPath);

                Config = new BaseConfig();
                Config.Url = doc.SelectSingleNode("//Pm/Url") == null ? "" : doc.SelectSingleNode("//Pm/Url").InnerXml;
                Config.UserName = doc.SelectSingleNode("//Pm/UserName") == null ? "" : doc.SelectSingleNode("//Pm/UserName").InnerXml;
                Config.UserKey = doc.SelectSingleNode("//Pm/UserKey") == null ? "" : doc.SelectSingleNode("//Pm/UserKey").InnerXml;

                Config.Doc = doc;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        /// <summary>
        /// xml配置信息
        /// </summary>
        public BaseConfig Config;
        #endregion

        #region  获取文章信息 _GetArticles  +BaseResponse

        /// <summary>
        /// 获取文章信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual MoArticlesResponse _GetArticles(object request)
        {
            return null;
        }
        #endregion

    }

    /// <summary>
    /// xml配置文件信息
    /// </summary>
    public class BaseConfig
    {

        /// <summary>
        /// 接口地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码|秘钥
        /// </summary>
        public string UserKey { get; set; }

        /// <summary>
        /// xml文件全部信息
        /// </summary>
        public XmlDocument Doc { get; set; }

    }
}
