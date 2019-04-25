using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TestJavaScript : MonoBehaviour
{

    private AndroidJavaObject javaClass;
    public Text textfield;

	void Start ()
    {
        javaClass = new AndroidJavaObject("com.example.xtt2library.AI");
    }
	
	void Update ()
    {
		
	}

    public void GetActionFromInference()
    {
        object[] parameters = new object[]
        {
            "CHOOSING_ACTION", 1, "essential", 1, false, false, false, false, true, false, 0.3d, 1.0d, 30, 5, 3, 5, "non_essential",  0.5d, 2, 2
        };
        string action = javaClass.Call<string>("StartInference", parameters);
        SetText(action);
    }

    public void UseJavaClass()
    {
        javaClass.Call("RunSetText");
    }

    public void SetText(string text)
    {
        textfield.text = text;
    }
}
