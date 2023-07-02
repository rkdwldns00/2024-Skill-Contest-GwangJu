using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    public static Rank[] ranking
    {
        get
        {
            Rank[] rank = new Rank[5];
            for (int i = 0; i < 5; i++)
            {
                rank[i] = new Rank() { name = PlayerPrefs.GetString("rn" + i.ToString()), score = PlayerPrefs.GetInt("rs" + i.ToString()) };
            }
            return rank;
        }
        private set
        {
            for (int i = 0; i < 5; ++i)
            {
                PlayerPrefs.SetString("rn" + i.ToString(), value[i].name);
                PlayerPrefs.SetInt("rs" + i.ToString(), value[i].score);
            }
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public static void AddRank(Rank rank)
    {
        List<Rank> list = ranking.ToList<Rank>();
        list.Add(rank);
        list.Sort((a, b) => b.score - a.score);
        Rank[] ranks = new Rank[5];
        for(int i=0;i<5;i++)
        {
            ranks[i] = list[i];
        }
        ranking = ranks;
    }
}

public struct Rank
{
    public string name;
    public int score;
}