using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Towers : MonoBehaviour
{
    EnemuManager enemuManager;
    [SerializeField]private ParticleSystem effect;

    [SerializeField] private Enemu targetEnemy;

    [SerializeField] private GameObject bulletPrefab;

    [SerializeField] private float CollDown;

    [SerializeField] private float AttackRadius;

    private float time;
    private void Start()
    {
        enemuManager = FindObjectOfType<EnemuManager>();
    }
    private List<Enemu> GetEnemiesInRange()
    {
        List<Enemu> enemiesInRAnge = new List<Enemu>();
        foreach (Enemu enemu in enemuManager.EnemyList)
        {
            if(enemu != null)
            {
                if (Vector3.Distance(transform.position, enemu.transform.position) <= AttackRadius)
                {
                    enemiesInRAnge.Add(enemu);
                }
            }
        }
        return enemiesInRAnge;
    }
    private Enemu GetNearestEnemy()
    {
        Enemu nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach (Enemu enemy in GetEnemiesInRange())
        {
            if (enemy != null)
            {
                if (Vector2.Distance(transform.position, enemy.transform.position) < smallestDistance)
                {
                    smallestDistance = Vector3.Distance(transform.position, enemy.transform.position);
                    nearestEnemy = enemy;
                }
            }
        }
        return nearestEnemy;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }
    private void Update()
    {
        if (GetNearestEnemy() != null)
        {
            targetEnemy = GetNearestEnemy();
        }
        if (time <= CollDown)
        {
            time -= Time.deltaTime;
        }

        if (targetEnemy != null && Vector3.Distance(transform.position, targetEnemy.transform.position) > AttackRadius)
        {
            targetEnemy = null;
        }

        if (time <= 0 && targetEnemy != null)
        {  
            if (time <= 0)
                Shout();
        }
    }
    private void Shout()
    {
        time = CollDown;
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        bullet.GetComponent<BulletScripts>().target = targetEnemy;
        effect.Play();
    }
}
