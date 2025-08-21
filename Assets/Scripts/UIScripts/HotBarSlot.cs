using UnityEngine;
using UnityEngine.UI;

public class HotBarSlot : MonoBehaviour
{
    public int iteamId;
    public bool isSelected;
    public Color selectedColor;
    public Color unselectedColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            gameObject.GetComponent<Image>().color = selectedColor;
        }
        else
        {
            gameObject.GetComponent<Image>().color = unselectedColor;
        }
    }
    public void ToggleSelected(bool _trueOrFalse)
    {
        isSelected = _trueOrFalse;
    }
}
