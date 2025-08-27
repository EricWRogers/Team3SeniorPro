using UnityEngine;
using UnityEngine.UI;

public class StatCircleDisplay : MonoBehaviour
{
    public Image statMeter;
    [HideInInspector] public float curStat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        statMeter.fillAmount = curStat;
    }
}
