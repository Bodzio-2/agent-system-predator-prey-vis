using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;

public class AnimalStat
{
    public Organism.OrganismType organismType;
    public string organismName;
    public int organismCount;

    public AnimalStat(Organism.OrganismType organismType, string organismName, int organismCount)
    {
        this.organismType = organismType;
        this.organismCount = organismCount;
        this.organismName = organismName;
    }
}
