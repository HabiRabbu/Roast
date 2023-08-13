using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed to manage scenes

public class OptionsManager : MonoBehaviour
{

    [SerializeField] GameObject optionsPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void CloseApplication()
    {
        Application.Quit();
    }

    public void GoBackOneScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex > 0)
        {
            SceneManager.LoadScene(currentSceneIndex - 1);
        }
    }

    public void ToggleOptionsPanel()
    {
            optionsPanel.SetActive(!optionsPanel.activeSelf);
    }
}
