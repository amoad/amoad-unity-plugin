using UnityEngine;
using System.Collections;

public class DisplaySceneHandler : FormHandler, IExitHandler {
    private static string SID = "62056d310111552c000000000000000000000000000000000000000000000000";

    void Start(){
        SetSid (SID);
    }

    public void Register(){
        AMoAdUnityPlugin.Register (
            sid:GetSid(),
            bannerSize:AMoAdUnityPlugin.BannerSize.B320x50 /* 320dpi x 50dpi */,
            hAlign:AMoAdUnityPlugin.HorizontalAlign.Center,
            vAlign:AMoAdUnityPlugin.VerticalAlign.Bottom,
            adjustMode:AMoAdUnityPlugin.AdjustMode.Responsive,
            rotateTrans:AMoAdUnityPlugin.RotateTransition.None,
            clickTrans:AMoAdUnityPlugin.ClickTransition.None,
            imageName:"banner.gif"
        );
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
