using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    // デフォルトのキャンディストック
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 10;
    public int score;

    // 現在のキャンディストック数
    public int candy = DefaultCandyAmount;
    // ストック回復までの残り秒数
    int counter;

    public void ConsumeCandy()
    {
        if (candy > 0) candy--;
    }

    public int GetCandyAmount()
    {
        return candy;
    }

    public void AddCandy(int amount)
    {
        candy += amount;
    }

    private void OnGUI()
    {
        GUI.color = Color.black;


        // スコアの表示
        string labelscore = "score : " + score;

        // キャンディのストック数表示
        string label = "candy : " + candy;

        // 回復カウントしている時だけ秒数表示
        if (counter > 0) label = label + " (" + counter + "s)";

        // ストックのラベル
        GUI.Label(new Rect(50, 50, 100, 30), label);

        GUI.matrix = Matrix4x4.Scale(Vector3.one * 2);
        // スコアのラベル
        GUI.Label(new Rect(50, 100, 100, 30), labelscore);
    }

    void Update()
    {
        // キャンディのストックがデフォルトより少なく
        // 回復カウントをしていない時にカウントをスタートする
        if(candy < DefaultCandyAmount && counter <= 0)
        {
            StartCoroutine(RecoverCandy());
        }
    }

    IEnumerator RecoverCandy()
    {
        counter = RecoverySeconds;

        // 1秒ずつカウントを進める
        while(counter > 0)
        {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }

        candy++;
    }

}
