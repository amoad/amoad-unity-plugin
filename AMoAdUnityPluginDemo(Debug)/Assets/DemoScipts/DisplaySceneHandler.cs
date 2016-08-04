using UnityEngine;
using System.Collections;

public class DisplaySceneHandler : FormHandler, IExitHandler {
    private static string SID = "62056d310111552c000000000000000000000000000000000000000000000000";

    void Start(){
        SetSid (SID);
    }

    public void Register(){
        #if UNITY_IOS
        AMoAdUnityPlugin.Register (
            sid:GetSid(),
            hAlign:AMoAdUnityPlugin.HorizontalAlign.Center,
            vAlign:AMoAdUnityPlugin.VerticalAlign.Bottom,
            adjustMode:AMoAdUnityPlugin.AdjustMode.Responsive,
            rotateTrans:AMoAdUnityPlugin.RotateTransition.None,
            clickTrans:AMoAdUnityPlugin.ClickTransition.None,
            imageName:"banner@2x.png"
        );
        #elif UNITY_ANDROID
        AMoAdUnityPlugin.Register (
            sid:GetSid(),
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
        AMoAdUnityPlugin.Unregister (GetSid());
        //AMoAdUnityPlugin.UnregisterAll ();
    }
    public void Show(){
        //広告を表示する
        AMoAdUnityPlugin.Show (GetSid());
    }
    public void Hide(){
        //広告を非表示にする
        AMoAdUnityPlugin.Hide (GetSid());
    }
    public void Exit(){
        Unregister ();
        Application.LoadLevel ("MainScene");
    }
}
