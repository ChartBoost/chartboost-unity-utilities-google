using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chartboost.Google.Editor
{
    [CustomEditor(typeof(GoogleSettings))]
    public class GoogleSettingsEditor : UnityEditor.Editor
    {
        public VisualTreeAsset inspectorXML;
        
        public override VisualElement CreateInspectorGUI()
        {
            // Create a new VisualElement to be the root of our Inspector UI.
            var myInspector = new VisualElement();

            // Load from default reference.
            inspectorXML.CloneTree(myInspector);

            // Return the finished Inspector UI.
            return myInspector;
        }
    }
}
