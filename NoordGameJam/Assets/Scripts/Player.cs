using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public enum PlayerType{
        Colonia = 0,
        Metropole
    }

    public float Speed = 5f;
    public int playerId = 1;
    public PlayerType Type;
    private Rigidbody rb;

    private string HorizontalAxis = "Horizontal";
    private string VerticalAxis = "Vertical";

    public List<Resource> MyResources;
	// Use this for initialization
	void Awake () {
        rb = GetComponent<Rigidbody>();
        HorizontalAxis += playerId;
        VerticalAxis += playerId;
        if (Type == PlayerType.Colonia)
        {
            MyResources.Add(new Resource("Madeira"));
            MyResources.Add(new Resource("Documentação"));
            MyResources.Add(new Resource("Açucar"));
        }
        else
        {
            MyResources.Add(new Resource("Ouro"));
            MyResources.Add(new Resource("Armas"));
            MyResources.Add(new Resource("Tecnologia"));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        float moveHorizontal = 0;
        float moveVertical = 0;

        moveHorizontal = Input.GetAxis(HorizontalAxis);
        moveVertical = Input.GetAxis(VerticalAxis);

		Vector3 movement = new Vector3 (moveHorizontal, moveVertical, 0.0f);

        rb.velocity  = movement * Speed;


        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (Resource res in MyResources)
            {
                print(res.name + ":" + res.value);
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            Building building = other.GetComponent<Building>();
			building.Interact(this);
           // GameObject GO = other.gameobject;
        }
    }

    public void collectResources(List<Resource> resources)
    {
        foreach (Resource myRes in MyResources)
	    {
            foreach (Resource otherRes in resources)
            {
                if (myRes.name == otherRes.name)
                {
                    myRes.value += otherRes.value;// 1;
                    otherRes.value = 0;
                }
            }
	    }
    }

    public void depositResources(List<Resource> resources)
    {
        foreach (Resource myRes in MyResources)
        {
            foreach (Resource otherRes in resources)
            {
                if (myRes.name == otherRes.name)
                {
                    otherRes.value += myRes.value;// 1;
                    myRes.value = 0;
                }
            }
        }
    }
}
