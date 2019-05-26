using System.Collections;
using System.Collections.Generic;
using UnityEngine;
class fanControl
{
    public static bool checkFan(Transform enemy,Transform player)
    {
        Transform fan = enemy.Find("check");
        Material mat = fan.GetComponent<Renderer>().sharedMaterial;
        float r = mat.GetFloat("_radius");
        float angle = mat.GetFloat("_clipangle");
        angle = (180f - angle * Mathf.PI * Mathf.Rad2Deg);
        Vector3 delta = (player.position - enemy.position);
        float cos = Vector3.Dot(delta.normalized, enemy.forward);
        float tangle = Mathf.Acos(cos)*Mathf.Rad2Deg;
        float clipr = r * fan.localScale.x * enemy.localScale.x / 2;
         //Debug.Log(tangle + " " +angle);
        if (tangle < angle && delta.magnitude < clipr)
            return true;
        return false;
    }
    //在需要有检测的物体下建立一个quad并命名为check，保证坐标系position为（0，0，0）
    //rotation为(90,90,0)并赋予shader MInigame/shape
    //根据自己需求改变检测形状，然后调用该方法进行判断即可。
}
