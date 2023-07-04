using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject rankingUIObj;
    public GameObject rankingCloseBtn;
    public GameObject rankingInfoObj;

    public void OnRankingUI()
    {
        rankingUIObj.SetActive(true);
        rankingCloseBtn.SetActive(true);

        foreach(Transform obj in rankingUIObj.GetComponentInChildren<Transform>())
        {
            if (obj != rankingUIObj.transform) Destroy(obj.gameObject);
        }

        for (int i = 0; i < RankingManager.ranking.Length;  i++)
        {
            if (RankingManager.ranking[i].score == 0) return;

            Text rankingInfoText = Instantiate(rankingInfoObj, rankingUIObj.transform).GetComponent<Text>();
            rankingInfoText.text = "\"" + RankingManager.ranking[i].name + "\", " + RankingManager.ranking[i].score;
        }
    }

    public void OffRankingUI()
    {
        rankingUIObj.SetActive(false);
        rankingCloseBtn?.SetActive(false);
    }

    public void StartGame()
    {
        InGameManager.currentStage = 1;
        InGameManager.totalTimer = 0;
        InGameManager.score = 0;
        SceneManager.LoadScene("Stage1Scene");
    }
}
