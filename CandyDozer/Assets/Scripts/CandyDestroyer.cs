using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;
    public GameObject effectPrefab;     // エフェクトプレハブプロパティ
    public Vector3 effectRotation;      // エフェクトローテーションプロパティ


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Candy")
        {
            // 指定数だけCandyのストックを増やす
            candyManager.AddCandy(reward);

            // オブジェクト削除
            Destroy(other.gameObject);

            if (effectPrefab != null)  // エフェクトプレハブの設定チェック
            {
                // Candyのポジションにエフェクトを作成
                Instantiate(
                    effectPrefab,
                    other.transform.position,
                    Quaternion.Euler(effectRotation)
                    );

                // スコアを加点する
                candyManager.score += 10;
            }
        }
    }
}
