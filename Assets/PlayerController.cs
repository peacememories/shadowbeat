using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float TileSize = 1;
    public float Speed = 10;

    public float Beat = 1;
    public float HitWindow = 0.1f;

    Vector3 TargetPosition;
    float NextBeat;
    bool WasOnBeat = false;
    bool MovedThisBeat = false;

    // Use this for initialization
    void Start()
    {
        TargetPosition = transform.position;
        NextBeat = 0;
    }

    void Move(Vector3 direction)
    {
        if (!MovedThisBeat && OnBeat())
        {
            // We hit the beat
            TargetPosition += direction * TileSize;
        }
        else
        {
            // We didn't hit the beat
            Debug.Log("Try better next time");
        }
        MovedThisBeat = true;
    }

    bool OnBeat()
    {
        return NextBeat <= HitWindow || NextBeat >= (1 / Beat) - HitWindow;
    }

    // Update is called once per frame
    void Update()
    {
        NextBeat -= Time.deltaTime;
        if (OnBeat())
        {
            WasOnBeat = true;
        }
        else
        {
            if (WasOnBeat)
            {
                MovedThisBeat = false;
            }
            WasOnBeat = false;
        }
        if (NextBeat <= 0)
        {
            Debug.Log("Beat!");
            NextBeat += 1 / Beat;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(new Vector3(0, 1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(new Vector3(1, 0, 0));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(new Vector3(0, -1, 0));
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(new Vector3(-1, 0, 0));
        }

        transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.deltaTime * Speed);
    }
}
