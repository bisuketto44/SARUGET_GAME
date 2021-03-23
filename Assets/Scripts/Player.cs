using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private float _inputX;
    private float _inputY;
    private Vector3 _characterVec;

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

    }

    void Update()
    {
        Tps_Character_Movemant();
    }

    void Tps_Character_Movemant()
    {
        _inputX = Input.GetAxisRaw("Horizontal");
        _inputY = Input.GetAxisRaw("Vertical");


        //カメラの前方向ベクトルから、移動に必要なXとZ軸を決定。
        //Scale() = 同じ要素同士を掛ける。y = 0にしてあるのは進行方向の決定に上下のベクトルを入れないため。それを正規化する。
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //Vector同士の足し算 = カメラの前方向へキャラWSキー移動と、カメラの左右移動のベクトルに合わせたADキー移動
        Vector3 moveForward = (cameraForward * _inputY) + (Camera.main.transform.right * _inputX);

        rb.velocity = moveForward * 10;// + new Vector3(0, rb.velocity.y, 0);ジャンプや落下が必要な場合追記

        //この記述がないと顔の位置がカメラの方向を向かない
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }

    }
}
