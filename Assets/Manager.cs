using System.Collections;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject slime_prefab;
    [SerializeField] private GameObject[] platform_prefabs;
    public static int Counter_kill = 0;
    public static float Timer = 0;
    [SerializeField] private TMP_Text Text_stat;
    [SerializeField] private float spawnSpeed;
    [SerializeField] private int numberofIslands;
    void Start()
    {
        Timer = 0;
        Counter_kill = 0;
        StartCoroutine(Spawn());
        for (int i = 0; i <numberofIslands ; i++)
        {
            int random_prefab=Random.Range(0,platform_prefabs.Length);
            Vector2 random_position = new Vector2(Random.Range(-15, 17), Random.Range(0, 16));
            Instantiate(platform_prefabs[random_prefab], random_position, transform.rotation);
        };
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            Vector2 slimePos = new Vector2(Random.Range(-15, 17), Random.Range(0, 16));
            Vector2 distance_to_player =(Vector2) Player.player_pos - slimePos;
            if (distance_to_player.x<10|| distance_to_player.y < 10)
            {
                yield return null;
            }
            Instantiate(slime_prefab, slimePos, transform.rotation);
            yield return new WaitForSeconds(spawnSpeed);
        }
    }
        void Update()
    {
        Timer += Time.deltaTime;
        Text_stat.text = "Время игры: " + (int)Timer + "\n Убито слизней: " + Counter_kill;
    }
}
