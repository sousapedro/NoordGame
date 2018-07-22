using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInScript : MonoBehaviour
{
    public Animator anim;

    private int levelToStart;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FadeToLevel(1);
        }
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToStart = levelIndex;
        anim.SetTrigger("fadeStart");
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToStart);
    }
}