using UnityEngine;
using System.Collections;

public class InterstitialSceneHandler : FormHandler, IExitHandler {
    private static string SID = "62056d310111552c000000000000000000000000000000000000000000000000";

    void Start(){
        SetSid (SID);
    }

    public void Register(){
        AMoAdUnityPlugin.RegisterInterstitialAd(GetSid());
        //縦画面の広告の背景画像を設定する
        //AMoAdUnityPlugin.SetInterstitialPortraitPanel (sid:GetSid(), imageName:"portrait.gif");
        //横画面の広告の背景画像を設定する
        //AMoAdUnityPlugin.SetInterstitialLandscapePanel (sid:GetSid(), imageName:"landscape.gif");
        //リンクボタン画像を設定する
        //AMoAdUnityPlugin.SetInterstitialLinkButton(sid:GetSid(), imageName:"normal_link.gif", highlighted:"pressed_link.gif");
        //閉じるボタン画像を設定する
        //AMoAdUnityPlugin.SetInterstitialCloseButton(sid:GetSid(), imageName:"normal_close.gif", highlighted:"pressed_close.gif");
    }
    public void Load(){
        //広告をロードする
        AMoAdUnityPlugin.LoadInterstitialAd (GetSid());
    }
    public void Show(){
        //広告がロードされているかを確認する
        if (AMoAdUnityPlugin.IsLoadedInterstitialAd (GetSid())) {
            //広告を表示する
            AMoAdUnityPlugin.ShowInterstitialAd (GetSid());
        }
    }
    public void Unregister(){
        AMoAdUnityPlugin.Unregister (GetSid());
        //AMoAdUnityPlugin.UnregisterAll ();
    }
    public void AutoReloadDisable(){
        //広告の自動リロードフラグをfalseに設定する
        AMoAdUnityPlugin.SetInterstitialAutoReload(GetSid(), false);
    }
    public void AutoReload(){
        //広告の自動リロードフラグをtrueに設定する
        AMoAdUnityPlugin.SetInterstitialAutoReload(GetSid(), true);
    }
    public void Exit(){
        Unregister ();
        Application.LoadLevel ("MainScene");
    }
}
