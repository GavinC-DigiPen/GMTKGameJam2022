//------------------------------------------------------------------------------
//
// File Name:	ButtonsUI.cs
// Author(s):	Gavin Cooper (gavin.cooper)
// Project:	    GMTK GameJam 2022
//
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsUI : MonoBehaviour
{
    [Tooltip("The sound of the button being pushed")] [SerializeField]
    private AudioClip buttonClickSound;
    [Tooltip("The delay on doing an action to let the sound play")] [SerializeField]
    private float timeDelay = 0.15f;
    [Tooltip("The name of the scene that will be loaded")] [SerializeField]
    private string[] sceneName;

    private AudioSource UIAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        UIAudioSource = GetComponent<AudioSource>();
    }

    public void ButtonSound()
    {
        StartCoroutine(_ButtonSound());
    }

    public void ChangeScene(int index)
    {
        StartCoroutine(_ChangeScene(index));
    }

    public void QuiteGame()
    {
        StartCoroutine(_QuiteGame());
    }

    IEnumerator _ButtonSound()
    {
        UIAudioSource.PlayOneShot(buttonClickSound);
        yield return new WaitForSeconds(timeDelay);
    }

    IEnumerator _ChangeScene(int index)
    {
        UIAudioSource.PlayOneShot(buttonClickSound);
        yield return new WaitForSeconds(timeDelay);
        SceneManager.LoadScene(sceneName[index]);
    }

    IEnumerator _QuiteGame()
    {
        UIAudioSource.PlayOneShot(buttonClickSound);
        yield return new WaitForSeconds(timeDelay);

        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
               
                            Application.Quit();
        #endif
    }

}
