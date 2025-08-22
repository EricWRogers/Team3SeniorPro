using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public int id;
    public string displayName;
    [TextArea(4, 4)]
    public string description;
    public Sprite Image;
    public int maxStack = 20;
    public GameObject itemPrefab;
}
