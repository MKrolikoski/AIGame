using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumber : MonoBehaviour
{
    public Animator animator;
    private Transform cam;
    public Text damageText;
    private Transform location;

	void Start ()
    {
        cam = Camera.main.transform;
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
    }

    void LateUpdate()
    {
        transform.forward = cam.forward;
        if (location != null)
        {
            transform.position = location.position;
        }
    }

    public void setLocation(Transform location)
    {
        this.location = location;
    }

    public void SetText(string text)
    {
        damageText.text = text;
    }
}
