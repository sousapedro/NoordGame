using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarAttack : MonoBehaviour {

    public RectTransform healthBar;

    private float MaxAmount = 100;
    private float MaxTime = 60f;
    public float ActiveTime = 0f;
    public bool Activate = false;

	public void Start()
	{
		MaxAmount = healthBar.sizeDelta.x;
		print(MaxAmount);
		healthBar.sizeDelta = new Vector2(0, healthBar.sizeDelta.y);
	}
 
	public void SetTimerBarAttack(float time) {
		MaxTime = time;
	}
	public void UpdateBar() {
		ActiveTime += Time.deltaTime;

        var percent = ActiveTime / MaxTime;
        float curAmount = Mathf.Lerp(0, 1, percent);
		print(percent);


        healthBar.sizeDelta = new Vector2(curAmount * MaxAmount, healthBar.sizeDelta.y);
	}

    public void Restart()
    {
        healthBar.sizeDelta = new Vector2(0, healthBar.sizeDelta.y);
        ActiveTime = 0f;
        Activate = false;
    }
}
