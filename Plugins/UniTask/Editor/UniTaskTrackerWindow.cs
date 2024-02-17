#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;
using UnityEditor.IMGUI.Controls;
using Cysharp.Threading.Tasks.Internal;

namespace Cysharp.Threading.Tasks.Editor
{
    public class UniTaskTrackerWindow : EditorWindow
    {
        static int _interval;

        static UniTaskTrackerWindow _window;

        [MenuItem("Window/UniTask Tracker")]
        public static void OpenWindow()
        {
            if (_window != null)
            {
                _window.Close();
            }

            // will called OnEnable(singleton instance will be set).
            GetWindow<UniTaskTrackerWindow>("UniTask Tracker").Show();
        }

        static readonly GUILayoutOption[] EmptyLayoutOption = new GUILayoutOption[0];

        UniTaskTrackerTreeView _treeView;
        object _splitterState;

        void OnEnable()
        {
            _window = this; // set singleton.
            _splitterState = SplitterGUILayout.CreateSplitterState(new float[] { 75f, 25f }, new int[] { 32, 32 }, null);
            _treeView = new UniTaskTrackerTreeView();
            TaskTracker.EditorEnableState.EnableAutoReload = EditorPrefs.GetBool(TaskTracker.EnableAutoReloadKey, false);
            TaskTracker.EditorEnableState.EnableTracking = EditorPrefs.GetBool(TaskTracker.EnableTrackingKey, false);
            TaskTracker.EditorEnableState.EnableStackTrace = EditorPrefs.GetBool(TaskTracker.EnableStackTraceKey, false);
        }

        void OnGUI()
        {
            // Head
            RenderHeadPanel();

            // Splittable
            SplitterGUILayout.BeginVerticalSplit(this._splitterState, EmptyLayoutOption);
            {
                // Column Tabble
                RenderTable();

                // StackTrace details
                RenderDetailsPanel();
            }
            SplitterGUILayout.EndVerticalSplit();
        }

        #region HeadPanel

        public static bool EnableAutoReload => TaskTracker.EditorEnableState.EnableAutoReload;
        public static bool EnableTracking => TaskTracker.EditorEnableState.EnableTracking;
        public static bool EnableStackTrace => TaskTracker.EditorEnableState.EnableStackTrace;
        static readonly GUIContent EnableAutoReloadHeadContent = EditorGUIUtility.TrTextContent("Enable AutoReload", "Reload automatically.", (Texture)null);
        static readonly GUIContent ReloadHeadContent = EditorGUIUtility.TrTextContent("Reload", "Reload View.", (Texture)null);
        static readonly GUIContent GCHeadContent = EditorGUIUtility.TrTextContent("GC.Collect", "Invoke GC.Collect.", (Texture)null);
        static readonly GUIContent EnableTrackingHeadContent = EditorGUIUtility.TrTextContent("Enable Tracking", "Start to track async/await UniTask. Performance impact: low", (Texture)null);
        static readonly GUIContent EnableStackTraceHeadContent = EditorGUIUtility.TrTextContent("Enable StackTrace", "Capture StackTrace when task is started. Performance impact: high", (Texture)null);

        // [Enable Tracking] | [Enable StackTrace]
        void RenderHeadPanel()
        {
            EditorGUILayout.BeginVertical(EmptyLayoutOption);
            EditorGUILayout.BeginHorizontal(EditorStyles.toolbar, EmptyLayoutOption);

            if (GUILayout.Toggle(EnableAutoReload, EnableAutoReloadHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption) != EnableAutoReload)
            {
                TaskTracker.EditorEnableState.EnableAutoReload = !EnableAutoReload;
            }

            if (GUILayout.Toggle(EnableTracking, EnableTrackingHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption) != EnableTracking)
            {
                TaskTracker.EditorEnableState.EnableTracking = !EnableTracking;
            }

            if (GUILayout.Toggle(EnableStackTrace, EnableStackTraceHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption) != EnableStackTrace)
            {
                TaskTracker.EditorEnableState.EnableStackTrace = !EnableStackTrace;
            }

            GUILayout.FlexibleSpace();

            if (GUILayout.Button(ReloadHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption))
            {
                TaskTracker.CheckAndResetDirty();
                _treeView.ReloadAndSort();
                Repaint();
            }

            if (GUILayout.Button(GCHeadContent, EditorStyles.toolbarButton, EmptyLayoutOption))
            {
                GC.Collect(0);
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        #endregion

        #region TableColumn

        Vector2 _tableScroll;
        GUIStyle _tableListStyle;

        void RenderTable()
        {
            if (_tableListStyle == null)
            {
                _tableListStyle = new GUIStyle("CN Box");
                _tableListStyle.margin.top = 0;
                _tableListStyle.padding.left = 3;
            }

            EditorGUILayout.BeginVertical(_tableListStyle, EmptyLayoutOption);

            this._tableScroll = EditorGUILayout.BeginScrollView(this._tableScroll, new GUILayoutOption[]
            {
                GUILayout.ExpandWidth(true),
                GUILayout.MaxWidth(2000f)
            });
            var controlRect = EditorGUILayout.GetControlRect(new GUILayoutOption[]
            {
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true)
            });


            _treeView?.OnGUI(controlRect);

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void Update()
        {
            if (EnableAutoReload)
            {
                if (_interval++ % 120 == 0)
                {
                    if (TaskTracker.CheckAndResetDirty())
                    {
                        _treeView.ReloadAndSort();
                        Repaint();
                    }
                }
            }
        }

        #endregion

        #region Details

        static GUIStyle _detailsStyle;
        Vector2 _detailsScroll;

        void RenderDetailsPanel()
        {
            if (_detailsStyle == null)
            {
                _detailsStyle = new GUIStyle("CN Message");
                _detailsStyle.wordWrap = false;
                _detailsStyle.stretchHeight = true;
                _detailsStyle.margin.right = 15;
            }

            string message = "";
            var selected = _treeView.state.selectedIDs;
            if (selected.Count > 0)
            {
                var first = selected[0];
                var item = _treeView.CurrentBindingItems.FirstOrDefault(x => x.id == first) as UniTaskTrackerViewItem;
                if (item != null)
                {
                    message = item.Position;
                }
            }

            _detailsScroll = EditorGUILayout.BeginScrollView(this._detailsScroll, EmptyLayoutOption);
            var vector = _detailsStyle.CalcSize(new GUIContent(message));
            EditorGUILayout.SelectableLabel(message, _detailsStyle, new GUILayoutOption[]
            {
                GUILayout.ExpandHeight(true),
                GUILayout.ExpandWidth(true),
                GUILayout.MinWidth(vector.x),
                GUILayout.MinHeight(vector.y)
            });
            EditorGUILayout.EndScrollView();
        }

        #endregion
    }
}

