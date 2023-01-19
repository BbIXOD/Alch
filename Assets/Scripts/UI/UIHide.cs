using MyExtensions;
using UnityEngine;

public class UIHide : MonoBehaviour
{
    protected string Obj;
    private RectTransform _rect;
    private GetObj _getObj;
    private float _width;
    protected virtual void Awake()
    {
        _getObj = Camera.main!.GetComponent<GetObj>(); // what means this "!"?
        _rect = GetComponent<RectTransform>();
        _width = _rect.localScale.x;
    }

    protected virtual void Update()
    {
        Obj = _getObj.curObj;
        _rect.localScale = Obj == null ? _rect.localScale.SetWidth(0) :
            _rect.localScale.SetWidth(_width);
    }
}
