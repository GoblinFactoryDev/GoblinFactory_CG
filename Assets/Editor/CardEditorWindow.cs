using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Linq;

public class CardEditorWindow : EditorWindow
{
    [MenuItem("Tools/Card Editor Window")]

    public static void ShowWindow()
    {
        var window = GetWindow<CardEditorWindow>();
        window.titleContent = new GUIContent("Card Editor");
        window.minSize = new Vector2(800, 800);
    }

    private void OnEnable()
    {
        VisualTreeAsset original = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/CardEditorWindow.uxml");
        TemplateContainer treeAsset = original.CloneTree();
        rootVisualElement.Add(treeAsset);

        StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/CardEditorStyles.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        CreateCardListView();
    }

    private void CreateCardListView()
    {
        FindAllCards(out CardData[] cards);

        ListView cardList = rootVisualElement.Query<ListView>("Card-List").First();
        cardList.makeItem = () => new Label();
        cardList.bindItem = (element, i) => (element as Label).text = cards[i].name;

        cardList.itemsSource = cards;
        cardList.fixedItemHeight = 16;
        cardList.selectionType = SelectionType.Single;

        cardList.selectionChanged += (enumerable) =>
        {
            foreach (object it in enumerable)
            {
                Box cardInfoBox = rootVisualElement.Query<Box>("Card-Info").First();
                cardInfoBox.Clear();

                CardData card = it as CardData;

                SerializedObject serializedCard = new SerializedObject(card);
                SerializedProperty cardProperty = serializedCard.GetIterator();
                cardProperty.Next(true);

                while (cardProperty.NextVisible(false))
                {
                    PropertyField prop = new PropertyField(cardProperty);

                    //ensure we dont allow anyone to change the scripting reference
                    prop.SetEnabled(cardProperty.name != "m_Script");
                    prop.Bind(serializedCard);
                    cardInfoBox.Add(prop);

                    if (cardProperty.name == "icon")
                    {
                        prop.RegisterCallback<ChangeEvent<UnityEngine.Object>>((changeEvt) => LoadCardImage(card.icon));
                    }
                }
                LoadCardImage(card.icon);
            }
        };
        cardList.Rebuild();

    }

    private void FindAllCards(out CardData[] cards)
    {
        var guids = AssetDatabase.FindAssets("t:CardData");

        cards = new CardData[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            var path = AssetDatabase.GUIDToAssetPath(guids[i]);
            cards[i] = AssetDatabase.LoadAssetAtPath<CardData>(path);
        }
    }

    private void LoadCardImage(Sprite tex)
    {
        var previewImage = rootVisualElement.Query<VisualElement>("preview").First();
        previewImage.style.backgroundImage = new StyleBackground(tex);
    }
}
