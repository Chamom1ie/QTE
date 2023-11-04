using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomHierarchy : MonoBehaviour
{
#if UNITY_EDITOR //Unity Editor에서만 돌아가야 함
    private static Dictionary<Object, CustomHierarchy> coloredObject = new Dictionary<Object, CustomHierarchy>();

    static CustomHierarchy() //스크립트 로드 시 한 번만 실행
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleDraw;
    }

    private static void HandleDraw(int instanceID, Rect selectionRect)
    {
        Object obj = EditorUtility.InstanceIDToObject(instanceID); //Key, instanceID를 참조할 수 있는 obj 생성

        if (obj != null && coloredObject.ContainsKey(obj)) //null 체크, coloredObject에 obj가 들어있는지 확인
        {
            GameObject gameObj = obj as GameObject; // 해당 Key 값인 Object의 GameObject 저장
            if (gameObj.TryGetComponent<CustomHierarchy>(out CustomHierarchy ch))
            {
                PaintObject(obj, selectionRect, ch);
            }
            else //obj가 CustomHierarchy를 가지고 있지 않음
            {
                coloredObject.Remove(obj);
            }
        }
    }

    private static void PaintObject(Object obj, Rect selectionRect, CustomHierarchy ch)
    {
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height); //Prefix를 넣기 위해 selectionRect.width + 50

        if (Selection.activeObject != obj) // obj가 클릭되어 있지 않다면
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

    //커스텀 할 것들
    public string prefix;
    public string objName;
    public Color backColor;
    public Color fontColor;

    //에디터 함수
    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        if (coloredObject.ContainsKey(this.gameObject) == false) //coloredObject에 없으면
        {
            coloredObject.Add(this.gameObject, this); //추가
        }
    }
#endif
}