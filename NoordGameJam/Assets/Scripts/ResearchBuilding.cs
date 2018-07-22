using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ResearchBuilding : Building
{
	public Research research;
	public delegate void ResearchCompleted();
	private ResearchCompleted onResearchCompleted;
    // Use this for initialization
    public new void Start()
    {
		base.Start();

        ResourceList.Add(new Resource("Madeira"));
        ResourceList.Add(new Resource("Documentação"));
        ResourceList.Add(new Resource("Açucar"));
        ResourceList.Add(new Resource("Ouro"));
        ResourceList.Add(new Resource("Armas"));
        ResourceList.Add(new Resource("Tecnologia"));

		//List<Resource> list = new List<Resource>();
		//Resource resource = new Resource("Madeira");
		//resource.modifyResource(20);
		//list.Add(resource);
		//resource = new Resource("Documentação");
		//resource.modifyResource(20);
		//list.Add(resource);
		//resource = new Resource("Açucar");
		//resource.modifyResource(20);
		//list.Add(resource);
		//Research research = new Research(list);
		//SetResearch(research);

        
        print("adding resources");
        foreach (Resource myRes in ResourceList)
        {
			myRes.modifyResource(5);
        }
    }
	private void Reset()
	{
        State = BuildingState.Idle;
		currentCollect = 0;
	}

	// Update is called once per frame
	public new void FixedUpdate()
    {
    }
	public void SetResearch(Research research, ResearchCompleted callback) {
		onResearchCompleted = callback;
		this.research = research;
		Reset();
	}
	override public void Interact(Player player)
    {
		interacting = true;
        ChangeState();
	}
    public override void WhileInteracting(Player player)
    {
        ChangeState();
    }   
	public override void EndInteraction(Player player) {
		interacting = false;
        ChangeState();
	}
    
	void ChangeState() {
		if(interacting) {
			if(CheckResources()) {
				UpdateResearch();
            }
		} else {
			StopResearch();
		}
	}
    void UpdateResearch()
	{
        if (State == BuildingState.Idle)
        {
			print("research  started");
            State = BuildingState.Loading;

			currentCollect += Time.deltaTime;

            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        if (State == BuildingState.Loading)
        {
			currentCollect += Time.deltaTime;
			if(currentCollect >= collectRate) {
				EndResearch();
			}
        }
    }
	void StopResearch() {
        if (State == BuildingState.Loading)
        {
			print("research stoped");
			State = BuildingState.Idle;
			GetComponent<SpriteRenderer>().color = Color.white;
        }
	}
    
	void EndResearch() {
		State = BuildingState.Ready;
		GetComponent<SpriteRenderer>().color = Color.white;
		print("research ended");
		onResearchCompleted();
	}

	bool CheckResources() {
		bool ret = true;
		foreach (Resource otherRes in research.ResourceList)
        {
			foreach (Resource myRes in ResourceList)
            {
				if(myRes.name == otherRes.name) {
					ret = ret && myRes.value >= otherRes.value;
					if(!ret) {
						break;
					}
				}
            }
			if(!ret) {
				break;
			}
        }
		return ret;
	}

	void RemoveResources() {

        foreach (Resource otherRes in research.ResourceList)
        {
            foreach (Resource myRes in ResourceList)
            {
                if (myRes.name == otherRes.name)
                {
					myRes.modifyResource(-otherRes.value);
                }
            }
        }
	}
}
