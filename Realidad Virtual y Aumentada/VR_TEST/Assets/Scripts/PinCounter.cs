using UnityEngine;

public class PinCounter : MonoBehaviour
{
    public int fallenPins = 0;
    public TMPro.TextMeshProUGUI text; 

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pin"))
        {
            fallenPins++;
            other.tag = "Untagged";
            text.text = fallenPins.ToString();
        }
    }
}
