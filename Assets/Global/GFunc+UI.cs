using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public static partial class GFunc
{
    //! 텍스트 메쉬 프로형태의 컴포넌트의 텍스트를 설정하는 함수
    public static void SetTmpText(GameObject obj_ ,  string text_){
        TMP_Text tmpTxt = obj_.GetComponent<TMP_Text>();
        if(tmpTxt == null ||tmpTxt == default(TMP_Text)) {
            return;
         } //if : 가져올 텍스트 메쉬 컴포넌트가 없는 경우

         tmpTxt.text = text_;

    }
}
