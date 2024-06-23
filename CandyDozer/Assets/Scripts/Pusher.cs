using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    Vector3 startPotion;

    public float amplitude;      // 移動量のプロパティ
    public float speed;          // 移動速度のプロパティ

    void Start()
    {
        startPotion = transform.localPosition;
    }

    void Update()
    {
        // 変位を計算
        float z = amplitude * Mathf.Sin(Time.time * speed);

        // zを変異させたポジションに再設定
        transform.localPosition = startPotion + new Vector3(0, 0, z);   // ポジションの反映
    }
}