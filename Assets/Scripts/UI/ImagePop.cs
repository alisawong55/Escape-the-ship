using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImagePop : MonoBehaviour
{
    public float popDuration = 1f;
    Image imageToPop;

    private void Start()
    {
        imageToPop = gameObject.GetComponent<Image>();
        StartCoroutine(PopImage());
    }

    private IEnumerator PopImage()
    {
        float timer = 0f;

        while (timer < popDuration)
        {
            float scale = Mathf.Lerp(0f, 1f, timer / popDuration);
            imageToPop.transform.localScale = new Vector3(scale, scale, 1f);

            timer += Time.deltaTime;
            yield return null;
        }

        // Ensure the final scale is set to 1
        imageToPop.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(1f);
        Destroy(this);
    }
}
