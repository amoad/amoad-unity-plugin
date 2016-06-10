using UnityEngine;
using System.Collections;

public class NativeHtmlSceneHandler : MonoBehaviour {
    private const string k_TAG = "AD01";
    private string sid;
    void Start() {
        sid = Bundle.GetInstance().GetSid();
        Load();
        Show();
    }

    public void Show(){
        AMoAdNativeUnityPlugin.Show(sid, k_TAG);
    }
    public void Hide(){
        AMoAdNativeUnityPlugin.Hide(sid, k_TAG);
    }
    public void Load(){
        //CSSスタイルの使用
        AMoAdNativeUnityPlugin.Load(sid, k_TAG, 100, 200, 140, 120, "{\"border\": \"dotted 2px #0000ff\"}");
        //CSSスタイルの未使用
        //AMoAdNativeUnityPlugin.Load(sid, k_TAG, 100 /*dpi*/, 200/*dpi*/, 140/*dpi*/, 120/*dpi*/);
    }
    public void Reload(){
        AMoAdNativeUnityPlugin.Reload(sid, k_TAG);
    }
    public void StartRotation(){
        AMoAdNativeUnityPlugin.StartRotation(sid, k_TAG, 30 /*秒*/);
    }
    public void StopRotation(){
        AMoAdNativeUnityPlugin.StopRotation(sid, k_TAG);
    }
    public void Exit(){
        StopRotation ();
        AMoAdNativeUnityPlugin.Remove(sid, k_TAG);
        Application.LoadLevel ("FormScene");
    }
}
