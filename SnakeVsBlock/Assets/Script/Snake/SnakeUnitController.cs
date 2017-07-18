using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeUnitController : MonoBehaviour
{
    private const float distance = 36;

    public static float moveSpeed;
    private const string PREFAB_PATH = "Prefab/SnakeUnit";
    private SnakeUnitController _parent;
    private int _index;   
    private Vector3 _currentTarget;
    private Vector3 _lastTarget;
    private float _speedMagnitude;
    private bool _isStartFollow;
    private Queue<Vector3> _posQueue = new Queue<Vector3>();

    private float timeCount;

    public int index { get { return _index; } }
    private float _moveSpeedCount;
      
    public static SnakeUnitController Create(SnakeUnitController parent, int i)
    {
        GameObject go = Instantiate(Resources.Load(PREFAB_PATH)) as GameObject;
        SnakeUnitController script = go.GetComponent<SnakeUnitController>();
        script._parent = parent;
        script._index = i;
        return script;
    }

    public void MyUpdate()
    {
        if (_parent != null)
        {
            if (!_isStartFollow)
            {
                _moveSpeedCount += moveSpeed;
                if (Vector3.Distance(_parent.transform.localPosition, transform.localPosition) > distance)
                {
                    _Move(_moveSpeedCount - distance * index);
                    _isStartFollow = true;
                }
            }
            else
            {
                _Move();
            }
        }
        else
        {
            Debug.LogWarning("_parent:null" + index);
//            transform.localPosition = Vector3.zero;
            _posQueue.Enqueue(transform.localPosition);
        }
    }

    private void _Move(float speed = -1)
    {
        if (speed == -1)
            _speedMagnitude = moveSpeed;
        else
            _speedMagnitude = speed;

        _lastTarget = transform.localPosition;
        while (_speedMagnitude > 0)
        {
            _currentTarget = _parent._posQueue.Peek();
            Vector3 v = _currentTarget - _lastTarget;
            Vector3 unitV = v.normalized;
            float distance = v.magnitude;
            if (_speedMagnitude - distance <= 0)
            {
                transform.localPosition = _lastTarget + _speedMagnitude * unitV;
                if (_speedMagnitude == distance)
                    _posQueue.Enqueue(_parent._posQueue.Dequeue());
                else
                    _posQueue.Enqueue(transform.localPosition);
            }
            else
            {
                _lastTarget = _parent._posQueue.Dequeue();
                _posQueue.Enqueue(_lastTarget);
            }
            _speedMagnitude -= distance;
        }
    }

    void OnTriggerEnter2D(Collider2D cell)
    {
        if (cell.gameObject.name.Equals("WallUnit(Clone)"))
        {
            GGDebug.Log("---蛇：-----开始碰撞-------");
            GGDebug.Log(cell.gameObject.name);
            SnakeManage.GetInstance().DelSnakeUnit();
            Destroy(gameObject);
        }
        if (cell.gameObject.name.Equals("France1(Clone)") || cell.gameObject.name.Equals("France2(Clone)"))
        {
            LogicPartController.GetInstance().IsTriggerWall = true;
        }
    }

    void OnTriggerStay2D(Collider2D cell)
    {
//        if (cell.gameObject.name.Equals("France1(Clone)") || cell.gameObject.name.Equals("France2(Clone)"))
//        {
//            LogicPartController.GetInstance().IsTriggerWall = false;
//        }
    }

    void OnTriggerExit2D(Collider2D cell)
    {
        if (cell.gameObject.name.Equals("France1(Clone)") || cell.gameObject.name.Equals("France2(Clone)"))
        {
            LogicPartController.GetInstance().IsTriggerWall = false;
        }
    }

    private void Log(int index, string debug)
    {
        if (this.index == index)
            GGDebug.Log(debug);
    }
}
