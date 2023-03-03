using System;
using TMPro;
using UnityEngine;

public class ShowDescription : MonoBehaviour
{
    private GetObj _obj;
    private GameObject _tmp;
    private TextMeshProUGUI _textMeshPro;
    
    private void Awake()
    {
        _obj = GetComponent<GetObj>();
        _tmp = GameObject.Find("Description");
        _textMeshPro = _tmp.GetComponent<TextMeshProUGUI>();
    }
    
    void Update()
    {
        try
        {
            var description = _obj.Hit.transform.GetComponent<Description>();
            _tmp.SetActive(true);
            _textMeshPro.SetText(description.description);
        }
        catch (NullReferenceException)
        {
            _tmp.SetActive(false);
        }
        
    }
}
