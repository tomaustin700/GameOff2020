using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreenCam : MonoBehaviour
{
    [SerializeField] private GameObject MainMenuButton;
    GameObject Credits;
    private void Start()
    {
        Credits = GameObject.Find("Credits");
    }
    void Update()
    {
        InvokeRepeating(nameof(RiseCam), 3, 0);
        if (!MainMenuButton.activeSelf && transform.position.y > 55)
            MainMenuButton.SetActive(true);
        InvokeRepeating(nameof(RollCredits), 5, 0);
    }
    void RiseCam()
    {
        if (transform.position.y < 56)
        {
            Vector3 newPos = transform.position;
            newPos.y += 0.002f;
            transform.position = newPos;
        }
    }
    void RollCredits()
    {
        if (Credits.transform.position.y < 2080)
        {
            Vector3 creditsPos = Credits.transform.position;
            creditsPos.y += 0.2f;
            Credits.transform.position = creditsPos;
            Debug.Log("Credits Y: " + Credits.transform.position.y.ToString());
        }
        if (!MainMenuButton.activeSelf && Credits.transform.position.y >= 2080)
            MainMenuButton.SetActive(true);
    }
}
