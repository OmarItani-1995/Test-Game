using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ErrorText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI errorText;
    public void ShowError(string errorText)
    {
        CancelInvoke();
        this.errorText.text = errorText;
        Invoke("ClearText", 5f);
    }
    
    private void ClearText()
    {
        errorText.text = "";
    }
}
