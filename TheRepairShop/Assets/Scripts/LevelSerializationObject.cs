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
        public List<ItemDifficultyData> ItemDistribution;


    /// <summary>
    /// ReadOnly; Choose one item from ItemDistribution, according to probabilities of the items in it.
    /// </summary>
    /// <returns></returns>
    public ItemDifficultyData ChooseOneItem() {
        var f = UnityEngine.Random.Range(0, 1);
        float s = 0f;
        for (int p = 0; p < ItemDistribution.Count; p++)
        {
            s += ItemDistribution[p].Probability;
            if (f < s)
                return ItemDistribution[p];
        }
        return ItemDistribution.Last();
    
    }
   
    }

    [Serializable]
    struct ItemDifficultyData {
        public string ItemName;
        public float Probability;
        public float TimeGiven;
    
    }


