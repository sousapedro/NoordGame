using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour {

    public string name { get; private set; }
    public int value { get; private set; }
    private Action<Resource> onUpdateCb;

    public Resource(string name, int value = 0)
    {
        this.name = name;
        this.value = value;
    }

    public void registerOnUpdateCb(Action<Resource> newCallbackOnUpdate)
    {
        onUpdateCb += newCallbackOnUpdate;
    }

    public void unregisterOnUpdateCb(Action<Resource> newCallbackOnUpdate)
    {
        onUpdateCb -= newCallbackOnUpdate;
    }

    public void modifyResource(int resourceModifier)
    {
        value += resourceModifier;

        if (onUpdateCb != null)
        {
            onUpdateCb(this);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
