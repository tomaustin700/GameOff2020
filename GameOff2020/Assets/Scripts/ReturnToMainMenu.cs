using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void GoToMainMenu()
    {
        GameObject player = FindObjectOfType<PlayerManager>().gameObject;
        if (player != null)
            Destroy(player);
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
