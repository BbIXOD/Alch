using System.Text.RegularExpressions;
using UnityEngine;

public class GetObj : MonoBehaviour
{
    private Vector2 _ray;
    public RaycastHit2D Hit;
    public string curObj;
    private Camera _camera;

    protected void Awake()
    {
        _camera = Camera.main;
    }

    protected void Update()
    {
        _ray = _camera.ScreenToWorldPoint(Input.mousePosition);
        Hit = Physics2D.Raycast(_ray, Vector2.zero);
        if (!Hit)
        {
            curObj = null;
            return;
        } 
        curObj = Regex.Match(Hit.transform.name, "[a-zA-Z_]+")
            .Value
            .Replace("_", " ");
        
    }
}