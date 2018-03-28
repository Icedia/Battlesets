using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    //stores destination and audio file
    [SerializeField] private string Destination;
    [SerializeField] private AudioSource audioSource;
    /// <summary>
    /// loads scene and plays audio file
    /// </summary>
    public void LoadScene()
    {
        audioSource.Play();
        SceneManager.LoadScene(Destination);
    }

}
