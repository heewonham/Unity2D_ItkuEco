using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    public GameObject[] Story = new GameObject[4];
    AudioSource Clickaudio;
    int page;

    void Awake()
    {
        Clickaudio = GetComponent<AudioSource>();
        page = 0;
        Story[page].SetActive(true);
    }
    public void OpeningButton(string type)
    {
        switch (type)
        {
            case "Next":
                Clickaudio.Play();
                Story[page].SetActive(false);
                page++;
                Story[page].SetActive(true);
                break;
            case "Prev":
                Clickaudio.Play();
                Story[page].SetActive(false);
                page--;
                Story[page].SetActive(true);
                break;
            case "End":
                Clickaudio.Play();
                Story[page].SetActive(false);
                SceneManager.LoadScene(2);
                break;
        }
    }
}