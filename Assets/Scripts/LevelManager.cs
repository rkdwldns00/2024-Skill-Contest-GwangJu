using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public List<IResetable> resetable = new List<IResetable>();

    public static void AddResetable(IResetable listener)
    {
        instance.resetable.Add(listener);
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("레벨매니저가 중복됩니다.");
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetLevel()
    {
        resetable.RemoveAll((x) => x == null);
        resetable.ForEach((x) => { x.ResetObject(); });
    }
}
