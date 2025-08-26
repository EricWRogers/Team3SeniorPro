using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "Scriptable Objects/Recipes")]
public class Recipes : ScriptableObject
{
    public ItemData outcome;
    public List<ItemData> recipe;
    public int amountOfOutcome;
}
