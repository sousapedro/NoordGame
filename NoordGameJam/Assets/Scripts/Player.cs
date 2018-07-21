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

    public Resource Resource1;
    public Resource Resource2;
    public Resource Resource3;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        HorizontalAxis += playerId;
        VerticalAxis += playerId;
        if (Type == PlayerType.Colonia)
        {
            Resource1 = new Resource("Madeira");
            Resource2 = new Resource("Documentação");
            Resource3 = new Resource("Açucar");
        }
        else
        {
            Resource1 = new Resource("Ouro");
            Resource2 = new Resource("Armas");
            Resource3 = new Resource("Tecnologia");
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
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("space"))
        {
           // GameObject GO = other.gameobject;
            print("space key was pressed");
        }
        //if (stay)
        {
          //  if (stayCount > 0.25f)
            {
               // Debug.Log("staying");
            //    stayCount = stayCount - 0.25f;
            }
           // else
            {
              //  stayCount = stayCount + Time.deltaTime;
            }
        }
    }
}
