using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject OptionsMenuPrefab;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenNextScene()
    {
        // Current scene index
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // Load next scene
        SceneManager.LoadScene((currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void OpenOptionsMenu()
    {
        // Instantiate at the centre of the screen
        Vector3 centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Instantiate(OptionsMenuPrefab, centerOfScreen, Quaternion.identity);
    }

    public void CloseApplication()
    {
        // Close the application
        Application.Quit();
    }
}
