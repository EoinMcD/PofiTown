using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


namespace Utilities {


    public static class DSStyleUtitlity {
        public static VisualElement AddStyleSheets(this VisualElement element, params string[] styleSheetNames) {
            foreach (string styleSheetItem in styleSheetNames) {
                StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load(styleSheetItem);

                element.styleSheets.Add(styleSheet);


            }

            return element;
        }

        public static VisualElement AddClasses(this VisualElement element, params string[] classNames) {
            foreach (string className in classNames) {
                element.AddToClassList(className);
            }

            return element;
        }

    }
}
