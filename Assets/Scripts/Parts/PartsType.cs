﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BodyParts
{
    /// <summary>
    /// パーツの種類定義
    /// </summary>
    public class PartsType
    {
        //体の部位の型
        public enum BodyPartsType
        {
            Head,Body,Foot
        }
        //各部位の種類(もっと追加するかも)
        public enum EachPartsType
        {
            Kirin = 0,  //麒麟
            Kada,       //化蛇
            Baku,       //獏
            None
        }
    }
}