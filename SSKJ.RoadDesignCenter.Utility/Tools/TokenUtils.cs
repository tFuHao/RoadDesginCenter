using Jose;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Utility.Tools
{
    public static class TokenUtils
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <param name="payload">加密对象</param>
        /// <param name="privateKey">密钥</param>
        /// <returns></returns>
        public static string ToToken<T>(T payload, string privateKey = "KMSSKJROADDESIGNCENTER")
        {
            return JWT.Encode(payload, privateKey, JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
        }

        /// <summary>
        /// 将字符串token反序列化成T对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="token"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static T ToObject<T>(string token, string privateKey = "KMSSKJROADDESIGNCENTER")
        {
            string jsonStr = JWT.Decode(token, privateKey, JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
            return JsonUtils.ToObject<T>(jsonStr);
        }
    }
}
