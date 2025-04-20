using UnityEngine;

public class ZickZackAnimation : MonoBehaviour
{
    private Animator anim;

    /// <summary>
    /// Start method.
    /// </summary>
    private void Start()
    {
        anim = GetComponent<Animator>(); 
        Play();
    }

    /// <summary>
    /// Plays the animation.
    /// </summary>
    private void Play()
    {
        anim.Play(0);
    }
}


