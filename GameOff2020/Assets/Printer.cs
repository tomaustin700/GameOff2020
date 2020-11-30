using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

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
        var volume = GameObject.FindGameObjectWithTag("PostProcessVolume")?.GetComponent<Volume>();
        if (volume != null)
        {
            for (int i = 0; i < volume.profile.components.Count; i++)
            {
                if (volume.profile.components[i].name.Contains("Bloom"))
                {
                    Bloom bloom = (Bloom)volume.profile.components[i];
                    bloom.intensity.value = 1f;

                }
            }
        }
        foreach (Transform child in CraftUI.transform)
        {
            child.gameObject.SetActive(true);
        }
        CameraObject.GetComponent<CameraFollow>().CanAlterCursor = false;
        CameraObject.GetComponent<CameraFollow>().MoveToCursor = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsOpen = true;
    }

    public void CloseCrafting()
    {
        var volume = GameObject.FindGameObjectWithTag("PostProcessVolume")?.GetComponent<Volume>();
        if (volume != null)
        {
            for (int i = 0; i < volume.profile.components.Count; i++)
            {
                if (volume.profile.components[i].name.Contains("Bloom"))
                {
                    Bloom bloom = (Bloom)volume.profile.components[i];
                    bloom.intensity.value = 0.25f;

                }
            }
        }
        foreach (Transform child in CraftUI.transform)
        {
            child.gameObject.SetActive(false);
        }
        var cam = CameraObject.GetComponent<CameraFollow>();
        cam.PrepareCursor();
        IsOpen = false;
    }
}
