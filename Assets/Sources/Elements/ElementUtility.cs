using System;
using System.Collections.Generic;
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

    public static ListView CreateListViewObjectField<T>(List<T> sourceList, string label = null,
        bool allowSceneObjects = true) where T : UnityEngine.Object
    {
        ListView listView = new ListView(sourceList)
        {
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            showFoldoutHeader = true,
            headerTitle = label,
            showAddRemoveFooter = true,
            reorderMode = ListViewReorderMode.Animated,
            makeItem = () => new ObjectField
            {
                objectType = typeof(T),
                allowSceneObjects = allowSceneObjects
            },
            bindItem = (element, i) =>
            {
                ((ObjectField)element).value = sourceList[i];
                ((ObjectField)element).RegisterValueChangedCallback((value) => { sourceList[i] = (T)value.newValue; });
            }
        };

        return listView;
    }

    public static ListView CreateListViewTuple<TEnum1, TEnum2, TEnum3>(
        List<SerializableTuple<TEnum1, TEnum2, TEnum3>> sourceList, string label = null)
        where TEnum1 : Enum where TEnum2 : Enum where TEnum3 : Enum
    {
        ListView listView = new ListView(sourceList)
        {
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            showFoldoutHeader = true,
            headerTitle = label,
            showAddRemoveFooter = true,
            reorderMode = ListViewReorderMode.Animated,
            makeItem = () =>
            {
                VisualElement element = new VisualElement();
                element.Add(new Label("Tuple"));
                element.Add(CreateTupleField<TEnum1, TEnum2, TEnum3>());
                return element;
            },
            bindItem = (element, i) =>
            {
                if (element is TupleField<TEnum1, TEnum2, TEnum3> tupleField)
                {
                    tupleField.SetValue(sourceList[i]);
                    sourceList[i] = tupleField.GetValue();
                }
            }
        };

        return listView;
    }

    private static TupleField<TEnum1, TEnum2, TEnum3> CreateTupleField<TEnum1, TEnum2, TEnum3>()
        where TEnum1 : Enum where TEnum2 : Enum where TEnum3 : Enum
    {
        return new TupleField<TEnum1, TEnum2, TEnum3>();
    }

    public class TupleField<TEnum1, TEnum2, TEnum3> : VisualElement
        where TEnum1 : Enum where TEnum2 : Enum where TEnum3 : Enum
    {
        private TEnum1 enum1;
        private TEnum2 enum2;
        private TEnum3 enum3;

        private EnumField enumField1;
        private EnumField enumField2;
        private EnumField enumField3;

        public TupleField()
        {
            enumField1 =
                ElementUtility.CreateEnumField(enum1, "Job :", callback => { enum1 = (TEnum1)callback.newValue; });
            enumField2 = ElementUtility.CreateEnumField(enum2, "Positive Traits :",
                callback => { enum2 = (TEnum2)callback.newValue; });
            enumField3 = ElementUtility.CreateEnumField(enum3, "Negative Traits :",
                callback => { enum3 = (TEnum3)callback.newValue; });

            Add(enumField1);
            Add(enumField2);
            Add(enumField3);
        }

        public void SetValue(SerializableTuple<TEnum1, TEnum2, TEnum3> value)
        {
            enumField1.SetValueWithoutNotify(value.Item1);
            enumField2.SetValueWithoutNotify(value.Item2);
            enumField3.SetValueWithoutNotify(value.Item3);
        }

        public SerializableTuple<TEnum1, TEnum2, TEnum3> GetValue()
        {
            return new SerializableTuple<TEnum1, TEnum2, TEnum3>((TEnum1)enumField1.value, (TEnum2)enumField2.value, (TEnum3)enumField3.value);
        }
    }
}