using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Item item { get; private set; }
    public Unit unit { get; private set; }
    public Image icon;
    public Button itemButton;
    public Button removeButton;


    public void AddItem(Item item, Unit unit)
    {
        this.item = item;
        this.unit = unit;

        icon.sprite = item.icon;
        icon.enabled = true;
        itemButton.interactable = true;
        removeButton.interactable = true;
    }

    public void ClearItem()
    {
        item = null;
        unit = null;
        icon.sprite = null;
        icon.enabled = false;
        itemButton.interactable = false;
        removeButton.interactable = false;
    }

    public void UseItem()
    {
        item.Use(unit);
        unit.inventory.RemoveItem(item);
        ClearItem();
    }

    public void RemoveItem()
    {
        unit.inventory.RemoveItem(item);
        ClearItem();

    }

    public void DeactivateButton()
    {
        itemButton.interactable = false;
        removeButton.interactable = false;
    }
}
