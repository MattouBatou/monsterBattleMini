//Hotkeys:
//Redo by pressing 'Ctrl + Shift + Z'

using UnityEngine;
using UnityEditor;

public class ExtendedHotkeys : ScriptableObject {
	//Redo by pressing 'Ctrl + Shift + Z'
	[MenuItem ("MattouBatou Keys/Deselect All %#z")]
	static void DoRedo(){
        Undo.PerformRedo();
	}

}
