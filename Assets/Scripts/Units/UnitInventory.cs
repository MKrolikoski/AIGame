using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitInventory : MonoBehaviour
{
    public List<Item> items;
    public int size { get; private set; }

    private void Awake()
    {
        items = new List<Item>();
        size = 2;
    }

    public bool SpaceInInventory()
    {
        if (items.Count == size)
            return false;
        return true;
    }

    public bool AddItem(Item item)
    {
        if (!SpaceInInventory())
            return false;
        items.Add(item);
        return true;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public bool PotionInInventory(List<Type> types)
    {
        foreach (Item item in items)
        {
            if (types.Contains(item.GetType()))
                return true;
        }
        return false;
    }

    public bool HealthPotionInInventory()
    {
        List<Type> healthPotionTypes = new List<Type>
        {
            typeof(HealthPotion)
        };
        return PotionInInventory(healthPotionTypes);
    }

    public bool ManaPotionInInventory()
    {
        List<Type> manaPotionTypes = new List<Type>
        {
            typeof(ManaPotion)
        };
        return PotionInInventory(manaPotionTypes);
    }

    public bool OffensivePotionInInventory()
    {
        List<Type> offensivePotionTypes = new List<Type>
        {
            typeof(AttackPotion)
        };
        return PotionInInventory(offensivePotionTypes);
    }

    public bool DefensivePotionInInventory()
    {
        List<Type> defensivePotionTypes = new List<Type>
        {
            typeof(DefencePotion)
        };
        return PotionInInventory(defensivePotionTypes);
    }

    public void UseHealthPotion()
    {
        Item itemToUse = null;
        foreach (Item item in items)
        {
            if (item.GetType() == typeof(HealthPotion))
            {
                itemToUse = item;
                item.Use(GetComponent<Unit>());
                break;
            }
        }
        if (itemToUse != null)
            RemoveItem(itemToUse);
    }

    public void UseManaPotion()
    {
        Item itemToUse = null;
        foreach (Item item in items)
        {
            if (item.GetType() == typeof(ManaPotion))
            {
                itemToUse = item;
                item.Use(GetComponent<Unit>());
                break;
            }
        }
        if (itemToUse != null)
            RemoveItem(itemToUse);
    }

    public void UseDefensivePotion()
    {
        Item itemToUse = null;
        foreach (Item item in items)
        {
            if (item.GetType() == typeof(DefencePotion))
            {
                itemToUse = item;
                item.Use(GetComponent<Unit>());
                break;
            }
        }
        if (itemToUse != null)
            RemoveItem(itemToUse);
    }

    public void UseOffensivePotion()
    {
        Item itemToUse = null;
        foreach (Item item in items)
        {
            if (item.GetType() == typeof(AttackPotion))
            {
                itemToUse = item;
                item.Use(GetComponent<Unit>());
                break;
            }
        }
        if (itemToUse != null)
            RemoveItem(itemToUse);
    }

    //public void GetItem()
    //{
    //    AddItem(ItemSpawner.instance.SpawnItem());
    //}
}
