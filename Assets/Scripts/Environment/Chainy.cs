using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Environment
{
    [System.Serializable]
    public class Chainy
    {
        public Organism[] organisms;
        public GridElement[][] grid;
        public int size_x;
        public int size_y;
        public int time_step;
        public float sim_speed;
        public float[] stats;
    }
}
