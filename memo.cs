
#region 親子関係のオブジェクト取得
      //親オブジェクトを取得
        _parent = transform.root.gameObject;

        Debug.Log ("Parent:" + _parent.name);
 
        //子オブジェクトを取得
        _child = transform.FindChild ("Child").gameObject;

        Debug.Log("Child is:" + _child.name);

#endregion

#region  カメラコンポーネントの取得
     Camera cam;
 
    void Start () {
        cam = Camera.main;
    }

    //オブジェクトの取得
     GameObject mainCamObj;
 
    void Start () {
        mainCamObj = Camera.main.gameObject;
    }

#endregion

#region transform上のプロパティrotationは特殊

transform.rotation = new Vector3(x, y, z); //これは間違い

transform.rotation = new Quaternion(x, y, z, w); //これが正しい

//単純に値を代入して角度を変更したい
transform.rotation = Quaternion.Euler(0,0,0);
#endregion

#region  Resourcesフォルダからオブジェクトを読み込む

概要
各シーンで使用する素材(SEやBGMやスプライトなど)を○○Managerなどにアタッチしておき、
あらかじめヒエラルキー上に配置しておくとメモリの消費が激しい。
メモリが多いPCなどでアプリを実行する場合はあまり気にする必要性はないが
スマートフォン上だとメモリが足りなくなり、アプリがクラッシュする問題が発生する。
そのため、Resourcesなどを使用して該当の素材を使用する時のみ読み込むようにした方がよい。
また使用しなくなった素材についてはメモリ開放を行い快適なアプリ環境を保つ必要性がある。

Resourcesからデータを読み込む
sample.cs
//スプライトの読み込み
private Sprite buttn;
buttn = Resources.Load("Image/StageSelect/OrangeButton", typeof(Sprite)) as Sprite;

//BGM(SE)の読み込み
private AudioSource audioSource;
audioSource = gameObject.AddComponent<AudioSource>();
audioSource.clip = Resources.Load("Sound/BGM/sample") as AudioClip;

//Prefabの読み込み
GameObject prefab;
prefab = (GameObject)Resources.Load ("Prefabs/sample");


メモリを開放する
シーンの切替時にもメモリ開放される(？)かもしれないけれど、とりあえず明示的にも記載しておく方がよい。

sample.cs
//現在使用していないアセットを破棄する
Resources.UnloadUnusedAssets();

#endregion

#region 現在のアニメーションの状態を取得する
bool isReady = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(Animator.StringToHash("Clipの名前"));

もしくは

bool isReady = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("レイヤー名.Clipの名前"）

前者はClipの名前だけでよくて、後者はレイヤーの名前も必要となる。

#endregion

#region float値が同じかどうか判別する
if(Mathf.Approximately(test, (10f / 100f)) {
	Debug.Log("testは10/100と同値");
}
#endregion

#region 　目的地までの移動
■目的地まで等速で移動
transform.position = Vector3.MoveTowards(transform.position, targetpos, Time.deltaTime);　(現在地、目的地、移動速度)　

■目的地まで指定の時間で移動
transform.position = Vector3.Lerp(a,b,0～1の値);　aを0、bを1として中間点を出す　time.deltaTimeを指定時間をで割ればいい感じになるんじゃない？

共に毎フレーム処理
#endregion

#region オブジェクトの向きを目的の位置に向ける、歩いている方向に向ける

■目的の位置に向ける　
transform.rotation = Quaternion.LookAt(Vector3)

■歩く方向に向ける
transform.rotation = Quaternion.LookRotation(自分の位置(transform.position) +　
(vector3.right * input.getaxsis("Horizontal")+
(vector3.foward * input.getaxsis(vertical))-
transform.position));                           //自分の向きたい方向　-　自分の位置を計算

//input.getaxisの入力系が押されている間のみ判定したい場合は　Input.GetButton("Horizontal")　という形で表記できる

transform.rotation = Quaternion.LookRotation(new Vector3( Vector3.right.x * x, 0, Vector3.forward.z * z));　//コレでもいけた

#endregion

#region 生成したオブジェクトをリストの中に加える
list.Add(Instantiate(pin, pinpos, Quaternion.identity));

addのメソッドの中にInstantiateメソッドでオブジェクトを作成すると
その生成されたオブジェクトがリストに加わる

#endregion

#region 自身のオブジェクトの角度を取得する また　キャラの方向転換を実装する
    float x = transform.localEulerAngles.y
    float y = transform.rotation.y  //コレだと0～1間での範囲で値が保存される上に沸けわからん計算をされた値が入る
                            
     //任意の角度を入力するなりマウスドラッグでの方向転換を実装できる
     //0~360度で管理される　-1度は359度で管理される 
     //マイナスの値を入力した場合は一応その値の角度に設定される                      
     transform.localEulerAngles = new Vector3(0,
                                               endrotation -
                                               (startpos.x - endpos.x) * lookspeed,
                                               0);

    上記方法で角度を取得しようとすると45～-45の角度の制限をかけようとするとかなり大変になる
    だから一度別の変数に代入する値を保存してから代入する

    float setrotation = Mathf.Clamp(neckendrotation + (startpos.x - endpos.x) * lookspeed, -45, 45);
    transform.localEulerAngles = new Vector3(0, setrotation,0);
    コレなら簡単にマイナスになってもしっかり反映される
#endregion 

#region　アセットモデルを動かそうとしたが動かなかった
■アセットモデル Unity mask manに　rigidbody コライダ　をアタッチしてスクリプトを組んでも動かなかった
・アニメーターのコンポーネントをとめると動くようになった
・アニメーターのルートモーションの適用の項目からチェックをはずすと動くようになった
ルートモーションのせいで座標が固定され動けなかった可能性がビレゾンちえみ
#endregion

#region ドラッグドロップの制御
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

                                        //ondrag onbegindrag, onenddragを使うために継承しなければならない
public class dragdrop : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler  {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(PointerEventData evendata) {//候補が出ないので直打ち
        Debug.Log("start");
	startpos = Camera.main.WorldToScreenPoint(transform.position); 
	//transform.positionはワールド座標のため一旦スクリーン座標に変換する
	//スクリーン座標に変換していないワールド座標は解像度と一致した座標ではなく初期位置の座標となるためこの作業が必要
    }

    public void OnDrag(PointerEventData evendata)
    {
        Debug.Log("ing");
	Vector3 mouseposition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        this.transform.position = Camera.main.ScreenToWorldPoint(mouseposition);
    }

    public void OnEndDrag(PointerEventData evendata) {
        Debug.Log("end");
	transform.position = Camera.main.ScreenToWorldPoint(startpos);　//初期位置のポジションに戻すためにスクリーン座標をワールド座標に変換している
    }



}

#endregion

#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
#region 
#endregion
