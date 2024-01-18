using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemuManager : MonoBehaviour
{
    public List<Enemu> EnemyList = new List<Enemu>();

    public Enemu EnemyBoss;
    public void AddEnemu(Enemu enemu)
    {
        EnemyList.Add(enemu);
    }
    public void AddBoss(Enemu enemu)
    {
        EnemyBoss = enemu;
    }
    public void RemoveEnemu(Enemu enemu)
    {
        EnemyList.Remove(enemu);
    }
}
