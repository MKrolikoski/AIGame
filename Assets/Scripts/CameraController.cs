using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panZSpeed = 20f;
    public float panXSpeed = 30f;
    public float scrollSpeed = 10f;
    public float rotateSpeed = 100f;
    public Vector2 panLimit;
    public Vector2 scrollLimit;
	
	void Update ()
    {
        Vector3 pos = transform.position;
        Vector3 camF = transform.forward;
        Vector3 camR = transform.right;
        camF.y = 0f;
        camR.y = 0f;
        camF = camF.normalized;
        camR = camR.normalized;

        if(Input.GetKey("w"))
        {
            pos += (camF * panZSpeed) * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            pos -= (camF * panZSpeed) * Time.deltaTime;
        }
        if (Input.GetKey("d"))
        {
            pos += (camR * panXSpeed) * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            pos -= (camR * panXSpeed) * Time.deltaTime;
        }
        if (Input.GetKey("e"))
        {
            transform.RotateAround(transform.position, Vector3.up, rotateSpeed * -1 * Time.deltaTime);
        }
        if (Input.GetKey("q"))
        {
            transform.RotateAround(transform.position, Vector3.up, rotateSpeed * 1 * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y - 18f, panLimit.y - 18);
        pos.y = Mathf.Clamp(pos.y, scrollLimit.x, scrollLimit.y);

        transform.position = pos;
	}
}
