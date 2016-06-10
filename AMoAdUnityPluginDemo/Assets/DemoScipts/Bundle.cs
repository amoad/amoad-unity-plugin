using UnityEngine;
using System.Collections;

public class Bundle {
    private static Bundle bundle;
    private string sid;
    private string adtype;
    public static Bundle GetInstance() {
        if(bundle == null) {
            bundle = new Bundle();
        }
        return bundle;
    }

    public string GetSid(){
        return sid;
    }
    public string GetAdType(){
        return adtype;
    }
    public void SetSid(string sid){
        this.sid = sid;
    }
    public void SetAdType(string adtype){
        this.adtype = adtype;
    }
}
