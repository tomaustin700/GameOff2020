﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenu;
    private CameraFollow CameraObject;
    private void Awake()
    {
        CameraObject = Camera.main.GetComponent<CameraFollow>();
    }
    // Start is called before the first frame update
    private void Update()
    {
        if(CameraObject == null)
        {
            CameraObject = Camera.main.GetComponent<CameraFollow>();
        }
        if(MainMenu != null && Input.GetKeyUp(KeyCode.Escape))
        {
            if (MainMenu.activeInHierarchy)
            {
                CloseOpenMenus();
                MainMenu.SetActive(false);
                SetMenuEffects(false);
                Time.timeScale = 1;
            }
            else
            {
             
                CloseOpenMenus();
                MainMenu.SetActive(true);
                SetMenuEffects(true);
                Time.timeScale = 0;
            }
        }
    }
    private void CloseOpenMenus()
    {
        FindObjectsOfType<Storage>().Where(x => x.IsOpen).ToList().ForEach(x => x.CloseInventory());
        FindObjectsOfType<Printer>().Where(x => x.IsOpen).ToList().ForEach(x => x.CloseCrafting());
    }
    private void SetMenuEffects(bool isOpen)
    {
        var volume = GameObject.FindGameObjectWithTag("PostProcessVolume")?.GetComponent<Volume>();
        if (volume != null)
        {
            for (int i = 0; i < volume.profile.components.Count; i++)
            {
                if (volume.profile.components[i].name.Contains("Bloom"))
                {
                    Bloom bloom = (Bloom)volume.profile.components[i];
                    bloom.intensity.value = isOpen ? 1.0f : 0.25f;

                }
            }
        }

        if (isOpen)
        {
            CameraObject.CanAlterCursor = false;
            CameraObject.MoveToCursor = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            CameraObject.PrepareCursor();
        }
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1,LoadSceneMode.Single);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting game");
        if (Application.isEditor) {
           // UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void Resume()
    {
        Time.timeScale = 1;
        MainMenu.SetActive(false);
        SetMenuEffects(false);
    }
}
