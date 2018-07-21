using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ResearchBuilding : Building
{
	public Research research;
	private bool test;
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
		//resource.value = 20;
		//list.Add(resource);
		//resource = new Resource("Documentação");
		//resource.value = 20;
		//list.Add(resource);
		//resource = new Resource("Açucar");
		//resource.value = 20;
		//list.Add(resource);
		//Research research = new Research(list);
		//SetResearch(research);


        //print("adding resources");
        //foreach (Resource myRes in ResourceList)
        //{
        //    myRes.value += 100;
        //}
    }

    // Update is called once per frame
    public new void FixedUpdate()
    {
		ChangeState();
    }
	public void SetResearch(Research research) {
		this.research = research;
	}
	override public void Interact(Player player)
    {
		interacting = true;
	}
    public override void WhileInteracting(Player player)
    {
		
    }   
	public override void EndInteraction(Player player) {
		interacting = false;
	}
    
	void ChangeState() {
		if(interacting) {
			if(CheckResources()) {
				UpdateResearch();
            }
		} else {
			StopResearch();
            if(test) {
				test = true;
            }
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
			print("research  stoped");
			State = BuildingState.Idle;
			GetComponent<SpriteRenderer>().color = Color.white;
        }
	}
    
	void EndResearch() {
		State = BuildingState.Ready;
		GetComponent<SpriteRenderer>().color = Color.white;
		print("research  ended");
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
}
