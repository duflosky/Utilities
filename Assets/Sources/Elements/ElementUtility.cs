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
        bool allowSceneObjects = true, EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null) where T : UnityEngine.Object
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
                var objectField = ((ObjectField)element.ElementAt(1));
                objectField.value = sourceList[i];
                objectField.SetValueWithoutNotify(sourceList[i]);
                if (onValueChanged != null)
                {
                    objectField.RegisterValueChangedCallback(onValueChanged);
                }
            }
        };

        return listView;
    }

    public static ListView CreateListViewEnumObjectField<TEnum, TObjectField>(
        List<SerializableTuple<TEnum, TObjectField>> sourceList, string label = null,
        EventCallback<ChangeEvent<SerializableTuple<TEnum, TObjectField>>> onValueChanged = null)
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
                sourceList[i] ??= new SerializableTuple<TEnum, TObjectField>() { Item1 = default, Item2 = default };
                tupleField.value = sourceList[i];
                tupleField.SetValueWithoutNotify(sourceList[i]);
                if (onValueChanged != null)
                {
                    tupleField.RegisterValueChangedCallback(onValueChanged);
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
                sourceList[i] ??= new SerializableTuple<TEnum1, TEnum2, TEnum3>()
                    { Item1 = default, Item2 = default, Item3 = default };
                tupleField.value = sourceList[i];
                tupleField.SetValueWithoutNotify(sourceList[i]);
                if (onValueChanged != null)
                {
                    tupleField.RegisterValueChangedCallback(onValueChanged);
                }
            }
        };

        return listView;
    }

    public class TupleField<TEnum, TObjectField> : VisualElement,
        INotifyValueChanged<SerializableTuple<TEnum, TObjectField>>
        where TEnum : Enum where TObjectField : UnityEngine.Object
    {
        private SerializableTuple<TEnum, TObjectField> _value;

        public SerializableTuple<TEnum, TObjectField> value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    NotifyValueChanged(_value);
                }
            }
        }

        public event EventCallback<ChangeEvent<SerializableTuple<TEnum, TObjectField>>> onValueChanged;

        public void NotifyValueChanged(SerializableTuple<TEnum, TObjectField> newValue)
        {
            onValueChanged?.Invoke(new ChangeEvent<SerializableTuple<TEnum, TObjectField>>());
        }

        public void RegisterValueChangedCallback(EventCallback<ChangeEvent<SerializableTuple<TEnum, TObjectField>>> callback)
        {
            onValueChanged += callback;
        }

        public void UnregisterValueChangedCallback(EventCallback<ChangeEvent<SerializableTuple<TEnum, TObjectField>>> callback)
        {
            onValueChanged -= callback;
        }

        private EnumField enumField;
        private ObjectField objectField;

        public TupleField()
        {
            enumField = new EnumField();
            objectField = new ObjectField();

            Add(enumField);
            Add(objectField);
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
        
        public event EventCallback<ChangeEvent<SerializableTuple<TEnum1, TEnum2, TEnum3>>> onValueChanged;

        public void NotifyValueChanged(SerializableTuple<TEnum1, TEnum2, TEnum3> newValue)
        {
            onValueChanged?.Invoke(new ChangeEvent<SerializableTuple<TEnum1, TEnum2, TEnum3>>());
        }

        public void RegisterValueChangedCallback(EventCallback<ChangeEvent<SerializableTuple<TEnum1, TEnum2, TEnum3>>> callback)
        {
            onValueChanged += callback;
        }

        public void UnregisterValueChangedCallback(EventCallback<ChangeEvent<SerializableTuple<TEnum1, TEnum2, TEnum3>>> callback)
        {
            onValueChanged -= callback;
        }

        public TupleField()
        {
            enumField1 = new EnumField();
            enumField2 = new EnumField();
            enumField3 = new EnumField();

            Add(enumField1);
            Add(enumField2);
            Add(enumField3);
        }

        public void SetValueWithoutNotify(SerializableTuple<TEnum1, TEnum2, TEnum3> newValue)
        {
            enumField1.SetValueWithoutNotify(newValue.Item1);
            enumField2.SetValueWithoutNotify(newValue.Item2);
            enumField3.SetValueWithoutNotify(newValue.Item3);
        }
    }
}