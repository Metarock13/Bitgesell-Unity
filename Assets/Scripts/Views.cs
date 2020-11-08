using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TMPro;
using UnityEngine;

public class Views : MonoBehaviour
{
    [Header("Start")]
    public GameObject viewStart;
    [Header("Screen Login")]
    public GameObject screenLogin;
    [Header("Screen Wallet")]
    public GameObject screenWallet;
    [Header("Login Data")]
    public TMP_InputField externalip;
    public TMP_InputField rpcport;
    public TMP_InputField rpcuser;
    public TMP_InputField rpcpassword;
    public LoginData data;
    public string url;
    //[Header("JSON RPC")]
    //private JSON_RPC rPC;
    void ActivePanel(string panelToBeActivated)
    {
        viewStart.SetActive(panelToBeActivated.Equals(viewStart.name));
        screenLogin.SetActive(panelToBeActivated.Equals(screenLogin.name));
        screenWallet.SetActive(panelToBeActivated.Equals(screenWallet.name));
    }

    private void Start()
    {
        //ActivePanel(viewStart.name);
        //rPC=GetComponent<JSON_RPC>();
        GetAwake();
    }
    public void GetAwake()
    {
        if (LoadSaveData())
        {
            ActivePanel(screenWallet.name);
        }
        else
        {
            ActivePanel(viewStart.name);
        }
    }
    public void GetStart()
    {
        
        //if(LoadSaveData())
        //{
        //    ActivePanel(screenWallet.name);
        //}
        //else
        //{
            ActivePanel(screenLogin.name);
        //}
    }

    public void SignIn()
    {
        data.externalip = externalip.text;
        //string dff =  rpcport.text;
        //Encoding ANSI = Encoding.ASCII;
        //Encoding UTF8 = Encoding.UTF32;
        //byte[] win1251Bytes = Encoding.Convert(UTF8, ANSI, UTF8.GetBytes(dff));
        //string ansi_str = ANSI.GetString(win1251Bytes);
        //Debug.LogError(ansi_str);

        //int.TryParse(dff, out res);
        data.rpcport = rpcport.text;
        data.rpcuser = rpcuser.text;
        data.rpcpassword = rpcpassword.text;
        url = data.GetURL();

        string saveJson = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Save", saveJson);
        PlayerPrefs.Save();

    }

    bool LoadSaveData()
    {
        if(PlayerPrefs.HasKey("Save"))
        {
            string saveJson = PlayerPrefs.GetString("Save");
            data = JsonUtility.FromJson<LoginData>(saveJson);
            object[] _params = { "*", 6 };
            //Debug.Log(data);
            string uri = data.GetURL();
            Debug.Log(uri);
            //JSON_RPC rrr = new JSON_RPC();
            ////        public string Url;
            ////public string rpcUsername;
            ////public string rpcPassword;
            ////public string method;
            ////public string[] myparams;
            ////rrr.Url = data.externalip;
            //rrr.rpcUsername = data.rpcuser;
            //rrr.rpcPassword = data.rpcpassword;
            //rrr.method = "getbalance";

            JObject rt = JSON_RPC.InvokeMethod(uri, data.rpcuser, data.rpcpassword, "getbalance", _params);
            Debug.Log(rt);
            return true;
        }
        return false;
    }


}
