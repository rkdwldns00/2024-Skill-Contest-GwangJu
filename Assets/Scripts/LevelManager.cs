using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

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

    int life = 10;

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
            Debug.LogWarning("�����Ŵ����� �ߺ��˴ϴ�.");
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
        if (Input.GetKeyDown(KeyCode.F2))
        {
            life = Mathf.Min(10, life + 1);
        }

        rotateCentor.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), rotateValue);
        rotateSpeed += Time.deltaTime;
        rotateValue += rotateSpeed * Time.deltaTime;
    }

    public void ResetLevel()
    {
        if(life <= 0)
        {
            SceneManager.LoadScene("EndGameScene");
        }
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
    }

    public void SetRotate(float x, float y, float z)
    {
        SetRotate(new Vector3(x, y, z));
    }
}
