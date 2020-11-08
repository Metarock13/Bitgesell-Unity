using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
[System.Serializable]
public class LoginData 
{
    public string externalip;
    public string rpcport;
    public string rpcuser;
    public string rpcpassword;

    public string GetURL()
    {

        //http://btcuser:btcpass@35.228.98.142:8332
        return "http://"+ rpcuser + ":"+ rpcpassword + "@"+ externalip + ":"+ rpcport;
    }
}
