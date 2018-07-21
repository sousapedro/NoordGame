using System;
using System.Collections.Generic;

public class Research
{
	public List<Resource> ResourceList;
	public Research(List<Resource> list)
    {
		ResourceList = list;
    }
	public static Research NewColonyResearch(int wood, int doc, int sugar) {
        List<Resource> resources = new List<Resource>();
        Resource resource = new Resource("Madeira");
        resource.value = wood;
        resources.Add(resource);
        resource = new Resource("Documentação");
        resource.value = doc;
        resources.Add(resource);
        resource = new Resource("Açucar");
        resource.value = sugar;
        resources.Add(resource);

		return new Research(resources);
	}
    public static Research NewMetropolyResearch(int wood, int doc, int sugar)
    {
        List<Resource> resources = new List<Resource>();
		Resource resource = new Resource("Ouro");
        resource.value = wood;
        resources.Add(resource);
		resource = new Resource("Armas");
        resource.value = doc;
        resources.Add(resource);
		resource = new Resource("Tecnologia");
        resource.value = sugar;
        resources.Add(resource);

        return new Research(resources);
    }
}

