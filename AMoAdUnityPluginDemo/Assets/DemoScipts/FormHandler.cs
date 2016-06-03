using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FormHandler : MonoBehaviour {
    private const string k_FORM_PATH = "/Canvas/SidForm";

    public void SetSid(string sid){
        GameObject.Find (k_FORM_PATH).GetComponent<InputField>().text = sid;
    }

    public string GetSid() {
        return GameObject.Find (k_FORM_PATH).GetComponent<InputField>().text;
    }

    public void Clear(){
        GameObject.Find (k_FORM_PATH).GetComponent<InputField>().text = "";
    }
}
