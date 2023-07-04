using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public static int currentStage; // 1, 2, 3, 4, 5 ¼ø¼­
    public static int score;
    public static float totalTimer;
    public static void NextStage()
    {
        if (currentStage == 5) SceneManager.LoadScene("EndGameScene");
        else
        {
            currentStage++;
            SceneManager.LoadScene("Stage" + currentStage + "Scene");
        }
    }

    public static void RestartStage() => SceneManager.LoadScene("Stage" + currentStage + "Scene");

    public static void MoveStage(int stageIdx)
    {
        currentStage = stageIdx;
        SceneManager.LoadScene("Stage" + stageIdx + "Scene");
    }
}