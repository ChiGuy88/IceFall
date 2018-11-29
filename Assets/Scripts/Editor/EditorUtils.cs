using UnityEngine;
using UnityEditor;

namespace IceFalls.Editor {

    public static class EditorUtils {

        /// <summary>
        /// Add horizontal separators
        /// </summary>
        public static int Seperate {
            set {
                if (value > 0) {
                    int i, n = value;
                    for (i = 0; i < n; ++i) EditorGUILayout.Separator();
                }
            }
        }

        /// <summary>
        /// Add vertical separators
        /// </summary>
        public static int Margin {
            set {
                if (value > 0) GUILayout.Space(value);
            }
        }

        /// <summary>
        /// Add horizontal line
        /// </summary>
        public static int Line {
            set {
                if (value > 0) {
                    int i, n = value;
                    for (i = 0; i < n; ++i) EditorGUILayout.LabelField("", GUI.skin.horizontalScrollbar);
                }
            }
        }
    }
}