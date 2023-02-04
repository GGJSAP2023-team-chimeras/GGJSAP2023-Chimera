using System.Collections;
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
        //頭部の種類
        public enum HeadType
        {
            Kirin,    //麒麟
            Kada,     //化蛇
            Baku,     //獏
            None
        }
        //身体の種類
        public enum BodyType
        {
            Kirin,    //麒麟
            Kada,     //化蛇
            Baku,     //獏
            None
        }
        //脚部の種類
        public enum FootType
        {
            Kirin,    //麒麟
            Kada,     //化蛇
            Baku,     //獏
            None
        }        
    }
}