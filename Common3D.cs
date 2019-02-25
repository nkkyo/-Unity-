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



   

}
