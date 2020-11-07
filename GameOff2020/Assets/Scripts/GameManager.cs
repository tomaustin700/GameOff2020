using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int daysSurvived = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateDaysSurvived", 0, 600);

    }

    // Update is called once per frame
    void UpdateDaysSurvived()
    {
        daysSurvived++;
    }

    


}
