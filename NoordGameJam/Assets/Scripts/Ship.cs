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
        Waiting
    }

    public float Speed = 5f;
    private Rigidbody rb;
    public WayToGo Path = WayToGo.Colonia;
    public ShipState State = ShipState.Idle;

    public List<Resource> MyResources;

    public float waitingRate = 0.5F;
    private float nextWaiting = 0.0F;

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
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float step = Speed * Time.deltaTime;
        string pathStr = Path == WayToGo.Colonia ? "ColonyDepot" : "MetropolyDepot";
        
        // Move our position a step closer to the target.
        if (State == ShipState.Travelling)
            transform.position = Vector3.MoveTowards(transform.position, GameObject.Find(pathStr).transform.position , step);

        if (State == ShipState.Idle)
        {
            State = ShipState.Travelling;
        }
        else if (State == ShipState.Waiting && Time.time > nextWaiting)
        {
            if (Path == WayToGo.Colonia)
                Path = WayToGo.Metropole;
            else
                Path = WayToGo.Colonia;

            State = ShipState.Travelling;
            nextWaiting = Time.time + waitingRate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("entrou");
        if (other.GetComponent<DepositBuilding>())
        {
            DepositBuilding building = other.GetComponent<DepositBuilding>();
            building.ShipInteract(this);
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
        nextWaiting = Time.time + waitingRate;

    }

    public void debugResources()
    {
        print("=================");
        foreach (Resource myRes in MyResources)
        {
            print(myRes.name + ":" + myRes.value);
        }
    }
}
