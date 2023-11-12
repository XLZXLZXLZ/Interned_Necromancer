using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// ����һ��������
/// ��洢һЩ���õĹ��ܣ����Ծ�̬�����洢
/// </summary>
public class Tools : MonoBehaviour
{
    /// <summary>
    /// ��ȡ���λ�õ�����
    /// </summary>
    static public Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);

    /// <summary>
    /// ��ȡ�����
    /// </summary>
    static public GameObject player => GameObject.FindGameObjectWithTag("Player");


    /// <summary>
    /// ��ȡ�����ֵĲ㼶
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    static public int GetLayer(string name)
    {
        return 1 << LayerMask.NameToLayer(name);
    }

    public static T[] GetAllComponentsInChildren<T>(Transform transform) where T : Component
    {
        T[] results = transform.GetComponentsInChildren<T>();
        for (int i = 0; i < transform.childCount;i++)
            results = results.Concat(GetAllComponentsInChildren<T>(transform.GetChild(i))).ToArray();
        return results;

    }
}

