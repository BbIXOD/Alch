using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.UI;
using MyExtensions;

public class InfoPanel : MonoBehaviour
{
    private string _curObj;
    private TextMeshProUGUI _text;
    private Vector2 _ray;
    private  RawImage _image;
    private RaycastHit2D _hit;
    private Camera _camera;
    private RawImage[] _objects = new RawImage[1]; //Треба міняти цю цифру при додаванні нових об'єктів
    private float _alpha;

    private void Awake()
    {
        _text = GameObject.Find("ShowName").GetComponent<TextMeshProUGUI>();
        _camera = Camera.main;
        _image = GetComponent<RawImage>();
        _alpha = _image.color.a;
        _objects = GetComponentsInChildren<RawImage>();
    }
    private void Update()
    {
        _ray = _camera.ScreenToWorldPoint(Input.mousePosition);
        _hit = Physics2D.Raycast(_ray, Vector2.zero);
        if(_hit)
        {
            SetOpacity(_alpha);
            _curObj = Regex.Match(_hit.transform.name, "[a-zA-Z_]+").Value;
            _curObj = _curObj.Replace("_", " ");
            _text.SetText(_curObj);
        }
        else
        {
            SetOpacity(0);
        }
    }

    private void SetOpacity(float opacity)
    {
        foreach (var image in _objects)
        {
            image.color = image.color.SetAlpha(opacity);
        }
        _text.color = _text.color.SetAlpha(opacity);
    }
}
