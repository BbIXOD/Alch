using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pathfinder : MonoBehaviour
{
    private Vector2 _size, _directNew;
    public Vector2 direct = Vector2.up;
    private int _curDepth;
    private Vector2[] _directs = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private Enemy _owner;
    private void Awake()
    {
        _size = GetComponent<BoxCollider2D>().size / 2;

    }

    private void FixedUpdate()
    {
        _curDepth = 200;
        Step((Vector2)transform.position + direct, direct, 1);
        Shuffle();
        foreach (var v in _directs)
        {
            if (v == direct) continue;
            Step((Vector2)transform.position + v, v, 1);
        }

        direct = _directNew;
    }

    private void Step(Vector2 pos, Vector2 first, int depth)
    {
        if (depth == _curDepth) return;
        if (CheckDest(pos))
        {
            _directNew = first;
            _curDepth = depth;
            return;
        }
        if (CheckCon(pos)) return;
        var directs = new List<Vector2>(_directs);
        depth++;
        foreach (var v in directs)
        {
            Step(pos + v, first, depth);
        }
        
    }
    
    private bool CheckDest(Vector2 pos)
    {
        if (!CheckCon(pos)) return false;
        return (Physics2D.Raycast(pos, Vector2.zero).transform.name == "Player" ||
                Physics2D.Raycast(pos + new Vector2(_size.x, 0), Vector2.zero)
                    .transform.name == "Player" ||
                Physics2D.Raycast(pos - new Vector2(_size.x, 0), Vector2.zero)
                    .transform.name == "Player" ||
                Physics2D.Raycast(pos + new Vector2(0, _size.y), Vector2.zero)
                    .transform.name == "Player" ||
                Physics2D.Raycast(pos - new Vector2(0, _size.y), Vector2.zero)
                    .transform.name == "Player");
    }

    private bool CheckCon(Vector2 pos)
    {
        return (!Physics2D.Raycast(pos, Vector2.zero) &&
                !Physics2D.Raycast(pos + new Vector2(_size.x, 0), Vector2.zero) &&
                !Physics2D.Raycast(pos - new Vector2(_size.x, 0), Vector2.zero) &&
                !Physics2D.Raycast(pos + new Vector2(0, _size.y), Vector2.zero) &&
                !Physics2D.Raycast(pos - new Vector2(0, _size.y), Vector2.zero));
    }

    private void Shuffle()
    {
        var l = _directs.Select(_ => _directs[Random.Range(0, _directs.Length)]).ToArray();
        _directs = l;
    }
}
