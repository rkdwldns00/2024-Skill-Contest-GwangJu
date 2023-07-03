using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItem : MonoBehaviour, IUseable, IResetable
{
    public Vector3 targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.AddResetable(this);
    }

    public void Use(Player player)
    {
        player.transform.position = transform.position;
        LevelManager.instance.SetRotate(targetRotation);
        gameObject.SetActive(false);
    }

    public void ResetObject()
    {
        gameObject.SetActive(true);
    }
}
