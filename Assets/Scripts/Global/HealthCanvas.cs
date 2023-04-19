using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthCanvas : MonoBehaviour
{
    public GameObject HealthText;

    void Update()
    {
        HealthText.GetComponent<TextMeshProUGUI>().text = "Health: " + PlayerManager.currHealth;
    }
}
