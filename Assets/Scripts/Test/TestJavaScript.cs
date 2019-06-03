using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJavaScript : MonoBehaviour
{

    private AndroidJavaObject javaClass;

	void Start ()
    {
        javaClass = new AndroidJavaObject("Test.TestClass");
	}
	
	void Update ()
    {
		
	}

    public void UseJavaClass()
    {
        javaClass.Call("getNumber");
    }
}
