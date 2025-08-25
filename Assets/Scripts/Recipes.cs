using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipes", menuName = "Scriptable Objects/Recipes")]
public class Recipes : ScriptableObject
{
    public PseudoDictionary<ItemData, List<ItemData>> allRecipes;// Recipe outcome ID, ingredint ID
}
