using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class FormSceneHandler : MonoBehaviour {
    private const string k_FORM_PATH = "/Canvas/SidForm";
    private const string k_FORM_ERROR_PATH = "/Canvas/SidFormError";

    public void SetSid(string sid){
        GameObject.Find (k_FORM_PATH).GetComponent<InputField>().text = sid;
    }

    public string GetSid() {
        return GameObject.Find (k_FORM_PATH).GetComponent<InputField>().text;
    }

    public void Clear(){
        SetErrorMsg("");
        GameObject.Find (k_FORM_PATH).GetComponent<InputField>().text = "";
    }

    public void SetErrorMsg(string msg) {
        GameObject.Find (k_FORM_ERROR_PATH).GetComponent<Text>().text = msg;
    }

    public void Next(){
        SetErrorMsg("");
        string sid = GetSid();
        if (sid != null && new Regex("[a-f0-9]{64}").Match(sid, 0).Success){
            Bundle.GetInstance().SetSid(sid);
            string type = Bundle.GetInstance().GetAdType();
            if("display".Equals(type)){
                Application.LoadLevel ("DisplayScene");
            }else if("interstitial".Equals(type)){
                Application.LoadLevel ("InterstitialScene");
            }else if("nativehtml".Equals(type)){
                Application.LoadLevel ("NativeHtmlScene");
            }
        } else {
            SetErrorMsg("sid不正です。");
        }
    }
    public void Prev(){
        SetErrorMsg("");
        SetSid("");
        Application.LoadLevel ("MainScene");
    }
}
