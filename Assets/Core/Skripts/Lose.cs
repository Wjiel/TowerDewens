using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    [SerializeField] private ManagerUI managerUI;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private GameObject LosePanel;

    [SerializeField] private Animator animLosePanel;

    private int HealthGame = 3;
    private void Start()
    {
        if (managerUI.language == 0)
        {
            text.text = "Çהמנמגüו: " + HealthGame.ToString();
        }
        if (managerUI.language == 1)
        {
            text.text = "Health: " + HealthGame.ToString();
        }


        LosePanel.SetActive(true);
        animLosePanel.SetBool("Start", true);
        Invoke("TimeToSetActive", 1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemu")
        {
            HealthGame--;
            Destroy(other.gameObject);
            if (managerUI.language == 0)
            {
                text.text = "Çהמנמגüו: " + HealthGame.ToString();
            }

            if (managerUI.language == 1)
            {
                text.text = "Health: " + HealthGame.ToString();
            }

            if (HealthGame <= 0)
            {
                managerUI.MySave();
                LosePanel.SetActive(true);
                animLosePanel.SetBool("LoseTrue", true);
            }
        }
    }
    private void TimeToSetActive()
    {
        LosePanel?.SetActive(false);
    }
}
