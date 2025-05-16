using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPickup : MonoBehaviour
{
    [SerializeField] GameObject healthPickup;
    [SerializeField] GameObject energyPickup;

    public void SpawnPickup(int amount, Vector2 location){
        for(int i = 0; i < amount; i++)
        {
            Vector2 randomLocation = location + Random.insideUnitCircle * 3;
            int randomInt = Random.Range(0, 20);
            if(randomInt <= 4){
                GameObject instance = Instantiate(healthPickup, randomLocation, Quaternion.identity);
                //StartCoroutine(PopAnime(instance));
            }
            else if(randomInt == 5){
                GameObject instance = Instantiate(energyPickup, randomLocation, Quaternion.identity);
                //StartCoroutine(PopAnime(instance));
            }
            else{
                //no item
            }
        }

    }

    private IEnumerator PopAnime(GameObject pop)
    {
        float popDuration = 1f;
        float timer = 0f;

        while (timer < popDuration)
        {
            float scale = Mathf.Lerp(0f, 1f, timer / popDuration);
            pop.transform.localScale = new Vector3(scale, scale, 1f);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is set to 1
        pop.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(1f);
    }
}
