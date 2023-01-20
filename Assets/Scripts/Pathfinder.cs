using System;
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
        _curDepth = 10;
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
        if (!CheckCon(pos)) return;
        Shuffle();
        depth++;
        foreach (var v in _directs)
        {
            var newPos = new Vector2(pos.x + v.x, pos.y + v.y);
            Step(newPos, first, depth);
        }

    }
    
    private bool CheckDest(Vector2 pos)
    {
        return CheckCol(pos) ||
               CheckCol(pos + new Vector2(_size.x, 0)) ||
               CheckCol(pos - new Vector2(_size.x, 0)) ||
               CheckCol(pos + new Vector2(0, _size.y)) ||
               CheckCol(pos - new Vector2(0, _size.y));

        bool CheckCol(Vector2 p)
        {
            try
            {
                return Physics2D.Raycast(p, Vector2.zero).transform.name == "Player";
            }
            catch (NullReferenceException) { return false; }
        }
    }

    private bool CheckCon(Vector2 pos)
    {
        return IsOther(pos) ||
                IsOther(pos + new Vector2(_size.x, 0)) &&
                IsOther(pos - new Vector2(_size.x, 0)) &&
                IsOther(pos + new Vector2(0, _size.y)) &&
                IsOther(pos - new Vector2(0, _size.y));

        bool IsOther(Vector2 p)
        {
            var hit = Physics2D.Raycast(p, Vector2.zero);
            return !hit || hit.transform.gameObject == gameObject;
        }
    }

    private void Shuffle()
    {
        var l = _directs.Select(_ => _directs[Random.Range(0, _directs.Length)]).ToArray();
        _directs = l;
    }
}
