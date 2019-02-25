using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Common3D {

    /// <summary>
    /// 基本的な移動WASDで移動Ｗはオブジェクトの前方に移動
    /// </summary>
    /// <param name="rigid">動かしたいオブジェクトのrigidbodyを入れる</param>
    /// <param name="MoveSpeed">移動スピードを入れる</param>
    /// <param name="transform">動かしたいオブジェクトのtransformを入れる</param>
    static public void MoveBasic(Rigidbody rigid, float MoveSpeed,Transform transform)
    {
        float x, z;
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        rigid.velocity = 
            transform.rotation.normalized * 
            new Vector3(MoveSpeed * x, rigid.velocity.y, MoveSpeed * z);
    }

    /// <summary>
    /// ジャンプする
    /// </summary>
    /// <param name="rigid"></param>
    /// <param name="JumpForce"></param>
    static public void Jump(Rigidbody rigid, float JumpForce) {
        rigid.AddForce(Vector3.up * JumpForce);
    }

    /// <summary>
    /// マウスドラッグでY軸回転を可能にする 
    /// 引数のstartpos,endposは他の軸移動系のメソッドでフィールド上に作成されていたらそれを使ってよい
    /// </summary>
    /// <param name="transform">自身のtransform</param>
    /// <param name="startpos">フィールドに作成しておく　引数を渡すときはref startposで渡すこと</param>
    /// <param name="endpos">フィールドに作成しておく</param>
    /// <param name="endrotation">フィールドに作成しておく　引数を渡すときはref endrotationで渡すこと</param>
    /// <param name="minrotation">フィールドに作成しておく</param>
    /// <param name="maxrotation">フィールドに作成しておく</param>
    /// <param name="lookspeed">フィールドに作成しておく</param>
    static public void MouseTurnY(Transform transform,ref Vector2 startpos,Vector2 endpos,ref float endrotation,
                                  float minrotation, float maxrotation ,float lookspeed) {

        if (Input.GetMouseButtonDown(0))
        {
            startpos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            endpos = Input.mousePosition;
            if (minrotation == 0 && maxrotation == 0)
            {
                float math = endrotation - (startpos.x - endpos.x) * lookspeed;
                transform.localEulerAngles = new Vector3(0, math, 0);  
            }
            else
            {
                float math = 0;
                if (endrotation > 180)
                {
                    math = Mathf.Clamp((endrotation-360) - (startpos.x - endpos.x) * lookspeed, minrotation, maxrotation);
                }
                else
                {
                    math = Mathf.Clamp(endrotation - (startpos.x - endpos.x) * lookspeed, minrotation, maxrotation);
                }
                transform.localEulerAngles = new Vector3(0, math, 0);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            endrotation = transform.localEulerAngles.y; //自身の角度を取得する際はコレで取得する
        }
    }

}
