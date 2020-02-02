using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

    [CreateAssetMenu]
    class LevelSerializationObject : ScriptableObject
    {
        public List<LevelData> Levels;
    }

    [Serializable]
    struct LevelData {
        public float LevelLengthSeconds;
        public float ItemIncomeTermSeconds;
        /// <summary>
        /// Possibility distribution of the items of this level. 
        /// </summary>
        public List<ItemWithProbability> ItemDistribution;
    public string ChooseOneItem() {
        var f = UnityEngine.Random.Range(0, 1);
        float s = 0f;
        for (int p = 0; p < ItemDistribution.Count; p++)
        {
            s += ItemDistribution[p].Probability;
            if (f < s)
                return ItemDistribution[p].ItemName;
        }
        return ItemDistribution.Last().ItemName;
    
    }
   
    }

    [Serializable]
    struct ItemWithProbability {
        public string ItemName;
        public float Probability;
    
    }


