using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        Debug.Log("AudioManager.Instance.InitializeSFXSlider(GetComponent<Slider>());");
        AudioManager.Instance.InitializeSFXSlider(GetComponent<Slider>());
    }
}
