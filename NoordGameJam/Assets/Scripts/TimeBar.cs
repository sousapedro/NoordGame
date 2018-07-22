using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBar : MonoBehaviour {

    public RectTransform healthBar;

    private float MaxAmount = 100;
    public float MaxTime = 60f;
    public float ActiveTime = 0f;

	private void Start()
	{
		MaxTime = GameController.instance.GetResearchTime();
	}

	public void Update()
    {
        ActiveTime += Time.deltaTime;
        var percent = ActiveTime / MaxTime;
        float curAmount = 1f - Mathf.Lerp(0, 1, percent);

        healthBar.sizeDelta = new Vector2(curAmount * MaxAmount, healthBar.sizeDelta.y);
    }

    public void Restart()
    {
        MaxAmount = 100;
		MaxTime = GameController.instance.GetResearchTime();
        ActiveTime = 0f;
    }
}
