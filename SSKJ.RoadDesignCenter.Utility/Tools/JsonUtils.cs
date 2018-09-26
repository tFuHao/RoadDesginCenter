using Newtonsoft.Json;

namespace SSKJ.RoadDesignCenter.Utility.Tools
{
    public static class JsonUtils
    {
        /// <summary>
        /// 将T对象序列化成Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string ToJson<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }

        /// <summary>
        /// 将Json字符串反序列化成T对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
