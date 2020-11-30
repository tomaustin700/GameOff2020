using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        daysUntilRescue = 11;
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
                {
                    StartRescueTimer();
                    NotificationManager.AddNotification(new NotificationEvent(EventName.FirstMissionControlMessage, "New Message - Press M to view"));
                    MessageManager.AddMessage(new Message()
                    {
                        Title = "Mission Control - You're alive?",
                        Body = "Wow you're alive?! We were worried then for a few minutes, that was quite the crash! We're working on sending a rescue mission but it's looking like it'll be about 10 days until we can get to you. Hang tight until then, we'll be in touch with more instructions.",
                        EventName = EventName.FirstMissionControlMessage
                    });


                }

            }
        }

    }

    public void StartDropSpawn()
    {
        StartCoroutine(SpawnDrop(60));
    }

    IEnumerator SpawnDrop(float time)
    {
        yield return new WaitForSeconds(time);

        var asset = Resources.Load("Prefabs/SupplyDrop") as GameObject;

        Instantiate(asset, new Vector3(108f, -13f, 300f), Quaternion.identity);
        NotificationManager.AddNotification(new NotificationEvent(EventName.FifthMissionControlMessage, "New Message - Press M to view"));
        MessageManager.AddMessage(new Message()
        {
            Title = "Mission Control - Supply Drop",
            Body = "A supply drop is arriving soon containing some rations. It should be on the edge of the crater somewhere. It might be a long trip so take oxygen.",
            EventName = EventName.OpenDrop
        });

    }

    public void StartRescueTimer()
    {
        if (rescueHud != null)
        {
            rescueHud.SetActive(true);
            rescueText = GameObject.Find("daysValue").GetComponent<Text>();
            InvokeRepeating(nameof(UpdateDaysUntilRescue), 0, 200);
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
        {
            daysUntilRescue--;

            if (NotificationManager.Notifications.Any(a => a.EventName == EventName.Explore) && daysUntilRescue == 9)
            {
                NotificationManager.CompleteNotification(EventName.Explore,false);
                NotificationManager.AddNotification(new NotificationEvent(EventName.SecondMissionControlMessage, "New Message - Press M to view"));
                MessageManager.AddMessage(new Message()
                {
                    Title = "Mission Control - Rescue Plan Stage 1",
                    Body = "Okay we have a plan. We need you to craft and place a drill to start mining resources. You may need to clean your solar panel to get enough electricity to drill. Also be careful of your oxygen, it'll refill in the hab but outside it will go down. Await further instructions.",
                    EventName = EventName.SecondMissionControlMessage
                });

            }

            if (daysUntilRescue == 1)
            {
                NotificationManager.RemoveAllNotification();
                NotificationManager.AddNotification(new NotificationEvent(EventName.FinalMissionControlMessage, "New Message - Press M to view"));
                MessageManager.AddMessage(new Message()
                {
                    Title = "Mission Control - Last Message",
                    Body = "Rescue mission is nearly with you. Just a little bit longer to wait. See you soon!",
                    EventName = EventName.FinalMissionControlMessage
                });
            }


        }
        else
            SceneManager.LoadScene(4);

        rescueText.text = daysUntilRescue.ToString();
    }




}
