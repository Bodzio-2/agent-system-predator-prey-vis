using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environment;

namespace UI {
    public class AnimalStatsPanel : MonoBehaviour
    {
        [SerializeField]
        private AnimalStatUI plantStats;
        [SerializeField]
        private AnimalStatUI stage2Stats;
        [SerializeField]
        private AnimalStatUI stage3Stats;
        [SerializeField]
        private AnimalStatUI stage4Stats;
        [SerializeField]
        private AnimalStatUI stage5Stats;


        private Dictionary<Organism.OrganismType, AnimalStatUI> animalStatDict = new();
        private void Start()
        {
            animalStatDict.Add(Organism.OrganismType.PLANT, plantStats);
            animalStatDict.Add(Organism.OrganismType.STAGE2, stage2Stats);
            animalStatDict.Add(Organism.OrganismType.STAGE3, stage3Stats);
            animalStatDict.Add(Organism.OrganismType.STAGE4, stage4Stats);
            animalStatDict.Add(Organism.OrganismType.STAGE5, stage5Stats);
        }

        public void UpdateStats(List<AnimalStat> animalStats)
        {
            foreach (AnimalStat stat in animalStats)
                animalStatDict[stat.organismType].UpdateStat(stat);
        }
    }
}
