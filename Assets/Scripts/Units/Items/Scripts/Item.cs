
using UnityEngine;

public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon;



    public virtual void Use(Unit unit)
    {
        
    }
}
