using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyCms.Wechat
{
    /// <summary>
    /// 微信测试
    /// </summary>
    public class WechatAppService :  MyCmsAppServiceBase, IWechatAppService
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public WechatAppService()
        {

        }

        /// <summary>
        /// 获取测试结果
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetTest()
        {
            string result = "你好啊，世界";
            
            return result;
        }
    }
}
