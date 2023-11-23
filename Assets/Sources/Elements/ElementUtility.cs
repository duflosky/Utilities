﻿using System;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public static class ElementUtility
{
    public static Button CreateButton(string text, Action onClick = null)
    {
        Button button = new Button(onClick)
        {
            text = text
        };

        return button;
    }

    public static Foldout CreateFoldout(string title, bool collapsed = false)
    {
        Foldout foldout = new Foldout()
        {
            text = title,
            value = collapsed
        };

        return foldout;
    }

    public static Port CreatePort(this Node node, string portName = "",
        Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output,
        Port.Capacity capacity = Port.Capacity.Single)
    {
        Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));

        port.portName = portName;

        return port;
    }

    public static TextField CreateTextField(string value = null, string label = null,
        EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        TextField textField = new TextField()
        {
            value = value,
            label = label
        };

        if (onValueChanged != null)
        {
            textField.RegisterValueChangedCallback(onValueChanged);
        }

        return textField;
    }

    public static TextField CreateTextArea(string value = null, string label = null,
        EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        TextField textArea = CreateTextField(value, label, onValueChanged);

        textArea.multiline = true;

        return textArea;
    }

    public static IntegerField CreateIntegerField(int value = 1, string label = null,
        EventCallback<ChangeEvent<int>> onValueChanged = null)
    {
        IntegerField integerField = new IntegerField()
        {
            value = value,
            label = label
        };

        if (onValueChanged != null)
        {
            integerField.RegisterValueChangedCallback(onValueChanged);
        }

        return integerField;
    }

    public static UnsignedIntegerField CreateUnsignedIntegerField(uint value = 1, string label = null,
        EventCallback<ChangeEvent<uint>> onValueChanged = null)
    {
        UnsignedIntegerField unsignedIntegerField = new UnsignedIntegerField()
        {
            value = value,
            label = label
        };

        if (onValueChanged != null)
        {
            unsignedIntegerField.RegisterValueChangedCallback(onValueChanged);
        }

        return unsignedIntegerField;
    }

    public static FloatField CreateFloatField(float value = 1.0f, string label = null,
        EventCallback<ChangeEvent<float>> onValueChanged = null)
    {
        FloatField floatField = new FloatField()
        {
            value = value,
            label = label
        };

        if (onValueChanged != null)
        {
            floatField.RegisterValueChangedCallback(onValueChanged);
        }

        return floatField;
    }

    public static SliderInt CreateSliderIntField(int value = 1, string label = null, int lowValue = 0,
        int highValue = 1,
        EventCallback<ChangeEvent<int>> onValueChanged = null)
    {
        SliderInt sliderIntField = new SliderInt()
        {
            value = value,
            label = $"{label} {value}",
            lowValue = lowValue,
            highValue = highValue
        };

        if (onValueChanged != null)
        {
            sliderIntField.RegisterValueChangedCallback(onValueChanged);
        }

        return sliderIntField;
    }

    public static EnumField CreateEnumField(Enum value = null, string label = null,
        EventCallback<ChangeEvent<Enum>> onValueChanged = null)
    {
        EnumField enumField = new EnumField()
        {
            value = value,
            label = label
        };

        enumField.Init(value);

        if (onValueChanged != null)
        {
            enumField.RegisterValueChangedCallback(onValueChanged);
        }

        return enumField;
    }

    public static EnumFlagsField CreateEnumFlagsField(Enum value = null, string label = null,
        EventCallback<ChangeEvent<Enum>> onValueChanged = null)
    {
        EnumFlagsField enumFlagsField = new EnumFlagsField()
        {
            value = value,
            label = label
        };

        enumFlagsField.Init(value);

        if (onValueChanged != null)
        {
            enumFlagsField.RegisterValueChangedCallback(onValueChanged);
        }

        return enumFlagsField;
    }

    public static ObjectField CreateObjectField(UnityEngine.Object value = null, Type type = null,
        string label = null,
        EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null)
    {
        ObjectField objectField = new ObjectField()
        {
            objectType = type,
            allowSceneObjects = false,
            value = value,
            label = label
        };

        if (onValueChanged != null)
        {
            objectField.RegisterValueChangedCallback(onValueChanged);
        }

        return objectField;
    }

    public static Toggle CreateToggle(bool value = false, string label = null,
        EventCallback<ChangeEvent<bool>> onValueChanged = null)
    {
        Toggle toggle = new Toggle()
        {
            value = value,
            label = label
        };

        if (onValueChanged != null)
        {
            toggle.RegisterValueChangedCallback(onValueChanged);
        }

        return toggle;
    }

    public static ListView CreateListView<T>(T[] items)
    {
        ListView listView = new ListView(items)
        {
            selectionType = SelectionType.Single
        };

        return listView;
    }
}