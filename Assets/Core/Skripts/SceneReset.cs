using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReset : MonoBehaviour
{ 
    public void Reset()
    {
        SceneManager.LoadScene(0);
    }
}
