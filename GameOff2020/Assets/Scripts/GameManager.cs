using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int daysUntilRescue = 11;
    public bool rescueTimerStarted;
    public GameObject rescueHud;

    private ItemSpawner itemSpawner;
    private Text rescueText;


    void Start()
    {
        SpawnItems();
        Physics.gravity = new Vector3(0, -3, 0);


    }

    private void Update()
    {
        if (!rescueTimerStarted)
        {
            var comms = GameObject.Find("CommunicationsDevice(Clone)");
            if (comms)
            {
                var rigid = comms.GetComponentInChildren<Rigidbody>();
                if (!rigid.isKinematic)
                    StartRescueTimer();

            }
        }

    }

    public void StartRescueTimer()
    {
        if (rescueHud != null)
        {
            rescueHud.SetActive(true);
            rescueText = GameObject.Find("daysValue").GetComponent<Text>();
            InvokeRepeating(nameof(UpdateDaysUntilRescue), 0, 600);
            rescueTimerStarted = true;
        }


    }

    void SpawnItems()
    {
        itemSpawner = GetComponent<ItemSpawner>();
        if (itemSpawner != null)
            itemSpawner.Spawn();
    }


    // Update is called once per frame
    void UpdateDaysUntilRescue()
    {
        if (daysUntilRescue > 0)
            daysUntilRescue--;

        rescueText.text = daysUntilRescue.ToString();
    }




}
