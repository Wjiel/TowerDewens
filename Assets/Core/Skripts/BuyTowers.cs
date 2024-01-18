using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyTowers : MonoBehaviour
{
    public ManagerUI managerUI;

    public TowerManager towerManager;

    [SerializeField] private ParticleSystem particle;

    [SerializeField] private int prise;

    [SerializeField] private TextMeshProUGUI textTowerCost;

    [SerializeField] private GameObject[] TowerPrefabs;

    [SerializeField] private GameObject Panel;

    [SerializeField] private GameObject ButtonExit;

    private int lg;
    private void Start()
    {
        lg = managerUI.language;
    }
    private void OnEnable()
    {
        if (lg == 0)
        {
            textTowerCost.text = "Цена: " + prise.ToString();
        }

        if (lg == 1)
        {
            textTowerCost.text = "Price: " + prise.ToString();
        }

    }
    public void OnClick(int value)
    {
        if (managerUI.Money >= prise)
        {
            managerUI.RemoveMovey(prise);
            GameObject tower = Instantiate(TowerPrefabs[value]);
            tower.transform.position = new Vector3(towerManager.BuildPoint.transform.position.x, towerManager.BuildPoint.transform.position.y + 0.5f, towerManager.BuildPoint.transform.position.z);
            particle.transform.position = new Vector3(tower.transform.position.x, tower.transform.position.y, tower.transform.position.z);
            particle.Play();
            Destroy(towerManager.BuildPoint);
            ButtonExit.SetActive(false);
            Panel.SetActive(false);
        }
    }
}
