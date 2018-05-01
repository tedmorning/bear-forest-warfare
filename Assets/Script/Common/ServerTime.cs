using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ServerTime : MonoBehaviour{

    private string url = "http://www.beijing-time.org/time.asp";
    private string time = string.Empty;

    public delegate void GetTimeBackCall(string time);
    private static GetTimeBackCall call;

    public void GetServerTime(GetTimeBackCall backCall) 
    {
        call = backCall;
        StartCoroutine("GetTime");

    }

    public IEnumerator GetTime()
    {
        //Debug.Log("开始请求服务器时间");

        while (true) 
        {
            WWW www = new WWW(url);
            yield return www;                   //在这里阻塞,等待响应之后返回
            if (www.isDone && string.IsNullOrEmpty(www.error) && www.text.Length >= 10)
            {
                SpliitString(www);
                break;
            }
            //Debug.Log("重新请求服务器时间");
        }

        yield return new WaitForFixedUpdate();
    }

    private void SpliitString(WWW www)
    {
        //使用正则表达式匹配
        string patten = @"[0-9]{1,};";
        Regex regex = new Regex(patten);
        MatchCollection result = regex.Matches(www.text);

        //组织时间
        time = string.Format("{0}-{1}-{2} {3}:{4}:{5}"
                        , result[0].Value.TrimEnd(';')
                        , result[1].Value.TrimEnd(';')
                        , result[2].Value.TrimEnd(';')
                        , result[4].Value.TrimEnd(';')
                        , result[5].Value.TrimEnd(';')
                        , result[6].Value.TrimEnd(';')
                        );


        if(time.Length >= 10)
        {
            call(time);
        }

        //Debug.Log("北京时间:" + time);
    }


}
