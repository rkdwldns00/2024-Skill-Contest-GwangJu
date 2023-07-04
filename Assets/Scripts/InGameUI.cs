using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    Text t;

    void Start()
    {
        t = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int c = 0;
        LevelManager.instance.monsters.ForEach((x) => { if (x.gameObject.activeSelf) c++; });
        t.text = "남은목숨 : " + LevelManager.instance.life
            + "\n남은적 : " + c
            + "\n스킬 : " + FindObjectOfType<Player>()?.haveSkill ?? ""
            + "\n점수 : " + (InGameManager.score + LevelManager.instance.score)
            + "\n시간 : " + (int)(InGameManager.totalTimer + LevelManager.instance.mapTimer);
    }
}
