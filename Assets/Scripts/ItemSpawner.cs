using System;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public static ItemSpawner instance;

    [Serializable]
    public struct AvailableItems
    {
        public Item item;
        public int dropChance;
    }
    public AvailableItems[] availableItems;
    private int totalDropChance;
    [Range(0,1)]
    public float dropChancePerc = 0.25f;

    private void Awake()
    {
        if(instance != null)
        {
            return;
        }
        instance = this;
    }

    void Start ()
    {
        CountDropChance();
    }

    private void CountDropChance()
    {
        for (int i = 0; i < availableItems.Length; i++)
        {
            totalDropChance += availableItems[i].dropChance;
        }
    }
	
	void Update ()
    {
		
	}

    public Item SpawnItem(UnitStats stats = null)
    {
        Item item = null;
        int itemDropChance = Mathf.RoundToInt(UnityEngine.Random.value * totalDropChance);
        int noDropChance = totalDropChance - Mathf.RoundToInt(totalDropChance * dropChancePerc);
        if (itemDropChance <= noDropChance)
        {
            return null;
        }
        int sum = 0;
        for (int i = 0; i < availableItems.Length; i++)
        {
            sum += availableItems[i].dropChance;
            if (itemDropChance <= sum)
            {
                item = Instantiate(availableItems[i].item);
                if (item.GetType() == typeof(ManaPotion) && stats != null && stats.mp.baseValue <= 0)
                {
                    item = null;
                }
                break;
            }
        }
        return item;
    }
}
