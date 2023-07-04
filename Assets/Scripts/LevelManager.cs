using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public bool isDebuging;

    public List<IResetable> resetable = new List<IResetable>();

    [SerializeField] Transform rotateCentor;
    public Transform RotateCentor
    {
        get
        {
            if (rotateCentor == null)
            {
                return transform;
            }
            return rotateCentor;
        }
    }

    Vector3 targetRotation = Vector3.zero;
    float rotateSpeed = 0f;
    float rotateValue = 0f;

    public int life = 10;
    public int score = 0;
    float mapTimer=0;

    List<Monster> monsters = new List<Monster>();

    public static void AddResetable(IResetable listener)
    {
        instance.resetable.Add(listener);
    }

    private void Awake()
    {
        if (instance == null)
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
        monsters = FindObjectsOfType<Monster>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        mapTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.F2))
        {
            life = Mathf.Min(10, life + 1);
        }

        rotateCentor.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), rotateValue);
        rotateSpeed += Time.deltaTime;
        rotateValue += rotateSpeed * Time.deltaTime;

        bool c = true;
        monsters.ForEach((x) => { if (x.gameObject.activeSelf) c = false; });
        if (c || Input.GetKeyDown(KeyCode.F3))
        {
            InGameManager.totalTimer += mapTimer;
            InGameManager.score += score;
            InGameManager.score += life * 50;
            InGameManager.NextStage();
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.2f, Time.deltaTime * 5);
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.deltaTime * 5);
        }
    }

    public void ResetLevel()
    {
        if (life <= 0)
        {
            InGameManager.totalTimer += mapTimer;
            InGameManager.score += score;
            SceneManager.LoadScene("EndGameScene");
        }
        mapTimer = 0;
        score = 0;
        resetable.RemoveAll((x) => x == null);
        resetable.ForEach((x) => { x.ResetObject(); });
        life--;
        if (transform.rotation != Quaternion.identity)
        {
            SetRotate(0, 0, 0);
        }
    }

    public void SetRotate(Vector3 rotation)
    {
        targetRotation = rotation;
        rotateSpeed = 0f;
        rotateValue = 0f;
        if (FindObjectOfType<Player>() != null)
        {
            FindObjectOfType<Player>().stunTimer = 0.5f;
        }
        if (FindObjectsOfType<Monster>() != null)
        {
            Object[] monsters = FindObjectsOfType<Monster>();
            foreach (Monster monster in monsters)
            {
                monster.stunTime = 0.5f;
            }
        }
    }

    public void SetRotate(float x, float y, float z)
    {
        SetRotate(new Vector3(x, y, z));
    }
}
