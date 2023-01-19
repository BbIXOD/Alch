using System.Collections.Generic;
using MyExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Globalization;

public class DrawBar : MonoBehaviour
{
    private GetObj _getObj;
    private Enemy _enemy;
    public GameObject bar, text;
    private List<GameObject> _bars = new List<GameObject>();
    private const float X = -60, Y = 30, Down = 20, TextX = 100;
    private float _y;
    private Image _image;
    private void Awake()
    {
        _getObj = Camera.main!.GetComponent<GetObj>();
    }
    
    private void Update()
    {
        _y = Y;
        foreach(var b in _bars) Destroy(b);
        if(!_getObj.Hit) return;
        _enemy = _getObj.Hit.transform.gameObject.GetComponent<Enemy>();
        if (_enemy)
        {
            CreateBar(_enemy.live, Color.magenta, 10);
            for (var state = 0; state < 3; state++)
            {
                var col = new Color((state + 1) % 3 % 2, state % 2, ((state + 1) % 3 + 1) % 3 % 2, 1);
                CreateBar(_enemy.states[state], col, 50);
            }

        }
    }

    private void CreateBar(float width, Color color, float mult)
    {
        _bars.Add(Instantiate(bar, new Vector3(X, _y, 0), new Quaternion(0f, 0f, 0f, 0f)));
        _bars[^1].transform.localScale =
            _bars[^1].transform.localScale.SetWidth(width / mult);
        _bars[^1].transform.SetParent(transform,  false);
        _image = _bars[^1].GetComponent<Image>();
        _image.color = color;
        _bars.Add(Instantiate(text, new Vector3(TextX, _y - 15, 0), new Quaternion(0f, 0f, 0f, 0f)));
        _bars[^1].transform.SetParent(transform,  false);
        _bars[^1].GetComponent<TextMeshProUGUI>().SetText(Convert.ToString(width, CultureInfo.InvariantCulture));
        _y -= Down;
    }
}
