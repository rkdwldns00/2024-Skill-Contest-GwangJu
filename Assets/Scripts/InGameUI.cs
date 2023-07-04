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
        t.text = "������� : " + LevelManager.instance.life
            + "\n������ : " + c
            + "\n��ų : " + FindObjectOfType<Player>()?.haveSkill ?? ""
            + "\n���� : " + (InGameManager.score + LevelManager.instance.score)
            + "\n�ð� : " + (int)(InGameManager.totalTimer + LevelManager.instance.mapTimer);
    }
}
