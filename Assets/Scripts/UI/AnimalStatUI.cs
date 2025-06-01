using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Environment;

namespace UI
{
    public class AnimalStatUI : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> organismPrefabs = new();
        [SerializeField]
        private Transform prefabSpawnPoint;

        [SerializeField]
        TextMeshProUGUI organismNameText;
        [SerializeField]
        TextMeshProUGUI organismCountText;

        private bool initialSetup = true;

        Dictionary<Organism.OrganismType, GameObject> organismDict = new();
        private void Start()
        {
            organismDict.Add(Organism.OrganismType.PLANT, organismPrefabs[0]);
            organismDict.Add(Organism.OrganismType.STAGE2, organismPrefabs[1]);
            organismDict.Add(Organism.OrganismType.STAGE3, organismPrefabs[2]);
            organismDict.Add(Organism.OrganismType.STAGE4, organismPrefabs[3]);
            organismDict.Add(Organism.OrganismType.STAGE5, organismPrefabs[4]);
        }

        public void UpdateStat(AnimalStat stats)
        {
            if (initialSetup)
            {
                // Spawn the model and set the name text
                Instantiate(organismDict[stats.organismType], prefabSpawnPoint);
                organismNameText.text = stats.organismName;
                initialSetup = false;
            }
            organismCountText.text = stats.organismCount.ToString();
        }
    }
}
