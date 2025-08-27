using UnityEngine;

public class MiningBeamVisual : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }
    public void ChangeScale(Vector3 _newScale, Vector3 _offsetPos)
    {
        transform.localScale = _newScale;
        transform.localPosition = _offsetPos;
    }
}
