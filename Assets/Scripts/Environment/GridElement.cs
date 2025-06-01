using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Environment
{
    [System.Serializable]
    public class GridElement
    {
        public enum TerrainType
        {
            ROCK = 1,
            WATER = 2,
            DIRT = 3
        }

        public TerrainType GetTerrainType()
        {
            switch (terrain)
            {
                case "ROCK":
                    return TerrainType.ROCK;
                case "WATER":
                    return TerrainType.WATER;
                case "DIRT":
                    return TerrainType.DIRT;
                default:
                    return TerrainType.DIRT;
            }
        }

        public Organism[] organisms;
        public int position_x;
        public int position_y;
        public string terrain;
        public int sunlight_lvl;
    }
}
