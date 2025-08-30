using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public int id = -1;
    public string displayName;
    [TextArea(4, 4)]
    public string description;
    public Sprite image;
    public int maxStack = 20;
    public GameObject itemPrefab;

    public enum ItemUseType { NoUse, Placeable, Throwable, Consumable} ;

    public ItemUseType itemUseType;
}
