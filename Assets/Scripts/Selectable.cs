using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool canBeSelected = true;

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    public virtual void OnSelect() { }

    public virtual void OnDeselect() { }
}
