using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public enum WayToGo
    {
        Colonia,
        Metropole
    }

    public enum ShipState
    {
        Idle,
        Travelling,
        Waiting,
        OnAttack
    }


    public float Speed = 5f;
    private Rigidbody rb;
    public WayToGo Path = WayToGo.Colonia;
    public ShipState State = ShipState.Idle;

    public List<Resource> MyResources;

    public float waitingRate = 0.5F;
	private float currentTime = 0.0F;
	private ShipState lastState = ShipState.Idle;
	private bool needsInteraction = false;
	private Building lastBuilding = null;
    string pathStr = "";

    public AudioClip myAudioClip;
    public AudioSource myAudioSource;

	public Animator animator;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        MyResources.Add(new Resource("Madeira"));
        MyResources.Add(new Resource("Documentação"));
        MyResources.Add(new Resource("Açucar"));
        MyResources.Add(new Resource("Ouro"));
        MyResources.Add(new Resource("Armas"));
        MyResources.Add(new Resource("Tecnologia"));

        myAudioSource.clip = myAudioClip;
        animator.SetTrigger("flip");
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float step = Speed * Time.deltaTime;
        if (Path == WayToGo.Colonia && pathStr != "ColonyDepot")
            pathStr = "ColonyDepot";
        else if (Path == WayToGo.Metropole && (pathStr != "MetropolyDepot" && pathStr != "MetropolyDepot1"))
            pathStr = Random.Range(0, 2) == 0 ? "MetropolyDepot" : "MetropolyDepot1";
 //       pathStr = Path == WayToGo.Colonia ? "ColonyDepot" : "MetropolyDepot";
        
        // Move our position a step closer to the target.
		if (State == ShipState.Travelling) {
			transform.position = Vector3.MoveTowards(transform.position, GameObject.Find(pathStr).transform.position , step);
        }
        else if (State == ShipState.Idle)
        {
            State = ShipState.Travelling;
        }
		else if (State == ShipState.Waiting)
        {
            if(currentTime < waitingRate) {
				currentTime += Time.deltaTime;
            } else {
                if (Path == WayToGo.Colonia)
                    Path = WayToGo.Metropole;
                else
                    Path = WayToGo.Colonia;
                
                State = ShipState.Travelling;
				animator.SetTrigger("flip");
			}
		} else if(State == ShipState.OnAttack) {
			//do nothing?
		}
    }

	private void OnTriggerStay(Collider other)
	{
        if (other.GetComponent<DepositBuilding>())
        {
            DepositBuilding building = other.GetComponent<DepositBuilding>();
            if (building.isUnderAttack)
            {
                setOnAttack(building);
			} else if (needsInteraction) {
				needsInteraction = false;
				building.ShipInteract(this);
			}
        }
	}
	private void OnTriggerEnter(Collider other)
	{
        if (other.GetComponent<DepositBuilding>())
        {
            DepositBuilding building = other.GetComponent<DepositBuilding>();
            if (building.isUnderAttack)
            {
                setOnAttack(building);
				needsInteraction = true;
            }
            else
            {
                building.ShipInteract(this);
            }
        }
    }

    public void collectResources(DepositBuilding depot)
    {
        List<Resource> resources = depot.ResourceList;

        foreach (Resource myRes in MyResources)
        {
            foreach (Resource otherRes in resources)
            {
                if (myRes.name == otherRes.name)
                {
                    myRes.modifyResource(otherRes.value);// 1;
                    otherRes.modifyResource(-otherRes.value);
                }
            }
        }
    }

    public void depositResources(ResearchBuilding researchBuilding)
    {
        List<Resource> resources = researchBuilding.ResourceList;
        foreach (Resource myRes in MyResources)
        {
            foreach (Resource otherRes in resources)
            {
                if (myRes.name == otherRes.name)
                {
                    otherRes.modifyResource(myRes.value);
                    myRes.modifyResource(-myRes.value);
                }
            }
        }
    }

    public void startWaiting()
    {
        State = ShipState.Waiting;
		currentTime = 0;
        if(myAudioSource != null)
            myAudioSource.Play();
    }

    public void debugResources()
    {
        print("=================");
        foreach (Resource myRes in MyResources)
        {
            print(myRes.name + ":" + myRes.value);
        }
    }

	public void setOnAttack(Building building) {
		if(State != ShipState.OnAttack) {
			lastState = State;
			State = ShipState.OnAttack;
			lastBuilding = building;
			building.RegisterOnAttackFinished(OnAttackFinished);
        }
	}
	public void OnAttackFinished(bool nothing) {
		State = lastState;
		lastBuilding.RemoveOnAttackFinished(OnAttackFinished);
		lastBuilding = null;
	}
}
