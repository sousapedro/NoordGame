using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

    public string name { get; private set; }
    public int value { get; private set; }

    public Resource(string name, int value = 0)
    {
        this.name = name;
        this.value = value;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
