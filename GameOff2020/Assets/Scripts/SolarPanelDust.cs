using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarPanelDust : MonoBehaviour
{
    int solarPanelStage;
    [SerializeField] private Material stageOne;
    [SerializeField] private Material stageTwo;
    [SerializeField] private Material stageThree;
    [SerializeField] private Material stageFour;
    private Rigidbody player;
    private float useableRange = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        solarPanelStage = 1;
        InvokeRepeating(nameof(IncreaseSolarPanelDust), 5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        switch (solarPanelStage)
        {
            case 1:
                GetComponent<Renderer>().material = stageOne;
                break;
            case 2:
                GetComponent<Renderer>().material = stageTwo;
                break;
            case 3:
                GetComponent<Renderer>().material = stageThree;
                break;
            case 4:
                GetComponent<Renderer>().material = stageFour;
                break;
            default:
                break;
        }
    }
    void IncreaseSolarPanelDust()
    {
        if (solarPanelStage != 4)
        {
            solarPanelStage++;
        }
    }
    private void OnMouseDown()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        player = GameObject.Find("Player_Astronaut").GetComponent<Rigidbody>();
        if (Physics.Raycast(ray, out hit))
        {
                float dist = Vector3.Distance(player.transform.position, hit.point);
                if (dist < useableRange)
                {
                solarPanelStage = 1;
            }
        }
    }
}
