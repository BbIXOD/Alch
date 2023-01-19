using TMPro;

public class ShowName : UIHide
{
    private TextMeshProUGUI _text;
    protected override void Awake()
    {
        base.Awake();
        _text = GetComponent<TextMeshProUGUI>();
    }
    protected override void Update()
    {
        base.Update();
        if(Obj == null) return;
        _text.SetText(Obj);
    }
}
