using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCms.Controllers
{

    [Route("[controller]/[action]")]
    public class HomeController: MyCmsControllerBase
    {

        public HomeController()
        {

        }
        /// <summary>
        /// 测试返回结果集
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ContentResult GetTestResult()
        {
            string result = Request.QueryString.ToString();
            return Content( "你好啊世界,参数为："+ result);
        }
        
    }
}
