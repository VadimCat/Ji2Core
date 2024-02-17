#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Cysharp.Threading.Tasks.Editor
{
    // reflection call of UnityEditor.SplitterGUILayout
    internal static class SplitterGUILayout
    {
        static BindingFlags _flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        static Lazy<Type> _splitterStateType = new Lazy<Type>(() =>
        {
            var type = typeof(EditorWindow).Assembly.GetTypes().First(x => x.FullName == "UnityEditor.SplitterState");
            return type;
        });

        static Lazy<ConstructorInfo> _splitterStateCtor = new Lazy<ConstructorInfo>(() =>
        {
            var type = _splitterStateType.Value;
            return type.GetConstructor(_flags, null, new Type[] { typeof(float[]), typeof(int[]), typeof(int[]) }, null);
        });

        static Lazy<Type> _splitterGUILayoutType = new Lazy<Type>(() =>
        {
            var type = typeof(EditorWindow).Assembly.GetTypes().First(x => x.FullName == "UnityEditor.SplitterGUILayout");
            return type;
        });

        static Lazy<MethodInfo> _beginVerticalSplit = new Lazy<MethodInfo>(() =>
        {
            var type = _splitterGUILayoutType.Value;
            return type.GetMethod("BeginVerticalSplit", _flags, null, new Type[] { _splitterStateType.Value, typeof(GUILayoutOption[]) }, null);
        });

        static Lazy<MethodInfo> _endVerticalSplit = new Lazy<MethodInfo>(() =>
        {
            var type = _splitterGUILayoutType.Value;
            return type.GetMethod("EndVerticalSplit", _flags, null, Type.EmptyTypes, null);
        });

        public static object CreateSplitterState(float[] relativeSizes, int[] minSizes, int[] maxSizes)
        {
            return _splitterStateCtor.Value.Invoke(new object[] { relativeSizes, minSizes, maxSizes });
        }

        public static void BeginVerticalSplit(object splitterState, params GUILayoutOption[] options)
        {
            _beginVerticalSplit.Value.Invoke(null, new object[] { splitterState, options });
        }

        public static void EndVerticalSplit()
        {
            _endVerticalSplit.Value.Invoke(null, Type.EmptyTypes);
        }
    }
}

