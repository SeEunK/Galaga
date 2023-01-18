using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    private GameObject poolingObjPrefab;

    private GameObject[] bullets;
    private int currIndex = 0;
    private Vector3 poolPosition= new Vector3(0, 0, -100);
    private float lastSpawnTime;
    private float timeBetSpawn;
    public float timeBetSpawnMin = 0.5f;
    public float timeBetSpawnMax = 3.0f;


//    void Start()
//    {
//     bullets = new GameObject[10];

//     for(int i = 0; i< 10; i++)
//     {
//         bullets[i]=Instantiate(poolingObjPrefab,poolPosition,Quaternion.identity);
//     }
//         lastSpawnTime = 0f;
//         timeBetSpawn = 0f;
//    }

//    void Update(){
//     if(Time.time >= lastSpawnTime +timeBetSpawn){
//         lastSpawnTime = Time.time;
//         timeBetSpawn = Random.Range()
//     }
//     else{

//     }
//    }
   // private void Awake()
   // {
   //     Instance = this;
   //     Initialize(10);
   //     
   // }
   // private void Initialize(int initCount)
   // {
   //     for(int i = 0; i < initCount; i++){
   //         poolingObjQueue.Enqueue(CreateNewObject());
//
   //     }
   // }

    //private Bullet CreateNewObject()
    //{
    //    Bullet newObj = Instantiate(poolingObjPrefab).GetComponent<Bullet>();
    //    newObj.gameObject.SetActive(false);
    //    newObj.transform.SetParent(transform);
    //    return newObj;
//
    //}
//
    //public static Bullet GetObject(){
    //    if(Instance.poolingObjQueue.Count > 0){
    //        Bullet obj = Instance.poolingObjQueue.Dequeue();
    //        obj.transform.SetParent(null);
    //        obj.gameObject.SetActive(true);
    //        return obj;
    //    }
    //    else{
    //        Bullet newObj = Instance.CreateNewObject();
    //        
    //        newObj.gameObject.SetActive(true);
    //        newObj.transform.SetParent(null);
    //        return newObj;
    //    }
    //}
//
    //public static void ReturnObject(Bullet obj){
    //    obj.gameObject.SetActive(false);
    //    obj.transform.SetParent(Instance.transform);
    //    Instance.poolingObjQueue.Enqueue(obj);
    //}

}
