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
            itemsSource = sourceList,
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            showFoldoutHeader = true,
            headerTitle = label,
            showAddRemoveFooter = true,
            reorderMode = ListViewReorderMode.Animated,
            makeItem = () =>
            {
                var element = new VisualElement();
                element.Add(new Label("Object"));
                element.Add(new ObjectField
                {
                    objectType = typeof(T),
                    allowSceneObjects = allowSceneObjects
                });
                return element;
            },
            bindItem = (element, i) =>
            {
                ((ObjectField)element).value = sourceList[i];
                ((ObjectField)element).RegisterValueChangedCallback((value) => { sourceList[i] = (T)value.newValue; });
            }
        };

        return listView;
    }

    public static ListView CreateListViewEnumObjectField<TEnum, TObjectField>(
        List<SerializableTuple<TEnum, TObjectField>> sourceList, string label = null, EventCallback<ChangeEvent<SerializableTuple<TEnum, TObjectField>>> onValueChanged = null)
        where TEnum : Enum where TObjectField : UnityEngine.Object
    {
        ListView listView = new ListView(sourceList)
        {
            itemsSource = sourceList,
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            showFoldoutHeader = true,
            headerTitle = label,
            showAddRemoveFooter = true,
            reorderMode = ListViewReorderMode.Animated,
            makeItem = () =>
            {
                var element = new VisualElement();
                element.Add(new Label("Tuple"));
                element.Add(new TupleField<TEnum, TObjectField>());
                return element;
            },
            bindItem = (element, i) =>
            {
                var tupleField = ((TupleField<TEnum, TObjectField>)element.ElementAt(1));
                tupleField.value = sourceList[i];
                if (onValueChanged != null)
                {
                    tupleField.RegisterValueChangedCallback((value) =>
                    {
                        sourceList[i] = tupleField.GetValue();
                        onValueChanged.Invoke(value);
                    });
                }
            }
        };

        return listView;
    }

    public static ListView CreateListViewTuple<TEnum1, TEnum2, TEnum3>(
        List<SerializableTuple<TEnum1, TEnum2, TEnum3>> sourceList, string label = null,
        EventCallback<ChangeEvent<SerializableTuple<TEnum1, TEnum2, TEnum3>>> onValueChanged = null)
        where TEnum1 : Enum where TEnum2 : Enum where TEnum3 : Enum
    {
        ListView listView = new ListView(sourceList)
        {
            itemsSource = sourceList,
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            showFoldoutHeader = true,
            headerTitle = label,
            showAddRemoveFooter = true,
            reorderMode = ListViewReorderMode.Animated,
            makeItem = () =>
            {
                VisualElement element = new VisualElement();
                element.Add(new Label("Tuple"));
                element.Add(new TupleField<TEnum1, TEnum2, TEnum3>());
                return element;
            },
            bindItem = (element, i) =>
            {
                var tupleField = ((TupleField<TEnum1, TEnum2, TEnum3>)element.ElementAt(1));
                tupleField.value = sourceList[i];
                if (onValueChanged != null)
                {
                    tupleField.RegisterValueChangedCallback((value) =>
                    {
                        sourceList[i] = tupleField.GetValue();
                        onValueChanged.Invoke(value);
                    });
                }
            }
        };

        return listView;
    }

    public class TupleField<TEnum, TObjectField> : VisualElement,
        INotifyValueChanged<SerializableTuple<TEnum, TObjectField>>
        where TEnum : Enum where TObjectField : UnityEngine.Object
    {
        public SerializableTuple<TEnum, TObjectField> value { get; set; }

        private EnumField enumField;
        private ObjectField objectField;

        public TupleField()
        {
            enumField = CreateEnumField(value.Item1, "Enum :",
                callback => { value.Item1 = (TEnum)callback.newValue; });
            objectField = CreateObjectField(value.Item2, typeof(TObjectField), "Object Field :",
                callback => { value.Item2 = (TObjectField)callback.newValue; });

            Add(enumField);
            Add(objectField);
        }

        public SerializableTuple<TEnum, TObjectField> GetValue()
        {
            return new SerializableTuple<TEnum, TObjectField>((TEnum)enumField.value, (TObjectField)objectField.value);
        }

        public void SetValueWithoutNotify(SerializableTuple<TEnum, TObjectField> newValue)
        {
            enumField.SetValueWithoutNotify(newValue.Item1);
            objectField.SetValueWithoutNotify(newValue.Item2);
        }
    }

    public class TupleField<TEnum1, TEnum2, TEnum3> : VisualElement,
        INotifyValueChanged<SerializableTuple<TEnum1, TEnum2, TEnum3>>
        where TEnum1 : Enum where TEnum2 : Enum where TEnum3 : Enum
    {
        public SerializableTuple<TEnum1, TEnum2, TEnum3> value { get; set; }

        private EnumField enumField1;
        private EnumField enumField2;
        private EnumField enumField3;

        public TupleField()
        {
            enumField1 = CreateEnumField(value.Item1, "Enum 1 :",
                callback => { value.Item1 = (TEnum1)callback.newValue; });
            enumField2 = CreateEnumField(value.Item2, "Enum 2 :",
                callback => { value.Item2 = (TEnum2)callback.newValue; });
            enumField3 = CreateEnumField(value.Item3, "Enum 3 :",
                callback => { value.Item3 = (TEnum3)callback.newValue; });

            Add(enumField1);
            Add(enumField2);
            Add(enumField3);
        }

        public SerializableTuple<TEnum1, TEnum2, TEnum3> GetValue()
        {
            return new SerializableTuple<TEnum1, TEnum2, TEnum3>((TEnum1)enumField1.value, (TEnum2)enumField2.value,
                (TEnum3)enumField3.value);
        }

        public void SetValueWithoutNotify(SerializableTuple<TEnum1, TEnum2, TEnum3> newValue)
        {
            enumField1.SetValueWithoutNotify(newValue.Item1);
            enumField2.SetValueWithoutNotify(newValue.Item2);
            enumField3.SetValueWithoutNotify(newValue.Item3);
        }
    }
}