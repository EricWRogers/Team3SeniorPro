using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseGame : MonoBehaviour
{
    public bool gamePause;
    public GameObject PauseHUD;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gamePause = false;
        Time.timeScale = 1;
        PauseHUD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();

        }
    }

    public void TogglePause()
    {
        if (!gamePause)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().canMove = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            gamePause = true;
            PauseHUD.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().canMove = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            gamePause = false;
            Time.timeScale = 1;
            PauseHUD.SetActive(false);
        }
    }

    public void Quit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif   
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
