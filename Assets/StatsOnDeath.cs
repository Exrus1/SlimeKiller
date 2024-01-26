
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsOnDeath : MonoBehaviour
{
    [SerializeField] private TMP_Text time;
    [SerializeField] private TMP_Text killed;


    void Start()
    {
        time.text = Manager.Timer.ToString();
        killed.text = Manager.Counter_kill.ToString();
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
}
