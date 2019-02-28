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
    /// 物理エンジンにおけるジャンプ　上方向に力を加える
    /// </summary>
    /// <param name="rigid"></param>
    /// <param name="JumpForce"></param>
    static public void Jump(Rigidbody rigid, float JumpForce) {
        rigid.AddForce(Vector3.up * JumpForce);
    }

    /// <summary>
    /// 上下方向のマウスドラッグで軸回転を可能にする 
    /// 引数のstartpos,endposは他の軸回転系のメソッドでフィールド上に作成されていたらそれを使ってよい
    /// minrotation,maxrotationは制限角度。両方ゼロなら制限なしで動ける
    /// 上方向が+の角度回転
    /// </summary>
    /// <param name="gameObject">回転させたいオブジェクト　自身の場合はthis.gameObject</param>
    /// <param name="startpos">フィールドを作成しておく　引数を渡すときはref startposで渡すこと</param>
    /// <param name="endpos">フィールドを作成しておく</param>
    /// <param name="endrotation">フィールドを作成しておく　引数を渡すときはref endrotationで渡すこと</param>
    /// <param name="minrotation">フィールドを作成しておく 最小値は0～-180で記載</param>
    /// <param name="maxrotation">フィールドを作成しておく 最大値は0～180で記載</param>
    /// <param name="lookspeed">フィールドを作成しておく</param>
    /// <param name="axis">どこを軸で回転させるか「x,y,z」で記述</param>
    /// <param name="mirror">反転処理</param>
    static public void MouseTurnY(GameObject gameObject, ref Vector2 startpos, Vector2 endpos, ref float endrotation,
                                 float minrotation, float maxrotation, float lookspeed ,string axis, bool mirror)
    {

        if (Input.GetMouseButtonDown(0))
        {
            startpos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            endpos = Input.mousePosition;

            #region 回転処理 mirrorがfalseが平常処理
            if (!mirror)
            {
                if (minrotation == 0 && maxrotation == 0)
                {
                    float math = endrotation - (startpos.y - endpos.y) * lookspeed;
                    switch (axis)
                    {
                        case "x":
                            gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                            break;

                        case "y":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
                            break;

                        case "z":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    float math = 0;
                    if (endrotation > 180)
                    {
                        math = Mathf.Clamp((endrotation - 360) - (startpos.y - endpos.y) * lookspeed, minrotation, maxrotation);
                    }
                    else
                    {
                        math = Mathf.Clamp(endrotation - (startpos.y - endpos.y) * lookspeed, minrotation, maxrotation);
                    }

                    switch (axis)
                    {
                        case "x":
                            gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                            break;

                        case "y":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
                            break;

                        case "z":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
                            break;

                        default:
                            break;
                    }
                    
                }
            }
            else
            {
                if (minrotation == 0 && maxrotation == 0)
                {
                    float math = endrotation + (startpos.y - endpos.y) * lookspeed;
                    switch (axis)
                    {
                        case "x":
                            gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                            break;

                        case "y":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
                            break;

                        case "z":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    float math = 0;
                    if (endrotation > 180)
                    {
                        math = Mathf.Clamp((endrotation - 360) + (startpos.y - endpos.y) * lookspeed, minrotation, maxrotation);
                    }
                    else
                    {
                        math = Mathf.Clamp(endrotation + (startpos.y - endpos.y) * lookspeed, minrotation, maxrotation);
                    }
                    switch (axis)
                    {
                        case "x":
                            gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                            break;

                        case "y":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
                            break;

                        case "z":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
                            break;

                        default:
                            break;
                    }
                }
            }
           
            #endregion

        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (axis)
            {
                case "x":
                    endrotation = gameObject.transform.localEulerAngles.x;
                    break;

                case "y":
                    endrotation = gameObject.transform.localEulerAngles.y;
                    break;

                case "z":
                    endrotation = gameObject.transform.localEulerAngles.z;
                    break;

                default:
                    break;
            }
            
        }
    }


    /// <summary>
    /// 左右方向のマウスドラッグで軸回転を可能にする 
    /// 引数のstartpos,endposは他の軸回転系のメソッドでフィールド上に作成されていたらそれを使ってよい
    /// minrotation,maxrotationは制限角度。両方ゼロなら制限なしで動ける
    /// 右方向が+の角度回転
    /// </summary>
    /// <param name="gameObject">回転させたいオブジェクト　自身の場合はthis.gameObject</param>
    /// <param name="startpos">フィールドを作成しておく　引数を渡すときはref startposで渡すこと</param>
    /// <param name="endpos">フィールドを作成しておく</param>
    /// <param name="endrotation">フィールドを作成しておく　引数を渡すときはref endrotationで渡すこと</param>
    /// <param name="minrotation">フィールドを作成しておく 最小値は0～-180で記載</param>
    /// <param name="maxrotation">フィールドを作成しておく 最大値は0～180で記載</param>
    /// <param name="lookspeed">フィールドを作成しておく</param>
    ///  <param name="axis">どこを軸で回転させるか「x,y,z」で記述</param>
    /// <param name="mirror">反転処理</param>
    static public void MouseTurnX(GameObject gameObject,ref Vector2 startpos,Vector2 endpos,ref float endrotation,
                                  float minrotation, float maxrotation ,float lookspeed,string axis,bool mirror) {

        if (Input.GetMouseButtonDown(0))
        {
            startpos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            endpos = Input.mousePosition;

            #region 回転処理　mirrorはfalseが平常処理
            if (!mirror)
            {
                if (minrotation == 0 && maxrotation == 0)
                {
                    float math = endrotation - (startpos.x - endpos.x) * lookspeed;
                    switch (axis)
                    {
                        case "x":
                            gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                            break;

                        case "y":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
                            break;

                        case "z":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    float math = 0;
                    if (endrotation > 180)
                    {
                        math = Mathf.Clamp((endrotation - 360) - (startpos.x - endpos.x) * lookspeed, minrotation, maxrotation);
                    }
                    else
                    {
                        math = Mathf.Clamp(endrotation - (startpos.x - endpos.x) * lookspeed, minrotation, maxrotation);
                    }
                    switch (axis)
                    {
                        case "x":
                            gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                            break;

                        case "y":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
                            break;

                        case "z":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
                            break;

                        default:
                            break;
                    }
                }
            }
            else
            {
                if (minrotation == 0 && maxrotation == 0)
                {
                    float math = endrotation + (startpos.x - endpos.x) * lookspeed;
                    switch (axis)
                    {
                        case "x":
                            gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                            break;

                        case "y":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
                            break;

                        case "z":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    float math = 0;
                    if (endrotation > 180)
                    {
                        math = Mathf.Clamp((endrotation - 360) + (startpos.x - endpos.x) * lookspeed, minrotation, maxrotation);
                    }
                    else
                    {
                        math = Mathf.Clamp(endrotation + (startpos.x - endpos.x) * lookspeed, minrotation, maxrotation);
                    }
                    switch (axis)
                    {
                        case "x":
                            gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
                            break;

                        case "y":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
                            break;

                        case "z":
                            gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
                            break;

                        default:
                            break;
                    }
                }
            }
            #endregion
            
        }

        if (Input.GetMouseButtonUp(0))
        {
            switch (axis)
            {
                case "x":
                    endrotation = gameObject.transform.localEulerAngles.x;
                    break;

                case "y":
                    endrotation = gameObject.transform.localEulerAngles.y;
                    break;

                case "z":
                    endrotation = gameObject.transform.localEulerAngles.z;
                    break;

                default:
                    break;
            }
        }
    }

    //static public void MouseTurnMix(GameObject gameObject, ref Vector2 startpos, Vector2 endpos, ref float endrotationX, ref float endrotationY, 
    //                          float minrotationX,float minrotationY, float maxrotationX, float maxrotationY, float lookspeed, 
    //                          string axisX, string axisY, bool mirrorX, bool mirrorY)
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        startpos = Input.mousePosition;
    //    }

    //    if (Input.GetMouseButton(0))
    //    {
    //        endpos = Input.mousePosition;

    //        #region X座標回転処理　mirrorはfalseが平常処理
    //        if (!mirrorX)
    //        {
    //            if (minrotationX == 0 && maxrotationX == 0)
    //            {
    //                float math = endrotationX - (startpos.x - endpos.x) * lookspeed;
    //                switch (axisX)
    //                {
    //                    case "x":
    //                        gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
    //                        break;

    //                    case "y":
    //                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
    //                        break;

    //                    case "z":
    //                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //            else
    //            {
    //                float math = 0;
    //                if (endrotationX > 180)
    //                {
    //                    math = Mathf.Clamp((endrotationX - 360) - (startpos.x - endpos.x) * lookspeed, minrotationX, maxrotationX);
    //                }
    //                else
    //                {
    //                    math = Mathf.Clamp(endrotationX - (startpos.x - endpos.x) * lookspeed, minrotationX, maxrotationX);
    //                }
    //                switch (axisX)
    //                {
    //                    case "x":
    //                        gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
    //                        break;

    //                    case "y":
    //                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
    //                        break;

    //                    case "z":
    //                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (minrotationX == 0 && maxrotationX == 0)
    //            {
    //                float math = endrotationX + (startpos.x - endpos.x) * lookspeed;
    //                switch (axisX)
    //                {
    //                    case "x":
    //                        gameObject.transform.localEulerAngles = new Vector3(math, 0, 0);
    //                        break;

    //                    case "y":
    //                        gameObject.transform.localEulerAngles = new Vector3(0, math, 0);
    //                        break;

    //                    case "z":
    //                        gameObject.transform.localEulerAngles = new Vector3(0, 0, math);
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //            else
    //            {
    //                float math = 0;
    //                if (endrotationX > 180)
    //                {
    //                    math = Mathf.Clamp((endrotationX - 360) + (startpos.x - endpos.x) * lookspeed, minrotationX, maxrotationX);
    //                }
    //                else
    //                {
    //                    math = Mathf.Clamp(endrotationX + (startpos.x - endpos.x) * lookspeed, minrotationX, maxrotationX);
    //                }
    //                switch (axisX)
    //                {
    //                    case "x":
    //                        gameObject.transform.localEulerAngles = new Vector3(math, 0, 0);
    //                        break;

    //                    case "y":
    //                        gameObject.transform.localEulerAngles = new Vector3(0, math, 0);
    //                        break;

    //                    case "z":
    //                        gameObject.transform.localEulerAngles = new Vector3(0, 0, math);
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //        }
    //        #endregion
    //        #region Y座標回転処理　mirrorはfalseが平常処理
    //        if (!mirrorY)
    //        {
    //            if (minrotationY == 0 && maxrotationY == 0)
    //            {
    //                float math = endrotationY - (startpos.y - endpos.y) * lookspeed;
    //                switch (axisY)
    //                {
    //                    case "x":
    //                        gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
    //                        break;

    //                    case "y":
    //                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
    //                        break;

    //                    case "z":
    //                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //            else
    //            {
    //                float math = 0;
    //                if (endrotationY > 180)
    //                {
    //                    math = Mathf.Clamp((endrotationY - 360) - (startpos.y - endpos.y) * lookspeed, minrotationY, maxrotationY);
    //                }
    //                else
    //                {
    //                    math = Mathf.Clamp(endrotationY - (startpos.y - endpos.y) * lookspeed, minrotationY, maxrotationY);
    //                }
    //                switch (axisY)
    //                {
    //                    case "x":
    //                        gameObject.transform.localEulerAngles = new Vector3(math, gameObject.transform.localEulerAngles.y, gameObject.transform.localEulerAngles.z);
    //                        break;

    //                    case "y":
    //                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, math, gameObject.transform.localEulerAngles.z);
    //                        break;

    //                    case "z":
    //                        gameObject.transform.localEulerAngles = new Vector3(gameObject.transform.localEulerAngles.x, gameObject.transform.localEulerAngles.y, math);
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //        }
    //        else
    //        {
    //            if (minrotationY == 0 && maxrotationY == 0)
    //            {
    //                float math = endrotationY + (startpos.y - endpos.y) * lookspeed;
    //                switch (axisY)
    //                {
    //                    case "x":
    //                        gameObject.transform.localEulerAngles = new Vector3(math, 0, 0);
    //                        break;

    //                    case "y":
    //                        gameObject.transform.localEulerAngles = new Vector3(0, math, 0);
    //                        break;

    //                    case "z":
    //                        gameObject.transform.localEulerAngles = new Vector3(0, 0, math);
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //            else
    //            {
    //                float math = 0;
    //                if (endrotationY > 180)
    //                {
    //                    math = Mathf.Clamp((endrotationY - 360) + (startpos.y - endpos.y) * lookspeed, minrotationY, maxrotationY);
    //                }
    //                else
    //                {
    //                    math = Mathf.Clamp(endrotationY + (startpos.y - endpos.y) * lookspeed, minrotationY, maxrotationY);
    //                }
    //                switch (axisY)
    //                {
    //                    case "x":
    //                        gameObject.transform.localEulerAngles = new Vector3(math, 0, 0);
    //                        break;

    //                    case "y":
    //                        gameObject.transform.localEulerAngles = new Vector3(0, math, 0);
    //                        break;

    //                    case "z":
    //                        gameObject.transform.localEulerAngles = new Vector3(0, 0, math);
    //                        break;

    //                    default:
    //                        break;
    //                }
    //            }
    //        }
    //        #endregion
    //    }

    //    if (Input.GetMouseButtonUp(0))
    //    {
    //        switch (axisX)
    //        {
    //            case "x":
    //                endrotationX = gameObject.transform.localEulerAngles.x;
    //                break;

    //            case "y":
    //                endrotationX = gameObject.transform.localEulerAngles.y;
    //                break;

    //            case "z":
    //                endrotationX = gameObject.transform.localEulerAngles.z;
    //                break;

    //            default:
    //                break;
    //        }

    //        switch (axisY)
    //        {
    //            case "x":
    //                endrotationY = gameObject.transform.localEulerAngles.x;
    //                break;

    //            case "y":
    //                endrotationY = gameObject.transform.localEulerAngles.y;
    //                break;

    //            case "z":
    //                endrotationY = gameObject.transform.localEulerAngles.z;
    //                break;

    //            default:
    //                break;
    //        }
    //    }
    //}



}
