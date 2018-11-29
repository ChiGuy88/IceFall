using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using CRYSTAL;
//using DG.Tweening;

namespace IceFalls.Editor {

    [CustomEditor(typeof(script_Tween))]
    public class editor_Tween : UnityEditor.Editor {

        private static readonly int MARGIN = 2;

        public override void OnInspectorGUI() {

            script_Tween script = (script_Tween)target;

            if (script == null) { return; }

            EditorGUILayout.BeginVertical();

            GUI.enabled = false;
            EditorGUILayout.ObjectField("Script:", MonoScript.FromMonoBehaviour(script), typeof(script_Tween), false);
            GUI.enabled = true;

            ETweenType preTweenType = script.TweenType;
            script.TweenType = (ETweenType)EditorGUILayout.EnumPopup("Tween Type", script.TweenType);
            if (preTweenType != script.TweenType) {
                script.ChangeTweenType();
            }

            if (script.TweenType != ETweenType.NONE) {

                EditorGUILayout.BeginHorizontal();
                EditorUtils.Seperate = 3;
                if (GUILayout.Button("Reset Tween") &&
                    EditorUtility.DisplayDialog("Confirm Reset", "Are you sure?", "Yes", "No")
                ) {
                    script.ChangeTweenType();
                }
                EditorGUILayout.EndHorizontal();

                // Render base tween fields
                this.RenderBaseTween();

                switch (script.TweenType) {
                    case ETweenType.Rect2D:
                        this.RenderRectTween(script);
                        break;
                    case ETweenType.TransformPosition:
                        this.RenderTransformPositionTween(script);
                        break;
                    case ETweenType.TransformRotation:
                        this.RenderTransformRotationTween(script);
                        break;
                    case ETweenType.TransformScale:
                        this.RenderTransformScaleTween(script);
                        break;
                    case ETweenType.CanvasGroupAlpha:
                        this.RenderCanvasGrouAlphaTween(script);
                        break;
                }

                if (GUI.changed && !EditorApplication.isPlaying) {
                    Undo.RecordObject(script, "Editor Modified script_Tween");
                    Undo.RecordObject(script.Tween, "Editor Modified Tween");
                    EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                    EditorUtility.SetDirty(script);
                    EditorUtility.SetDirty(script.Tween);
                }
            }

            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Render 'BASE' Tween Input
        /// </summary>
        /// <param name="_Script"></param>
        private void RenderBaseTween() {

            Tween tween = ((script_Tween)target).Tween;

            if (tween.GUID == string.Empty || tween.GUID == null) {
                return;
            }
            
            EditorUtils.Margin = MARGIN;
            EditorUtils.Line = 1;
            EditorUtils.Margin = MARGIN * 3;
            EditorGUILayout.LabelField("Base Tween Input", EditorStyles.centeredGreyMiniLabel);
            EditorUtils.Margin = MARGIN * 3;

            EditorUtils.Margin = MARGIN;
            EditorGUILayout.LabelField("GUID:", tween.GUID, EditorStyles.whiteLargeLabel);

            EditorUtils.Margin = MARGIN;
            EditorGUILayout.BeginHorizontal();
            tween.TweenDisplayName = EditorGUILayout.TextField(
                "Display Name:",
                tween.TweenDisplayName
            );
            EditorGUILayout.EndHorizontal();

            EditorUtils.Margin = MARGIN;
            EditorGUILayout.BeginHorizontal();
            tween.TweenStartDelayInSec = (float)Convert.ToDouble(
                EditorGUILayout.TextField(
                    "Start Delay (s):",
                    tween.TweenStartDelayInSec.ToString(),
                    EditorStyles.numberField
                )
            );
            EditorGUILayout.EndHorizontal();

            EditorUtils.Margin = MARGIN;
            EditorGUILayout.BeginHorizontal();
            tween.TweenTimeInSec = (float)Convert.ToDouble(
                EditorGUILayout.TextField(
                    "Time (s):",
                    tween.TweenTimeInSec.ToString(),
                    EditorStyles.numberField
                )
            );
            EditorGUILayout.EndHorizontal();

            EditorUtils.Margin = MARGIN;
            tween.ForcePlayTween = EditorGUILayout.Toggle("Play Tween:", tween.ForcePlayTween);
            
            EditorUtils.Margin = MARGIN;
            tween.ForceReplayTween = EditorGUILayout.Toggle("Replay Tween:", tween.ForceReplayTween);

            EditorUtils.Margin = MARGIN;
            tween.TweenEase = (DG.Tweening.Ease)EditorGUILayout.EnumPopup("Tween Type:", tween.TweenEase);
        }

        /// <summary>
        /// Render 'Rect2D' Tween Input
        /// </summary>
        /// <param name="_Script"></param>
        private void RenderRectTween(script_Tween _Script) {
            
            RectTween tween = _Script.GetTween<RectTween>();

            if (tween == null) {
                CONSOLE.Log("Tween Instance Value Is NULL");
                return;
            }

            EditorUtils.Margin = MARGIN*3;
            EditorUtils.Line = 1;
            EditorUtils.Margin = MARGIN*3;
            EditorGUILayout.LabelField("Rect Tween Input", EditorStyles.centeredGreyMiniLabel);
            EditorUtils.Margin = MARGIN * 3;

            EditorUtils.Margin = MARGIN;
            tween.StartRect = EditorGUILayout.RectField("Start Rect:", tween.StartRect);
            
            EditorUtils.Margin = MARGIN;
            tween.EndRect = EditorGUILayout.RectField("End Rect:", tween.EndRect);
        }

        /// <summary>
        /// Render 'TransformPosition' Tween Input
        /// </summary>
        /// <param name="_Script"></param>
        private void RenderTransformPositionTween(script_Tween _Script) {

            TransformPositionTween tween = _Script.GetTween<TransformPositionTween>();

            if (tween == null) {
                CONSOLE.Log("Tween Instance Value Is NULL");
                return;
            }

            EditorUtils.Margin = MARGIN * 3;
            EditorUtils.Line = 1;
            EditorUtils.Margin = MARGIN * 3;
            EditorGUILayout.LabelField("Transform Position Tween Input", EditorStyles.centeredGreyMiniLabel);
            EditorUtils.Margin = MARGIN * 3;

            EditorUtils.Margin = MARGIN;
            tween.StartPosition = EditorGUILayout.Vector3Field("Start Position:", tween.StartPosition);

            EditorUtils.Margin = MARGIN;
            tween.EndPosition = EditorGUILayout.Vector3Field("End Position:", tween.EndPosition);
        }
        
        /// <summary>
        /// Render 'TransformRotation' Tween Input
        /// </summary>
        /// <param name="_Script"></param>
        private void RenderTransformRotationTween(script_Tween _Script) {

            TransformRotationTween tween = _Script.GetTween<TransformRotationTween>();

            if (tween == null) {
                CONSOLE.Log("Tween Instance Value Is NULL");
                return;
            }

            EditorUtils.Margin = MARGIN * 3;
            EditorUtils.Line = 1;
            EditorUtils.Margin = MARGIN * 3;
            EditorGUILayout.LabelField("Transform Rotation Tween Input", EditorStyles.centeredGreyMiniLabel);
            EditorUtils.Margin = MARGIN * 3;

            EditorUtils.Margin = MARGIN;
            tween.StartRotation = EditorGUILayout.Vector3Field("Start Position:", tween.StartRotation);

            EditorUtils.Margin = MARGIN;
            tween.EndRotation = EditorGUILayout.Vector3Field("End Position:", tween.EndRotation);
        }
        
        /// <summary>
        /// Render 'TransformScale' Tween Input
        /// </summary>
        /// <param name="_Script"></param>
        private void RenderTransformScaleTween(script_Tween _Script) {

            TransformScaleTween tween = _Script.GetTween<TransformScaleTween>();

            if (tween == null) {
                CONSOLE.Log("Tween Instance Value Is NULL");
                return;
            }

            EditorUtils.Margin = MARGIN * 3;
            EditorUtils.Line = 1;
            EditorUtils.Margin = MARGIN * 3;
            EditorGUILayout.LabelField("Transform Scale Tween Input", EditorStyles.centeredGreyMiniLabel);
            EditorUtils.Margin = MARGIN * 3;

            EditorUtils.Margin = MARGIN;
            tween.StartScale = EditorGUILayout.Vector3Field("Start Scale:", tween.StartScale);

            EditorUtils.Margin = MARGIN;
            tween.EndScale = EditorGUILayout.Vector3Field("End Position:", tween.EndScale);
        }

        /// <summary>
        /// Render 'TransformScale' Tween Input
        /// </summary>
        /// <param name="_Script"></param>
        private void RenderCanvasGrouAlphaTween(script_Tween _Script) {

            CanvasGroupAlphaTween tween = _Script.GetTween<CanvasGroupAlphaTween>();

            if (tween == null) {
                CONSOLE.Log("Tween Instance Value Is NULL");
                return;
            }

            EditorUtils.Margin = MARGIN * 3;
            EditorUtils.Line = 1;
            EditorUtils.Margin = MARGIN * 3;
            EditorGUILayout.LabelField("CanvasGroup Alpha Tween Input", EditorStyles.centeredGreyMiniLabel);
            EditorUtils.Margin = MARGIN * 3;

            EditorUtils.Margin = MARGIN;
            tween.StartAlpha = (float) Convert.ToDouble(
                EditorGUILayout.TextField(
                    "Start Alpha:",
                    tween.StartAlpha.ToString(),
                    EditorStyles.numberField
                )
            );

            EditorUtils.Margin = MARGIN;
            tween.EndAlpha = (float)Convert.ToDouble(
                EditorGUILayout.TextField(
                    "End Alpha:",
                    tween.EndAlpha.ToString(),
                    EditorStyles.numberField
                )
            );

            EditorUtils.Margin = MARGIN;
            tween.UseCurrentAlphaToStart = EditorGUILayout.Toggle("Use Current Alpha Value", tween.UseCurrentAlphaToStart);
        }
    }
}