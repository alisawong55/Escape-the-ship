using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlertMessage : MonoBehaviour
{
    [SerializeField] TMP_Text tmp;
    public IEnumerator showAlert(string message){
        tmp.text = message;
        gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
