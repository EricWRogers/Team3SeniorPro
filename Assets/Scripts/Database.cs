using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Database", menuName = "Scriptable Objects/Database")]
public class Database : ScriptableObject
{
    [SerializeField] private List<ItemData> m_itemDatabase;

    [ContextMenu(itemName: "Set IDs")]
    public void SetItemIDs()
    {
        m_itemDatabase = new List<ItemData>();

        var foundItems = Resources.LoadAll<ItemData>(path: "ItemDict").OrderBy(i => i.id).ToList();

        var hasIDInRange = foundItems.Where(i => i.id != -1 && i.id < foundItems.Count).OrderBy(i => i.id).ToList();
        var hasIDNotInRange = foundItems.Where(i => i.id != -1 && i.id >= foundItems.Count).OrderBy(i => i.id).ToList();
        var noID = foundItems.Where(i => i.id <= -1).ToList();

        var index = 0;
        for (int x = 0; x < foundItems.Count; x++)
        {
            ItemData itemToAdd;

            itemToAdd = hasIDInRange.Find(i => i.id == x);

            if (itemToAdd != null)
            {
                m_itemDatabase.Add(itemToAdd);
            }
            else if (index < noID.Count)
            {
                noID[index].id = x;
                itemToAdd = noID[index];
                index++;
                m_itemDatabase.Add(itemToAdd);
            }
        }

        foreach (var item in hasIDNotInRange)
        {
            m_itemDatabase.Add(item);
        }
    }

    public ItemData GetItem(int _id)
    {
        return m_itemDatabase.Find(i => i.id == _id);
    }
}
