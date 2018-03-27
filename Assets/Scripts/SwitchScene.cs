using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{

    [SerializeField] private string Destination;
    [SerializeField] private AudioSource audioSour;

    public void LoadScene()
    {
        audioSour.Play();
        SceneManager.LoadScene(Destination);
    }

}
