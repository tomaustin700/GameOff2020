using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Printer : MonoBehaviour
{
    public GameObject CraftUI;
    public bool IsOpen = false;
    public GameObject CameraObject;
    // Start is called before the first frame update
    void Awake()
    {
        CraftUI = GameObject.FindGameObjectWithTag("CraftUI");
        CameraObject = Camera.main?.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(CraftUI == null)
        {
            CraftUI = GameObject.FindGameObjectWithTag("CraftUI");
        }
        if (CameraObject == null)
        {
            CameraObject = Camera.main?.gameObject;
        }
    }
    public void ToggleCrafting()
    {
        if(IsOpen)
        {
            CloseCrafting();
        }
        else
        {
            OpenCrafting();
        }
    }
    public void OpenCrafting()
    {
        foreach (Transform child in CraftUI.transform)
        {
            child.gameObject.SetActive(true);
        }
        CameraObject.GetComponent<CameraFollow>().CanAlterCursor = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsOpen = true;
    }

    public void CloseCrafting()
    {
        foreach (Transform child in CraftUI.transform)
        {
            child.gameObject.SetActive(false);
        }
        var cam = CameraObject.GetComponent<CameraFollow>();
        cam.CanAlterCursor = true;
        IsOpen = false;
    }
}
