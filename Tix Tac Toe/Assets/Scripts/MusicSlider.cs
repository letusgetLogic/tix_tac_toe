using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        Debug.Log("AudioManager.Instance.InitializeMusicSlider(GetComponent<Slider>());");
        AudioManager.Instance.InitializeMusicSlider(GetComponent<Slider>());
    }
}
