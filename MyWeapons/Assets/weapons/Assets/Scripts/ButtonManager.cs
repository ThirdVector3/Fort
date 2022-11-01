using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
