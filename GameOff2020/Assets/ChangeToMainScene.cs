using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ChangeToMainScene : MonoBehaviour
{
    private PlayableDirector director;
    public double Time;
    //   private AsyncOperation operation;
    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.Play();
    }
    private void Update()
    {
        Time = director.time;
        if (director.time >= 11.00)
        {
            SceneManager.LoadScene("MainScene",LoadSceneMode.Single);
        }
    }
   
}