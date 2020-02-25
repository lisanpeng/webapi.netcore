using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Linq;

namespace Api.Core
{
    //public class Appsetting
    //{
    //    private static IConfigurationSection _appSection = null;

    //    public static string AppSettings(string key)
    //    {
    //        string str = string.Empty;

    //        if (_appSection.GetSection(key) != null)
    //        {
    //            str = _appSection.GetSection(key).Value;
    //        }

    //        return str;
    //    }

    //    public static void SetConfig(IConfigurationSection section)
    //    {
    //        _appSection = section;
    //    }

    //    public static string GetConfig(string key)
    //    {
    //        return AppSettings(key);
    //    }
    //}

    /// <summary>
    /// appsettings.json操作类
    /// </summary>
    public class Appsettings
    {
        static IConfiguration Configuration { get; set; }
        static string contentPath { get; set; }

        public Appsettings(string contentPath)
        {
            string Path = "appsettings.json";

            //如果你把配置文件 是 根据环境变量来分开了，可以这样写
            //Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

            Configuration = new ConfigurationBuilder()
               .SetBasePath(contentPath)
               .Add(new JsonConfigurationSource { Path = Path, Optional = false, ReloadOnChange = true })//这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
               .Build();

        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string app(params string[] sections)
        {
            try
            {
                if (sections.Any())
                {
                    return Configuration[string.Join(":", sections)];
                }
            }
            catch (Exception) { }

            return "";
        }
    }
}
