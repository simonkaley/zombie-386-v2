using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gotoMap1()
    {
        SceneManager.LoadScene(1);
    }
    public void gotoMap2()
    {
        SceneManager.LoadScene(2);
    }
    public void gotoGameover()
    {
        SceneManager.LoadScene(3);
    }
    public void gotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void gotoSettings()
    {
        SceneManager.LoadScene(4);
    }
    public void quitGame()
    {
        Application.Quit();
    }
}
