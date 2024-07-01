using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;              // ショットパワーの回復時間定数

    int shotPower = MaxShotPower;
    AudioSource shotSound;

    public GameObject[] candyPrefabs;           // Candyプレハブプロパティ
    public Transform candyParentTransform;
    public CandyManager candyManager;
    public float shotForce;
    public float shotTorque;
    public float baseWidth;

    // Start
    private void Start()
    {
        shotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")) Shot();
    }

    // キャンディのプレハブからランダムに1つ選ぶ
    GameObject SampleCandy()
    {
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    // 発射位置の計算
    Vector3 GetInstantiatePotision()  
    {
        // 画面のサイズとInputの割合からキャンディの生成ポジションを計算
        float x = baseWidth *
            (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }

    public void Shot()
    {
        // キャンディを生成できる条件外ならばShotしない
        if (candyManager.GetCandyAmount() <= 0) return;

        // ショットパワーチェック
        if (shotPower <= 0) return;

        // プレハブからCandyオブジェクトを作成
        GameObject candy = (GameObject)Instantiate(
            SampleCandy(),
            GetInstantiatePotision(),
            Quaternion.identity
            );

        // 生成したCandyオブジェクトの親をcandyParentTransformmに設定する
        candy.transform.parent = candyParentTransform;

        // CandyオブジェクトのRigidBodyを取得し力と回転を加える
        Rigidbody candyRigidBody = candy.GetComponent<Rigidbody>();
        candyRigidBody.AddForce(transform.forward * shotForce);
        candyRigidBody.AddTorque(new Vector3(0, shotTorque, 0));

        // キャンディのストック消費
        candyManager.ConsumeCandy();
        // ShotPowerを消費
        ConsumePower();

        // サウンドを再生
        shotSound.Play();
    }

    /// <summary>
    /// ショットパワーの表示
    /// </summary>
    private void OnGUI()
    {
        GUI.color = Color.black;

        // ShotPowerのざんすうを+の数で表示
        string label = "";
        for (int i = 0; i < shotPower; i++) label = label + "+";

        GUI.Label(new Rect(50, 65, 100, 30), label);

    }

    void ConsumePower()
    {
        // ShotPowerを消費すると同時にカウントスタート
        shotPower--;
        StartCoroutine(RecoverPower());
    }

    IEnumerator RecoverPower()
    {
        // 一定時間待った後にshotPowerを回復
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }
}
