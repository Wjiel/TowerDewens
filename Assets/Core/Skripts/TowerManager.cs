using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerManager : MonoBehaviour
{
    private Camera _camera;
    public ManagerUI managerUI;

    [HideInInspector] public GameObject SelectTower;
    [HideInInspector] public GameObject BuildPoint;
    [HideInInspector] public GameObject BuildPointColor;

    public ParticleSystem DestroyEffect;

    public GameObject SellPanel;

    public GameObject panelBuyTower;

    public GameObject ButtonExit;

    public GameObject PanelBuildPoint;
    public LayerMask layer;

    [Header("UpPanel")]
    public int[] prise;

    public GameObject[] Bullet;

    [SerializeField] private TextMeshProUGUI[] textPrice;

    [SerializeField] private AudioSource _audio;

    [SerializeField] private TextMeshProUGUI[] TextStats;

    [SerializeField] private int[] NumberForStats;
    private void Start()
    {
        _camera = GetComponent<Camera>();
        prise[0] = 5;
        prise[1] = 20;

        if (managerUI.language == 0)
        {
            textPrice[0].text = "Цена: " + prise[0].ToString();
            textPrice[1].text = "Цена: " + prise[1].ToString();
        }

        if (managerUI.language == 1)
        {
            textPrice[0].text = "Price: " + prise[0].ToString();
            textPrice[1].text = "Price: " + prise[1].ToString();
        }

        Bullet[0].GetComponent<BulletScripts>().damage = 1;
        Bullet[1].GetComponent<BulletScripts>().damage = 15;


        if (managerUI.language == 0)
        {
            TextStats[0].text = "Урон:\n" + Bullet[0].GetComponent<BulletScripts>().damage.ToString() + " > " + (Bullet[0].GetComponent<BulletScripts>().damage + NumberForStats[0]).ToString();
            TextStats[1].text = "Урон:\n" + Bullet[1].GetComponent<BulletScripts>().damage.ToString() + " > " + (Bullet[1].GetComponent<BulletScripts>().damage + NumberForStats[1]).ToString();
        }

        if (managerUI.language == 1)
        {
            TextStats[0].text = "Damage:\n" + Bullet[0].GetComponent<BulletScripts>().damage.ToString() + " > " + (Bullet[0].GetComponent<BulletScripts>().damage + NumberForStats[0]).ToString();
            TextStats[1].text = "Damage:\n" + Bullet[1].GetComponent<BulletScripts>().damage.ToString() + " > " + (Bullet[1].GetComponent<BulletScripts>().damage + NumberForStats[1]).ToString();
        }

    }
    void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, layer))
        {
            {
                if (hit.collider.tag != "BuildPoint")
                {
                    if (BuildPointColor != null)
                    {
                        BuildPointColor.transform.localScale = new Vector3(1f, 0.1f, 1f);
                        BuildPointColor.GetComponent<MeshRenderer>().material.color = Color.white;
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, layer))
            {
                if (hit.collider.tag == "Tower")
                {
                    SelectTower = hit.transform.gameObject;
                    SellPanel.SetActive(true);
                    ButtonExit.SetActive(true);
                }

                if (hit.collider.CompareTag("BuildPoint"))
                {
                    BuildPoint = hit.transform.gameObject;
                    panelBuyTower.SetActive(true);
                    ButtonExit.SetActive(true);

                    BuildPointColor = hit.transform.gameObject;
                    BuildPointColor.transform.localScale = new Vector3(1.1f, 0.1f, 1.1f);
                    BuildPointColor.GetComponent<MeshRenderer>().material.color = Color.black;
                }
            }
        }
    }
    public void Sell()
    {
        managerUI.AddMoney();

        DestroyEffect.transform.position = new Vector3(SelectTower.transform.position.x, SelectTower.transform.position.y, SelectTower.transform.position.z);
        DestroyEffect.Play();

        Instantiate(PanelBuildPoint, new Vector3(SelectTower.transform.position.x, SelectTower.transform.position.y - 0.5f, SelectTower.transform.position.z), Quaternion.identity);
        Destroy(SelectTower);

        SellPanel.SetActive(false);
    }
    public void UpGrade(int value)
    {
        if (managerUI.Money >= prise[value])
        {
            if (value == 0)
            {
                _audio.Play();
                managerUI.RemoveMovey(prise[value]);
                prise[value] += 5;
                Bullet[value].GetComponent<BulletScripts>().damage += NumberForStats[value];


                if (managerUI.language == 0)
                {
                    textPrice[value].text = "Цена: " + prise[value].ToString();
                    TextStats[0].text = "Урон:\n" + Bullet[0].GetComponent<BulletScripts>().damage.ToString() + " > " + (Bullet[0].GetComponent<BulletScripts>().damage + NumberForStats[0]).ToString();
                }
                if (managerUI.language == 1)
                {
                    textPrice[value].text = "Price: " + prise[value].ToString();
                    TextStats[0].text = "Damage:\n" + Bullet[0].GetComponent<BulletScripts>().damage.ToString() + " > " + (Bullet[0].GetComponent<BulletScripts>().damage + NumberForStats[0]).ToString();
                }
            }
            else
            {
                _audio.Play();
                managerUI.RemoveMovey(prise[value]);
                prise[value] += 8;
                Bullet[value].GetComponent<BulletScripts>().damage += NumberForStats[value];

                if (managerUI.language == 0)
                {
                    textPrice[value].text = "Цена: " + prise[value].ToString();
                    TextStats[1].text = "Урон:\n" + Bullet[1].GetComponent<BulletScripts>().damage.ToString() + " > " + (Bullet[1].GetComponent<BulletScripts>().damage + NumberForStats[1]).ToString();
                }
                if (managerUI.language == 1)
                {
                    textPrice[value].text = "Price: " + prise[value].ToString();
                    TextStats[1].text = "Damage:\n" + Bullet[1].GetComponent<BulletScripts>().damage.ToString() + " > " + (Bullet[1].GetComponent<BulletScripts>().damage + NumberForStats[1]).ToString();
                }
            }
        }
    }
}
