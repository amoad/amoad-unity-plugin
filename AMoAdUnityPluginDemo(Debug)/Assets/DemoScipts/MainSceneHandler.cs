using UnityEngine;
using System.Collections;

public class MainSceneHandler : MonoBehaviour {

    public void GoToDisplayScene(){
        Application.LoadLevel ("DisplayScene");
    }
    public void GoToInterstitialScene(){
        Application.LoadLevel ("InterstitialScene");
    }
    public void GoToNativeHtmlScene(){
        Application.LoadLevel ("NativeHtmlScene");
    }
}
