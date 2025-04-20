using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttentionMenu : MonoBehaviour
{
    /// <summary>
    /// Starts the MainMenuScene.
    /// </summary>
    public void OnOkButtonClick()
    {
        SceneManager.LoadScene(SceneType.MainMenuScene.ToString());
    }
}