using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameManager : MonoBehaviour
{
    [Header("Ranking UI")]
    public GameObject rankingUIObj;
    public GameObject rankingInfoObj;

    [Header("Play Info UI")]
    public Text scoreText;

    [Header("Ranking Insert UI")]
    public GameObject rankingInsertUIObj;
    public InputField nameInputField;

    [Header("Etc UI")]
    public Button goToMenuBtn;

    private void Awake()
    {
        OnRankingUI();
        OnPlayInfoUI();
        if (CanRankingInsert(InGameManager.score)) OnRankingInsertUI();
        else OnGoToMenuBtn();
    }

    public void OnRankingUI()
    {
        foreach (Transform obj in rankingUIObj.GetComponentInChildren<Transform>())
        {
            if (obj != rankingUIObj.transform) Destroy(obj.gameObject);
        }

        for (int i = 0; i < RankingManager.ranking.Length; i++)
        {
            if (RankingManager.ranking[i].score == 0) return;

            Text rankingInfoText = Instantiate(rankingInfoObj, rankingUIObj.transform).GetComponent<Text>();
            rankingInfoText.text = RankingManager.ranking[i].name + RankingManager.ranking[i].score;
        }
    }

    public void OnPlayInfoUI()
    {
        scoreText.text = "Score : " + InGameManager.score;
    }

    public bool CanRankingInsert(int score)
    {
        if (RankingManager.ranking[4].score < score) return true;
        return false;
    }

    public void OnRankingInsertUI()
    {
        rankingInsertUIObj.SetActive(true);
    }

    public void RankingInsert()
    {
        RankingManager.AddRank(new Rank() { name = nameInputField.text, score = InGameManager.score} );
        OnRankingUI();
        OnGoToMenuBtn();
    }

    public void OnGoToMenuBtn()
    {
        goToMenuBtn.gameObject.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
