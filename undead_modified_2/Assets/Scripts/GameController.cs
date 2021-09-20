using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public CharacterControl chcon;
    public GameObject pause;
    public GameObject pausePanel;
    public GameObject pauseButton;

    private float chilltime = 0;
    private bool isEscPressed = false;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Escape)) Debug.Log("Pressed");
       // Debug.Log(chcon.isPause);
        
        if (chcon.isPause == false && Input.GetKeyUp(KeyCode.Escape) && !isEscPressed /*&& chilltime <=0*/)
        {
            Time.timeScale = 0;
            pause.SetActive(true);
            pauseButton.SetActive(false);
            pausePanel.SetActive(true);

            chilltime = 10 * Time.deltaTime;
            isEscPressed = true;
            chcon.isPause = true;
        }
        if (chcon.isPause == true && Input.GetKeyUp(KeyCode.Escape) && !isEscPressed)
        {
            pause.SetActive(false);
            pausePanel.SetActive(false);
            pauseButton.SetActive(true);

            Time.timeScale = 1;

            chcon.isPause = false;
        }

        isEscPressed = false;

        if (chilltime >= 0) chilltime -= Time.deltaTime;
    }

    

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetPause()
    {

        chcon.isPause = true;
        Time.timeScale = 0;
    }

    public void UnsetPause()
    {
        chcon.isPause = false;
        Time.timeScale = 1;
    }

    public void SceneLoad(int index)
    {
        SceneManager.LoadScene(index);
    }
}
