using UnityEngine;

public class GetObj : MonoBehaviour
{
    private Vector2 _ray;
    public RaycastHit2D Hit;
    public string curObj;
    private Camera _camera;
    private int _layers;

    protected void Awake()
    {
        const int l = 1 << 8;
        _layers = ~l;
        _camera = Camera.main;
    }

    protected void Update()
    {
        _ray = _camera.ScreenToWorldPoint(Input.mousePosition);
        Hit = Physics2D.Raycast(_ray, Vector2.zero, Mathf.Infinity, _layers);
        if (!Hit)
        {
            curObj = null;
            return;
        }

        curObj = MyExtensions.MyExtensions.ToTag(Hit.transform.name);

    }
}