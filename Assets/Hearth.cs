using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearth : MonoBehaviour
{
    [SerializeField] private GameObject HearthTakePlayer;
    [SerializeField] private GameObject smoke_prefab;

    IEnumerator Beating()
    {
        while (true)
        {
            while (transform.localScale.x < 0.15f)
            {
                yield return new WaitForSeconds(0.05f);
                transform.localScale *= 1.1f;

            }
            while (transform.localScale.x > 0.03f)
            {
                yield return new WaitForSeconds(0.05f);
                transform.localScale *= 0.9f;
            }
        }
    }
   
    private void Start()
    {
        StartCoroutine(Beating());
    }
    void FixedUpdate()
    {
        transform.rotation = Quaternion.Euler(0,0,1f);   
    }
    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;
        Instantiate(smoke_prefab, transform.position, transform.rotation);
        GameObject hearthTakePlayer = Instantiate(HearthTakePlayer);
        Destroy(hearthTakePlayer, 1f);
    }
}
