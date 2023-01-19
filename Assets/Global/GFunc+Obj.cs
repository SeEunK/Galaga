using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public static partial class GFunc
{
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();

        GameObject targetObj_ = default;

        foreach (GameObject rootObj in rootObjs_){
            if(rootObj.name.Equals(objName_)){
                targetObj_ = rootObj;
                return targetObj_;
            }
            else{continue;}
        }
        return targetObj_;

    }
    //!특정 오브젝트의 자식 오브젝트를 서치해서 찾아주는 함수
    public static GameObject FindChildObj(this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;
        for(int i = 0; i< targetObj_.transform.childCount; i++)
        {

            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if(searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                searchResult = FindChildObj(searchTarget,objName_);
            }
        }
        return searchResult;
    }

    public static GameObject GetChildObj(this GameObject targetObj_ , string objName_)
    {
        GameObject searchResult = default;
        for(int i = 0; i < targetObj_.transform.childCount; i++){
            if(targetObj_.transform.GetChild(i).gameObject.name.Equals(objName_)){
                searchResult =targetObj_.transform.GetChild(i).gameObject;
                return searchResult;
            }
            else { continue;}
        }
        return searchResult;

    }

    public static Scene GetActiveScene(){
        Scene activeScene_ = SceneManager.GetActiveScene();
        return activeScene_;
    }
}