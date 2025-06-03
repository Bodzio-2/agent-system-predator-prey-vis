using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;
using Newtonsoft.Json;
using System;
using System.IO;
using TMPro;
using UI;

public class SimulationHandler : MonoBehaviour
{
    public static SimulationHandler Instance { get; private set; }
    private const string JSON_PATH = "C:\\Users\\dunge\\Desktop\\Agent Systems\\simulation_full_run.json";

    public GameObject gridElementPrefab;
    private List<List<GridElementRenderer>> gridElementRenderers = new();

    [SerializeField]
    Transform simAnchor;

    [SerializeField]
    private AnimalStatsPanel statsPanel;

    [SerializeField]
    private SimTimelineUI simTimelineUI;

    [SerializeField]
    private TextMeshProUGUI simStepText;

    float timer = 0.0f;
    public float syncDelay = 1.0f;

    [SerializeField]
    bool jsonData = false;

    bool isPlaying = true;
    bool initialSetup = true;

    int dataIndex = 0;
    List<string> allData = new();
    List<Chainy> allDataJson = new();

    SocketClient client;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        if (!jsonData)
            RetrieveDataTcp();
        else
            RetrieveDataJson(JSON_PATH);
    }

    private void Update()
    {
        if (Time.time > timer && (dataIndex < allData.Count || dataIndex < allDataJson.Count) && isPlaying)
        {
            simTimelineUI.SetIndex(dataIndex);
            UpdateVisuals();
            timer = Time.time + syncDelay;
            if(simStepText)
                simStepText.text = (dataIndex + 1).ToString();
            dataIndex++;
        }

        if ((dataIndex >= allData.Count && dataIndex >= allDataJson.Count) && !initialSetup)
        {
            Debug.Log("END OF SIMULATION");
            isPlaying = false;
        }

        //if (Input.GetKeyDown("q"))
        //    client.ShutdownConnection();
    }


    public void PlayPauseSim()
    {
        isPlaying = !isPlaying;
    }

    public bool GetPlaying()
    {
        return isPlaying;
    }

    public void SetSimSpeed(float simSpeed)
    {
        syncDelay = 1 / simSpeed;
    }

    public void SelectTimestamp(int timeIndex)
    {
        if((allData.Count > timeIndex || allDataJson.Count > timeIndex) && timeIndex >= 0)
        {
            dataIndex = timeIndex;
            if (simStepText)
                simStepText.text = (dataIndex + 1).ToString();
            UpdateVisuals();
        }
    }

    private void RetrieveDataJson(string jsonPath)
    {
        string data = "";
        Debug.Log("Start receiving data...");
        try
        {
            data = File.ReadAllText(jsonPath);
        }catch(Exception e)
        {
            Debug.LogWarning("Error while retrieving JSON data!: " + e.Message);
            return;
        }
        allDataJson = new(JsonConvert.DeserializeObject<IEnumerable<Chainy>>(data));
        Debug.Log("All data received!");
    }

    private void RetrieveDataTcp()
    {
        client = new SocketClient();
        client.StartClient();

        Debug.Log("Start receiving data...");
        while (true)
        {
            string data = client.ReceiveData();
            if (data == "<EOF>")
                break;
            else if (data != "")
                allData.Add(data);
        }
        Debug.Log("All data received!");
    }

    private void UpdateVisuals()
    {
        //string data = client.ReceiveData();
        string data = "";
        if(allData.Count > dataIndex)
            data = allData[dataIndex];


        if (data != "" || allDataJson.Count > 0)
        {
            Chainy envData;
            if (!jsonData)
                envData = JsonConvert.DeserializeObject<Chainy>(data);
            else
                envData = allDataJson[dataIndex];


            if (initialSetup)
            {
                int i = 0;
                float z = 0;
                // Spawn all grid elements on the map
                foreach(GridElement[] gridRow in envData.grid)
                {
                    gridElementRenderers.Add(new());
                    float x = 0;
                    
                    foreach(GridElement gridEl in gridRow)
                    {
                        GameObject gridElement = Instantiate(gridElementPrefab, simAnchor);
                        gridElement.transform.position = new Vector3(x, 0f, z);
                        gridElementRenderers[i].Add(gridElement.GetComponent<GridElementRenderer>());
                        x += 10;
                    }
                    i += 1;
                    z += 10;
                }
                simTimelineUI.InitializeTimeline(jsonData ? allDataJson.Count : allData.Count, dataIndex);
                initialSetup = false;
            }

            for(int i = 0; i<gridElementRenderers.Count; i++)
            {
                for(int j = 0; j<gridElementRenderers[i].Count; j++)
                {
                    gridElementRenderers[i][j].UpdateVisuals(envData.grid[i][j]);
                }
            }
            UpdateStats(envData);

        }
    }

    private void UpdateStats(Chainy chainy)
    {
        Dictionary<Organism.OrganismType, AnimalStat> statDict = new();
        foreach (Organism.OrganismType orgType in Enum.GetValues(typeof(Organism.OrganismType)))
            statDict.Add(orgType, new AnimalStat(orgType, Organism.GetPrettyName(orgType), 0));

        // Iterate over all grid elements and count every plant/animal and update stats
        foreach (GridElement[] gridRow in chainy.grid)
            foreach (GridElement gridEl in gridRow)
                foreach (Organism org in gridEl.organisms)
                    statDict[org.GetOrganismType()].organismCount++;
        //foreach(Organism org in chainy.organisms)
        //    statDict[org.GetOrganismType()].organismCount++;

        // Update UI
        AnimalStat[] animalStats = new AnimalStat[5];

        statDict.Values.CopyTo(animalStats, 0);

        statsPanel.UpdateStats(new List<AnimalStat>(animalStats));
        //statsPanel.UpdateStats();

    }
}
