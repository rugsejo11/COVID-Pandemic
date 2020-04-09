using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void ButtonStart()
    {
        SceneManager.LoadScene(1);
    }
    //public void ButtonHighscores()
    //{
    //    SceneManager.LoadScene(2);
    //}
    //public void ButtonDemo()
    //{
    //    SceneManager.LoadScene(3);
    //}
    public void ButtonQuit()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

}
