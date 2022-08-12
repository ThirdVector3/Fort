using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Bot : MonoBehaviour
{
    public Vector2 RdoTimeRange;
    public float RdoDistance;

    private Vector3 startPos;
    [Range(0, 360)] public float ViewAngle = 90f;
    public float ViewDistance = 15f;
    public float ViewDistanceTmp = 15f;
    public float DetectionDistance = 3f;
    private float DetectionDistanceTmp = 3f;
    public Transform EnemyEye;
    public GameObject Target;

    public bool bore = false;
    public bool canR = true;

    public TextMeshPro healthDisplay;
    NavMeshAgent agent;


    private float rotationSpeed;
    private Transform agentTransform;
    public float agentHealth = 1000;
    public float delay;
    private float count = 10;
    private bool dead = false;

    IEnumerator death()
    {
        dead = true;
        for (int i = 0; i < count; i++)
        {
            yield return new WaitForSeconds(delay/count);
            gameObject.transform.localScale -= new Vector3(1/count, 1/count, 1/count);
        }
        Destroy(gameObject);
    }
    
    void Start()
    {
        DetectionDistanceTmp = DetectionDistance;
        ViewDistanceTmp = ViewDistance;
        startPos = gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        rotationSpeed = agent.angularSpeed;
        agentTransform = agent.transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(Target.transform.position, agent.transform.position);
        if (distanceToPlayer <= DetectionDistance || IsInView())
        {
            StopCoroutine(RRotateTimer());
            StopCoroutine(RRotation());
            bore = false;
            canR = true;
            RotateToTarget();
            MoveToTarget();
        }
        else
        {
            agent.SetDestination(startPos);
            bore = true;
        }
        DrawViewState();

        if (bore && canR)
        {
            
            canR = false;
            StartCoroutine(RRotateTimer());
        }
        healthDisplay.text = agentHealth.ToString();
        if (agentHealth <= 0 && !dead)
            StartCoroutine(death());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "bullet")
        {
            agentHealth -= collision.gameObject.GetComponent<Bullet>().damage;
            Destroy(collision.gameObject);
            StartCoroutine( AreaExpand());
        }
    }
    private bool IsInView() // true если цель видна
    {
        RaycastHit2D hit2d = Physics2D.Raycast(EnemyEye.position, Target.transform.position - EnemyEye.position, ViewDistance);
        Debug.DrawRay(EnemyEye.position, Target.transform.position - EnemyEye.position);

        float realAngle = Vector2.Angle(EnemyEye.up, Target.transform.position - EnemyEye.position);

        if (hit2d && hit2d.collider.tag == "Player" && realAngle <= ViewAngle / 2)
        {
            Target = hit2d.collider.gameObject;
            return true;
        }
        return false;
    }
    private void RotateToTarget() // поворачивает в стороно цели со скоростью rotationSpeed
    {
        Vector2 direction = Target.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        transform.rotation = rotation;
    }
    private void MoveToTarget() // устанвливает точку движения к цели
    {
        agent.SetDestination(Target.transform.position);
    }
    private void DrawViewState()
    {
        Vector3 left = EnemyEye.position + Quaternion.Euler(new Vector3(0, 0, ViewAngle / 2f)) * (EnemyEye.up * ViewDistance);
        Vector3 right = EnemyEye.position + Quaternion.Euler(-new Vector3(0, 0, ViewAngle / 2f)) * (EnemyEye.up * ViewDistance);
        Debug.DrawLine(EnemyEye.position, left, Color.yellow);
        Debug.DrawLine(EnemyEye.position, right, Color.yellow);
    }



    IEnumerator RRotation()
    {
        
        float randAngle = Random.Range(RdoDistance, -RdoDistance) / 10;
        for (int i = 0; i < 10; i++)
        {
            Vector3 rotation = transform.rotation.eulerAngles;
            yield return new WaitForSeconds(0.02f);
            transform.rotation = Quaternion.Euler(0, 0, rotation.z + randAngle);
        }
    }



    IEnumerator RRotateTimer()
    {
        if (bore)
        {
            yield return new WaitForSeconds(Random.Range(RdoTimeRange.x, RdoTimeRange.y));
            if (bore)
            {
                StartCoroutine(RRotateTimer());
                StartCoroutine(RRotation());
            }
        }
        else
            yield return null;
    }


    IEnumerator AreaExpand()
    {
        DetectionDistance = DetectionDistanceTmp * 10;
        ViewDistance = ViewDistanceTmp * 10;
        yield return new WaitForSeconds(1);
        ViewDistance = ViewDistanceTmp;
        DetectionDistance = DetectionDistanceTmp;
    }
}
