﻿using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Sisus.Init.EditorOnly
{
    using static InitializerEditorUtility;

    [CustomEditor(typeof(Initializer<,,,,>), true), CanEditMultipleObjects]
    public sealed class InitializerT4Editor : InitializerEditor
    {
        private SerializedProperty firstArgument;
        private SerializedProperty secondArgument;
        private SerializedProperty thirdArgument;
        private SerializedProperty fourthArgument;

        private Type firstArgumentType;
        private Type secondArgumentType;
        private Type thirdArgumentType;
        private Type fourthArgumentType;

        private PropertyDrawer firstPropertyDrawer;
        private PropertyDrawer secondPropertyDrawer;
        private PropertyDrawer thirdPropertyDrawer;
        private PropertyDrawer fourthPropertyDrawer;

        private GUIContent firstArgumentLabel;
        private GUIContent secondArgumentLabel;
        private GUIContent thirdArgumentLabel;
        private GUIContent fourthArgumentLabel;

        private bool firstArgumentIsService;
        private bool secondArgumentIsService;
        private bool thirdArgumentIsService;
        private bool fourthArgumentIsService;

        protected override void Setup(Type[] genericArguments)
        {
            firstArgument = serializedObject.FindProperty(nameof(firstArgument));
            secondArgument = serializedObject.FindProperty(nameof(secondArgument));
            thirdArgument = serializedObject.FindProperty(nameof(thirdArgument));
            fourthArgument = serializedObject.FindProperty(nameof(fourthArgument));

            var clientType = genericArguments[0];
            firstArgumentType = genericArguments[1];
            secondArgumentType = genericArguments[2];
            thirdArgumentType = genericArguments[3];
            fourthArgumentType = genericArguments[4];

            firstArgumentIsService = IsService(firstArgumentType);
            secondArgumentIsService = IsService(secondArgumentType);
            thirdArgumentIsService = IsService(thirdArgumentType);
            fourthArgumentIsService = IsService(fourthArgumentType);

            var initializerType = target.GetType();
            var metadata = initializerType.GetNestedType(InitArgumentMetadataClassName, BindingFlags.Public | BindingFlags.NonPublic);
            var members = metadata is null ? Array.Empty<MemberInfo>() : metadata.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

            // constructor + 4 other members
            if(members.Length == 5)
			{
                Array.Sort(members, (f1, f2) => f1.MetadataToken.CompareTo(f2.MetadataToken));

                TryGetAttributeBasedPropertyDrawer(metadata, firstArgument, firstArgumentType, out firstPropertyDrawer);
                TryGetAttributeBasedPropertyDrawer(metadata, secondArgument, secondArgumentType, out secondPropertyDrawer);
                TryGetAttributeBasedPropertyDrawer(metadata, thirdArgument, thirdArgumentType, out thirdPropertyDrawer);
                TryGetAttributeBasedPropertyDrawer(metadata, fourthArgument, fourthArgumentType, out fourthPropertyDrawer);

                firstArgumentLabel = GetLabel(members[0]);
                secondArgumentLabel = GetLabel(members[1]);
                thirdArgumentLabel = GetLabel(members[2]);
                fourthArgumentLabel = GetLabel(members[3]);
            }
            else
            {
                firstArgumentLabel = GetArgumentLabel(clientType, firstArgumentType);
                secondArgumentLabel = GetArgumentLabel(clientType, secondArgumentType);
                thirdArgumentLabel = GetArgumentLabel(clientType, thirdArgumentType);
                fourthArgumentLabel = GetArgumentLabel(clientType, fourthArgumentType);
            }
        }

        protected override void DrawArgumentFields(bool nullAllowed, bool servicesShown)
        {
            DrawArgumentField(firstArgument, firstArgumentType, firstArgumentLabel, firstPropertyDrawer, firstArgumentIsService, nullAllowed, servicesShown);
            DrawArgumentField(secondArgument, secondArgumentType, secondArgumentLabel, secondPropertyDrawer, secondArgumentIsService, nullAllowed, servicesShown);
            DrawArgumentField(thirdArgument, thirdArgumentType, thirdArgumentLabel, thirdPropertyDrawer, thirdArgumentIsService, nullAllowed, servicesShown);
            DrawArgumentField(fourthArgument, fourthArgumentType, fourthArgumentLabel, fourthPropertyDrawer, fourthArgumentIsService, nullAllowed, servicesShown);
        }
    }
}