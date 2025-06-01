using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Environment
{
    [System.Serializable]
    public class Organism
    {
        public enum OrganismType
        {
            PLANT = 0,
            STAGE2 = 1,
            STAGE3 = 2,
            STAGE4 = 3,
            STAGE5 = 4
        }

        public OrganismType GetOrganismType()
        {
            switch (organism_type)
            {
                case "PLANT":
                    return OrganismType.PLANT;
                case "STAGE2":
                    return OrganismType.STAGE2;
                case "STAGE3":
                    return OrganismType.STAGE3;
                case "STAGE4":
                    return OrganismType.STAGE4;
                default:
                    return OrganismType.PLANT;
            }
        }

        public static string GetPrettyName(OrganismType organismType)
        {
            switch (organismType)
            {
                case OrganismType.PLANT:
                    return "Plant";
                case OrganismType.STAGE2:
                    return "Stage 2";
                case OrganismType.STAGE3:
                    return "Stage 3";
                case OrganismType.STAGE4:
                    return "Stage 4";
                case OrganismType.STAGE5:
                    return "Stage 5";
            }
            return "";
        }


        public string organism_type;
        public int energy;
        //public int nutrition;
        //public (int, int) position;
        //public int speed;
        //public float grow_rate;
        //public float reproduction_rate;
        //public float reproduction_threshold;
        //public int fov;
    }
}
