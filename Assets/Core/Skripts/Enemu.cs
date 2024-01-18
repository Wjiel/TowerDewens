using System.Collections;
using UnityEngine;

public enum TypeEnemy { Enemy, Boss };
public class Enemu : MonoBehaviour
{
    ManagerUI managerUI;
    EnemuManager enemuManager;

    private ParticleSystem particle;

    [HideInInspector] public int NumberCount;
    public GameObject[] wayPoint;

    [SerializeField] private float speed;

    [SerializeField] private int Score;

    [SerializeField] private TypeEnemy Type;

    public GameObject[] Enemy;

    public int Health;
    void Start()
    {
        managerUI = FindObjectOfType<ManagerUI>();
        enemuManager = FindObjectOfType<EnemuManager>();
        particle = GameObject.Find("EnemyPariclDead").GetComponent<ParticleSystem>();
        enemuManager.AddEnemu(this);
        if (Type == TypeEnemy.Boss)
        {
            StartCoroutine(BossToSpawnEnemy());
            enemuManager.AddBoss(this);
        }
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoint[NumberCount].transform.position, speed * Time.deltaTime);
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "count")
        {
            if (transform.position == wayPoint[NumberCount].transform.position)
            {
                NumberCount++;
            }
        }
        if (collision.gameObject.name == "LoseObject")
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            enemuManager.RemoveEnemu(this);
            particle.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            particle.Play();
            gameObject.SetActive(false);

            managerUI.AddMoney();
            managerUI.AddScore(Score);
            Destroy(gameObject);
        }

    }
    private IEnumerator BossToSpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            int random = Random.Range(0, Enemy.Length);
            Enemy[random].GetComponent<Enemu>().wayPoint = wayPoint;
            Enemy[random].GetComponent<Enemu>().NumberCount = NumberCount;
            Instantiate(Enemy[random], transform.position, Quaternion.identity);
        }
    }
}
