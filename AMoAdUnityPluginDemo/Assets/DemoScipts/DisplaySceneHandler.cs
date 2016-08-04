﻿using UnityEngine;
using System.Collections;

public class DisplaySceneHandler : MonoBehaviour {
    private string sid;
    void Start() {
        sid = Bundle.GetInstance().GetSid();
        Register();
        Show();
    }

    public void Register(){
        #if UNITY_IOS
        AMoAdUnityPlugin.Register (
            sid:sid,
            hAlign:AMoAdUnityPlugin.HorizontalAlign.Center,
            vAlign:AMoAdUnityPlugin.VerticalAlign.Bottom,
            adjustMode:AMoAdUnityPlugin.AdjustMode.Responsive,
            rotateTrans:AMoAdUnityPlugin.RotateTransition.None,
            clickTrans:AMoAdUnityPlugin.ClickTransition.None,
            imageName:"banner@2x.png"
        );
        #elif UNITY_ANDROID
        AMoAdUnityPlugin.Register (
            sid:sid,
            hAlign:AMoAdUnityPlugin.HorizontalAlign.Center,
            vAlign:AMoAdUnityPlugin.VerticalAlign.Bottom,
            adjustMode:AMoAdUnityPlugin.AdjustMode.Responsive,
            androidRotateTrans:AMoAdUnityPlugin.AndroidRotateTransition.None,
            androidClickTrans:AMoAdUnityPlugin.AndroidClickTransition.None,
            imageName:"banner.png"
        );
        #endif
    }
    public void Unregister(){
        AMoAdUnityPlugin.Unregister (sid);
        //AMoAdUnityPlugin.UnregisterAll ();
    }
    public void Show(){
        //広告を表示する
        AMoAdUnityPlugin.Show (sid);
    }
    public void Hide(){
        //広告を非表示にする
        AMoAdUnityPlugin.Hide (sid);
    }
    public void Exit(){
        Unregister ();
        Application.LoadLevel ("FormScene");
    }
}
