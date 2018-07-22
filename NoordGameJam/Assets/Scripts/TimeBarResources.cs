using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBarResources : MonoBehaviour {

    public RectTransform healthBar;

    private float MaxAmount = 100;
    public float MaxTime = 60f;
    public float ActiveTime = 0f;
    public bool Activate = false;

    public void Start()
    {
		MaxAmount = healthBar.sizeDelta.x;
        healthBar.sizeDelta = new Vector2(0, healthBar.sizeDelta.y);
    }

    public void Update()
    {
        if (Activate)
        {
            ActiveTime += Time.deltaTime;
            var percent = ActiveTime / MaxTime;
            float curAmount = Mathf.Lerp(0, 1, percent);

            healthBar.sizeDelta = new Vector2(curAmount * MaxAmount, healthBar.sizeDelta.y);
            if (healthBar.sizeDelta.x == 100)
                Restart();
        }
    }

    public void Restart()
    {
        healthBar.sizeDelta = new Vector2(0, healthBar.sizeDelta.y);
        ActiveTime = 0f;
        Activate = false;
    }
}
