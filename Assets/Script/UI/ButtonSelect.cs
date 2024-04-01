using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSelect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Background;
    public Color SelectColor;
    public Color currentColor = Color.white;
    // Start is called before the first frame update
    private void Update()
    {
        
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        Background.SetActive(true);
        GetComponent<Text>().color = SelectColor;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        Background.SetActive(false);
        GetComponentInChildren<Text>().color = currentColor;
    }
}
