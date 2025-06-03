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


    //private List<GameObject> organisms = new();

    List<List<GameObject>> cellOrganisms = new();
    //Dictionary<Organism.OrganismType, int> organismTypeToIndex = new();

    private void Start()
    {
        //organismTypeToIndex.Add(Organism.OrganismType.PLANT, 0);
        //organismTypeToIndex.Add(Organism.OrganismType.STAGE2, 1);
        //organismTypeToIndex.Add(Organism.OrganismType.STAGE3, 2);
        //organismTypeToIndex.Add(Organism.OrganismType.STAGE4, 3);
        //organismTypeToIndex.Add(Organism.OrganismType.STAGE5, 4);

        for (int i = 0; i < 5; i++)
            cellOrganisms.Add(new());
    }

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


        //// Destroy previous & spawn all the animals and plants
        //foreach (GameObject _gameObject in organisms)
        //    Destroy(_gameObject);

        int[] tempChange = new int[5];
        

        foreach(Organism organism in gridElement.organisms)
            tempChange[((int)organism.GetOrganismType())]++;

        for(int i=0; i<cellOrganisms.Count; i++)
            tempChange[i] -= cellOrganisms[i].Count;

        for (int i= 0; i < cellOrganisms.Count; i++)
        {
            GameObject toSpawn = null;

            switch (i)
            {
                case 0:
                    toSpawn = treePrefabs[Random.Range(0, treePrefabs.Count)];
                    break;
                case 1:
                    toSpawn = stage2Prefab;
                    break;
                case 2:
                    toSpawn = stage3Prefab;
                    break;
                case 3:
                    toSpawn = stage4Prefab;
                    break;
                case 4:
                    toSpawn = stage5Prefab;
                    break;
                default:
                    break;
            }


            if (tempChange[i] > 0)
            {
                // Add organisms to list
                for (int j = 0; j < tempChange[i]; j++)
                {

                    if (toSpawn != null)
                    {
                        GameObject newOrganism = Instantiate(toSpawn, gameObject.transform);
                        newOrganism.transform.localPosition = new Vector3(Random.Range(-xSpawnLimit, xSpawnLimit), newOrganism.transform.localPosition.y, Random.Range(-zSpawnLimit, zSpawnLimit));
                        newOrganism.transform.Rotate(new Vector3(newOrganism.transform.rotation.x, Random.Range(0f, 180f), newOrganism.transform.rotation.z));
                        //organisms.Add(newOrganism);
                        cellOrganisms[i].Add(newOrganism);
                    }
                }
            }else if(tempChange[i] < 0)
            {
                // Delete organisms from list
                int temp = -tempChange[i];
                for(int j=0; j<temp; j++)
                {
                    GameObject _gameObject = cellOrganisms[i][cellOrganisms[i].Count - 1];
                    cellOrganisms[i].RemoveAt(cellOrganisms[i].Count - 1);
                    Destroy(_gameObject);
                }
            }
        }
    }
}
