using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

public static  class JSON_RPC
{
    //public string Url;
    //public string rpcUsername;
    //public string rpcPassword;
    //public string method;
    //public string[] myparams;
    //getbalance
    //"getblockchaininfo"

    //public void StartInvoke()
    //{
    //    //object[] _params = { "bgl1qp0zjtapvpt0n0d03vxrlhg0qzrprlu9swrqq25", 0.1, "donation", "seans outpost" };
    //    object[] _params = { "*", 6 };
    //    JObject rt = InvokeMethod(method, _params);
    //    Debug.Log(rt);
    //}

    public static JObject InvokeMethod( string url, string rpcUsername, string rpcPassword,   string a_sMethod, params object[] a_params)
    {
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
        webRequest.Credentials = new NetworkCredential(rpcUsername, rpcPassword); ;

        webRequest.ContentType = "application/json-rpc";
        webRequest.Method = "POST";

        JObject joe = new JObject();
        joe["jsonrpc"] = "1.0";
        joe["id"] = "curltest";
        joe["method"] = a_sMethod;

        if (a_params != null)
        {
            if (a_params.Length > 0)
            {
                JArray props = new JArray();
                foreach (var p in a_params)
                {
                    props.Add(p);
                }
                joe.Add(new JProperty("params", props));
            }
        }
        Debug.Log(joe);

        string s = JsonConvert.SerializeObject(joe);
        // serialize json for the request
        byte[] byteArray = Encoding.UTF8.GetBytes(s);
        webRequest.ContentLength = byteArray.Length;

        try
        {
            using (Stream dataStream = webRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
        }
        catch (WebException we)
        {
            //inner exception is socket
            //{"A connection attempt failed because the connected party did not properly respond after a period of time, or established connection failed because connected host has failed to respond 23.23.246.5:8332"}
            Debug.Log(we.ToString());
        }
        WebResponse webResponse = null;
        try
        {
            using (webResponse = webRequest.GetResponse())
            {
                using (Stream str = webResponse.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        return JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                    }
                }
            }
        }
        catch (WebException webex)
        {

            using (Stream str = webex.Response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(str))
                {
                    var tempRet = JsonConvert.DeserializeObject<JObject>(sr.ReadToEnd());
                    return tempRet;
                }
            }

        }
        catch (Exception)
        {

            throw;
        }
    }
}
