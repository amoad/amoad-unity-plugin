using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class NativeHtmlSceneHandler : FormHandler, IExitHandler {
    private const string SID = "62056d310111552c000000000000000000000000000000000000000000000000";
    private const string TAG = "Ad01";

    void Start(){
        SetSid (SID);
    }

    public void Show(){
        AMoAdNativeUnityPlugin.Show(GetSid(), TAG);
    }
    public void Hide(){
        AMoAdNativeUnityPlugin.Hide(GetSid(), TAG);
    }
    public void Load(){
        if (GetSid() == null){
            return;
        }
        var rx = new Regex("[a-f0-9]{64}").Match(GetSid(), 0);
        if (!rx.Success) {
            return;
        }
        //CSSスタイルの使用
        AMoAdNativeUnityPlugin.Load(GetSid(), TAG, 100, 200, 140, 120, "{\"border\": \"dotted 2px #0000ff\"}");
        //CSSスタイルの未使用
        //AMoAdNativeUnityPlugin.Load(GetSid(), TAG, 100 /*dpi*/, 200/*dpi*/, 140/*dpi*/, 120/*dpi*/);
    }
    public void Reload(){
        AMoAdNativeUnityPlugin.Reload(GetSid(), TAG);
    }
    public void StartRotation(){
        AMoAdNativeUnityPlugin.StartRotation(GetSid(), TAG, 30 /*秒*/);
    }
    public void StopRotation(){
        AMoAdNativeUnityPlugin.StopRotation(GetSid(), TAG);
    }
    public void Exit(){
        StopRotation ();
        AMoAdNativeUnityPlugin.Remove(GetSid(), TAG);
        Application.LoadLevel ("MainScene");
    }
}
