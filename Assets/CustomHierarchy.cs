using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomHierarchy : MonoBehaviour
{
#if UNITY_EDITOR //Unity Editor������ ���ư��� ��
    private static Dictionary<Object, CustomHierarchy> coloredObject = new Dictionary<Object, CustomHierarchy>();

    static CustomHierarchy() //��ũ��Ʈ �ε� �� �� ���� ����
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID); //Key, instanceID�� ������ �� �ִ� obj ����

        if (obj != null && coloredObject.ContainsKey(obj)) //null üũ, coloredObject�� obj�� ����ִ��� Ȯ��
        {
            GameObject gameObj = obj as GameObject; // �ش� Key ���� Object�� GameObject ����
            if (gameObj.TryGetComponent<CustomHierarchy>(out CustomHierarchy ch))
            {
                PaintObject(obj, selectionRect, ch);
            }
            else //obj�� CustomHierarchy�� ������ ���� ����
            {
                coloredObject.Remove(obj);
            }
        }
    }

    private static void PaintObject(Object obj, Rect selectionRect, CustomHierarchy ch)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height); //Prefix�� �ֱ� ���� selectionRect.width + 50

        if (Selection.activeObject != obj) // obj�� Ŭ���Ǿ� ���� �ʴٸ�
        {
            EditorGUI.DrawRect(bgRect, ch.backColor);
            string name = $"{ch.prefix} {obj.name}";
            if (ch.objName != "") name = $"{ch.prefix} {ch.objName}";

            EditorGUI.LabelField(bgRect, name, new GUIStyle()
            {
                normal = new GUIStyleState { textColor = ch.fontColor },
                fontStyle = FontStyle.Bold
            });
        }
    }

    //Ŀ���� �� �͵�
    public string prefix;
    public string objName;
    public Color backColor;
    public Color fontColor;

    //������ �Լ�
    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (coloredObject.ContainsKey(this.gameObject) == false) //coloredObject�� ������
        {
            coloredObject.Add(this.gameObject, this); //�߰�
        }
    }
#endif
}