using UnityEngine;
using System.Collections;

public class InterstitialSceneHandler : MonoBehaviour {
    private string sid;
    void Start() {
        sid = Bundle.GetInstance().GetSid();
        Register();
        Load ();
    }

    public void Register(){
        AMoAdUnityPlugin.RegisterInterstitialAd(sid);
        //縦画面の広告の背景画像を設定する
        //AMoAdUnityPlugin.SetInterstitialPortraitPanel (sid:sid, imageName:"portrait.gif");
        //横画面の広告の背景画像を設定する
        //AMoAdUnityPlugin.SetInterstitialLandscapePanel (sid:sid, imageName:"landscape.gif");
        //リンクボタン画像を設定する
        //AMoAdUnityPlugin.SetInterstitialLinkButton(sid:sid, imageName:"normal_link.gif", highlighted:"pressed_link.gif");
        //閉じるボタン画像を設定する
        //AMoAdUnityPlugin.SetInterstitialCloseButton(sid:sid, imageName:"normal_close.gif", highlighted:"pressed_close.gif");
    }
    public void Load(){
        //広告をロードする
        AMoAdUnityPlugin.LoadInterstitialAd (sid);
    }
    public void Show(){
        //広告がロードされているかを確認する
        if (AMoAdUnityPlugin.IsLoadedInterstitialAd (sid)) {
            //広告を表示する
            AMoAdUnityPlugin.ShowInterstitialAd (sid);
        }
    }
    public void Unregister(){
        AMoAdUnityPlugin.Unregister (sid);
        //AMoAdUnityPlugin.UnregisterAll ();
    }
    public void Exit(){
        Unregister ();
        Application.LoadLevel ("FormScene");
    }
}
