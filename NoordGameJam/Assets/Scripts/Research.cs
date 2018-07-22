using System;
using System.Collections.Generic;

public class Research
{
	public List<Resource> ResourceList;
	public Research(List<Resource> list)
    {
		ResourceList = list;
    }
    public static Research NewMetropolyResearch(int wood, int doc, int sugar)
    {
        List<Resource> resources = new List<Resource>();
        Resource resource = new Resource("Madeira");
		resource.modifyResource(wood);
        resources.Add(resource);
        resource = new Resource("Documentação");
		resource.modifyResource(doc);
        resources.Add(resource);
        resource = new Resource("Açucar");
		resource.modifyResource(sugar);
        resources.Add(resource);

		return new Research(resources);
	}
	public static Research NewColonyResearch(int gold, int guns, int tec)
    {
        List<Resource> resources = new List<Resource>();
		Resource resource = new Resource("Ouro");
		resource.modifyResource(gold);
        resources.Add(resource);
		resource = new Resource("Armas");
		resource.modifyResource(guns);
        resources.Add(resource);
		resource = new Resource("Tecnologia");
		resource.modifyResource(tec);
        resources.Add(resource);

        return new Research(resources);
    }
}

