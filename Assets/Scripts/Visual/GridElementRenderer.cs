using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;

public class GridElementRenderer : MonoBehaviour
{
    [SerializeField]
    private Material dirtMaterial;
    [SerializeField]
    private Material rockMaterial;
    [SerializeField]
    private Material waterMaterial;

    //public GridElement gridElement;

    [SerializeField]
    private List<GameObject> treePrefabs = new();


    [SerializeField]
    private GameObject stage2Prefab;
    [SerializeField]
    private GameObject stage3Prefab;
    [SerializeField]
    private GameObject stage4Prefab;
    [SerializeField]
    private GameObject stage5Prefab;

    [SerializeField]
    float xSpawnLimit = 0f;
    [SerializeField]
    float zSpawnLimit = 0f;


    private List<GameObject> organisms = new();

    private Material GetGridMaterial(GridElement.TerrainType terrainType)
    {
        switch (terrainType)
        {
            case GridElement.TerrainType.ROCK:
                return rockMaterial;
            case GridElement.TerrainType.WATER:
                return waterMaterial;
            case GridElement.TerrainType.DIRT:
                return dirtMaterial;
            default:
                return rockMaterial;
        }
    }

    public void UpdateVisuals(GridElement gridElement)
    {
        GetComponent<Renderer>().material = GetGridMaterial(gridElement.GetTerrainType());

        // Destroy previous & spawn all the animals and plants
        foreach (GameObject _gameObject in organisms)
            Destroy(_gameObject);

        foreach(Organism organism in gridElement.organisms)
        {
            GameObject toSpawn = null;
            switch (organism.GetOrganismType())
            {
                case Organism.OrganismType.PLANT:
                    toSpawn = treePrefabs[Random.Range(0, treePrefabs.Count)];
                    break;
                case Organism.OrganismType.STAGE2:
                    toSpawn = stage2Prefab;
                    break;
                case Organism.OrganismType.STAGE3:
                    toSpawn = stage3Prefab;
                    break;
                case Organism.OrganismType.STAGE4:
                    toSpawn = stage4Prefab;
                    break;
                case Organism.OrganismType.STAGE5:
                    toSpawn = stage5Prefab;
                    break;
                default:
                    break;
            }

            if(toSpawn != null)
            {
                GameObject newOrganism = Instantiate(toSpawn, gameObject.transform);
                newOrganism.transform.localPosition = new Vector3(Random.Range(-xSpawnLimit, xSpawnLimit), newOrganism.transform.localPosition.y, Random.Range(-zSpawnLimit, zSpawnLimit));
                newOrganism.transform.Rotate(new Vector3(newOrganism.transform.rotation.x, Random.Range(0f, 180f), newOrganism.transform.rotation.z));
                organisms.Add(newOrganism);
            }
        }
    }
}
