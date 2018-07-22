using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgressBar : MonoBehaviour {

    public GameObject[] lifesObjs;
    public GameObject[] lifesLostObjs;

    public RectTransform progressBar;

	private int currentLife = 0;

	private int MaxSteps = 0;
	private int CurrentStep = 0;
    private float MaxAmount = 0;

	private void Start()
	{
		MaxAmount = progressBar.sizeDelta.x;
		progressBar.sizeDelta = new Vector2(0, progressBar.sizeDelta.y);
		foreach (GameObject lifeLost in lifesLostObjs) {
			lifeLost.SetActive(false);
		}
		foreach (GameObject lifeLost in lifesObjs)
        {
            lifeLost.SetActive(true);
        }
	}

	public void Update()
    {
		
    }

	public void SetStep(int size) {
		MaxSteps = size;
		CurrentStep = 0;
	}

    public void Restart()
    {
		CurrentStep = 0;
    }

	public void loseLife() {
		if(currentLife < lifesObjs.Length) {
			lifesObjs[currentLife].SetActive(false);
			lifesLostObjs[currentLife].SetActive(true);
			currentLife++;
		}
	}
	public void nextProgressStep() {
        CurrentStep++;
		if(CurrentStep <= MaxSteps) {
			float percent = (float)CurrentStep / (float)MaxSteps;
			progressBar.sizeDelta = new Vector2(percent * MaxAmount, progressBar.sizeDelta.y);
        }
	}
}
