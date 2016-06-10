using UnityEngine;
using System.Collections;

public class MainSceneHandler : MonoBehaviour {

    public void GoToFormSceneForDisplay(){
        Bundle.GetInstance().SetAdType("display");
        Application.LoadLevel ("FormScene");
    }
    public void GoToFormSceneForInterstitial(){
        Bundle.GetInstance().SetAdType("interstitial");
        Application.LoadLevel ("FormScene");
    }
    public void GoToFormSceneForNativeHtml(){
        Bundle.GetInstance().SetAdType("nativehtml");
        Application.LoadLevel ("FormScene");
    }
}
