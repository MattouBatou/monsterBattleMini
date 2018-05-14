using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor.Sprites;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

namespace UnityEditor
{
	[CustomEditor(typeof(RuleTileAND))]
	[CanEditMultipleObjects]
	internal class RuleTileANDEditor : Editor
	{
		private const string s_XIconString = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABoSURBVDhPnY3BDcAgDAOZhS14dP1O0x2C/LBEgiNSHvfwyZabmV0jZRUpq2zi6f0DJwdcQOEdwwDLypF0zHLMa9+NQRxkQ+ACOT2STVw/q8eY1346ZlE54sYAhVhSDrjwFymrSFnD2gTZpls2OvFUHAAAAABJRU5ErkJggg==";

        // Arrows
		private const string s_Arrow0 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAACYSURBVDhPzZExDoQwDATzE4oU4QXXcgUFj+YxtETwgpMwXuFcwMFSRMVKKwzZcWzhiMg91jtg34XIntkre5EaT7yjjhI9pOD5Mw5k2X/DdUwFr3cQ7Pu23E/BiwXyWSOxrNqx+ewnsayam5OLBtbOGPUM/r93YZL4/dhpR/amwByGFBz170gNChA6w5bQQMqramBTgJ+Z3A58WuWejPCaHQAAAABJRU5ErkJggg==";
		private const string s_Arrow1 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABqSURBVDhPxYzBDYAgEATpxYcd+PVr0fZ2siZrjmMhFz6STIiDs8XMlpEyi5RkO/d66TcgJUB43JfNBqRkSEYDnYjhbKD5GIUkDqRDwoH3+NgTAw+bL/aoOP4DOgH+iwECEt+IlFmkzGHlAYKAWF9R8zUnAAAAAElFTkSuQmCC";
		private const string s_Arrow2 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAAC0SURBVDhPjVE5EsIwDMxPKFKYF9CagoJH8xhaMskLmEGsjOSRkBzYmU2s9a58TUQUmCH1BWEHweuKP+D8tphrWcAHuIGrjPnPNY8X2+DzEWE+FzrdrkNyg2YGNNfRGlyOaZDJOxBrDhgOowaYW8UW0Vau5ZkFmXbbDr+CzOHKmLinAXMEePyZ9dZkZR+s5QX2O8DY3zZ/sgYcdDqeEVp8516o0QQV1qeMwg6C91toYoLoo+kNt/tpKQEVvFQAAAAASUVORK5CYII=";
		private const string s_Arrow3 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAAB2SURBVDhPzY1LCoAwEEPnLi48gW5d6p31bH5SMhp0Cq0g+CCLxrzRPqMZ2pRqKG4IqzJc7JepTlbRZXYpWTg4RZE1XAso8VHFKNhQuTjKtZvHUNCEMogO4K3BhvMn9wP4EzoPZ3n0AGTW5fiBVzLAAYTP32C2Ay3agtu9V/9PAAAAAElFTkSuQmCC";
		private const string s_Arrow5 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABqSURBVDhPnY3BCYBADASvFx924NevRdvbyoLBmNuDJQMDGjNxAFhK1DyUQ9fvobCdO+j7+sOKj/uSB+xYHZAxl7IR1wNTXJeVcaAVU+614uWfCT9mVUhknMlxDokd15BYsQrJFHeUQ0+MB5ErsPi/6hO1AAAAAElFTkSuQmCC";
		private const string s_Arrow6 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAACaSURBVDhPxZExEkAwEEVzE4UiTqClUDi0w2hlOIEZsV82xCZmQuPPfFn8t1mirLWf7S5flQOXjd64vCuEKWTKVt+6AayH3tIa7yLg6Qh2FcKFB72jBgJeziA1CMHzeaNHjkfwnAK86f3KUafU2ClHIJSzs/8HHLv09M3SaMCxS7ljw/IYJWzQABOQZ66x4h614ahTCL/WT7BSO51b5Z5hSx88AAAAAElFTkSuQmCC";
		private const string s_Arrow7 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABQSURBVDhPYxh8QNle/T8U/4MKEQdAmsz2eICx6W530gygr2aQBmSMphkZYxqErAEXxusKfAYQ7XyyNMIAsgEkaYQBkAFkaYQBsjXSGDAwAAD193z4luKPrAAAAABJRU5ErkJggg==";
		private const string s_Arrow8 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAACYSURBVDhPxZE9DoAwCIW9iUOHegJXHRw8tIdx1egJTMSHAeMPaHSR5KVQ+KCkCRF91mdz4VDEWVzXTBgg5U1N5wahjHzXS3iFFVRxAygNVaZxJ6VHGIl2D6oUXP0ijlJuTp724FnID1Lq7uw2QM5+thoKth0N+GGyA7IA3+yM77Ag1e2zkey5gCdAg/h8csy+/89v7E+YkgUntOWeVt2SfAAAAABJRU5ErkJggg==";

        // Arrows AND
        private const string s_Arrow9 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPBAMAAADJ+Ih5AAAAKlBMVEUAAAAjPycjPycAAAAjPycOGQ8jPycjPyc2vEgAAAAjPycM/wAjRCgWTR3lT4RzAAAACHRSTlMA6u2X+/gf4F4rB4wAAABOSURBVAjXYwACSwMGMGCePRnC4Ny9E0wzis6eKghmdNy80QViMHV0dByAMnoSwFJAEQeYVAuEcaKjowAk1ZPV0QVWs4BhxQIGCOBiwAYAcvgWUBRJIvQAAAAASUVORK5CYII=";
        private const string s_Arrow10 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPBAMAAADJ+Ih5AAAAElBMVEUAAAAAAAA2vEgjPycAAAAM/ wC0dt3lAAAAAnRSTlMAlxj4y98AAABASURBVAjXYwADRkcGCBAJEQBRzAyuoQ4ghpEBRIRZSRmixkhJyYABLKCkpAwVgAgZGyspG0PkQDLYGQxgFZgAAElyB2GNfo98AAAAAElFTkSuQmCC";
        private const string s_Arrow11 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPBAMAAADJ+Ih5AAAAKlBMVEUAAAAjPycjPycAAAAjPycOGQ8jPycjPyc2vEgAAAAjPycM/wAjRCgWTR3lT4RzAAAACHRSTlMA6u2X+/gf4F4rB4wAAABGSURBVAjXY8AHGIUUlQRBDK6Ojo5GsNCJjo7WySBGTkfHzZ0GQMaKjo7ZuyeA1IBFwAIngGpAIi05EF0rHBgWghnsOGwCAMxbFuZWRo2+AAAAAElFTkSuQmCC";
        private const string s_Arrow12 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPBAMAAADJ+Ih5AAAAElBMVEUAAAAAAAA2vEgjPycAAAAM/ wC0dt3lAAAAAnRSTlMAlxj4y98AAAA + SURBVAjXY2B0ZIAAkRABCMM11IHZACpiBGIA1TArGUAkjZSMjUE0sxIQGIAEIAyECEINA0QXWAjGAJuMCQCGagf3g0g00gAAAABJRU5ErkJggg==";
        private const string s_Arrow14 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPBAMAAADJ+Ih5AAAAElBMVEUAAAAAAAA2vEgjPycAAAAM/ wC0dt3lAAAAAnRSTlMAlxj4y98AAAA7SURBVAjXY8AKjGEMIwMYQxlMMRsbKRmA + UpKSsowBlAIKgJXA9PF6Ag1RyREAGKya6gDRBIkAgZANQCt8Qf3XjE5LwAAAABJRU5ErkJggg==";
        private const string s_Arrow15 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPBAMAAADJ+Ih5AAAAJ1BMVEUAAAAjPycAAAAOGQ8jPycjPycjPycjPyc2vEgjQCcAAAAM/wAWTR0hkOvIAAAACHRSTlMA6pf47ewf4IGtylgAAABCSURBVAjXY8AO2MEk4wSGGRPAjM7Mjk4gzbS4Y0ZHB1BIa9eJDrDI6t0rQAIQkc4EiBqgCFgXkNEJZogEhggy4AMARSIW9MMqyMsAAAAASUVORK5CYII=";
        private const string s_Arrow16 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPBAMAAADJ+Ih5AAAAElBMVEUAAAAAAAA2vEgjPycAAAAM/ wC0dt3lAAAAAnRSTlMAlxj4y98AAAA7SURBVAjXY8AKjA0gNLOSMg6GsbGSMliVkRIQGIBllKByRhABRkeoYpEQASOwga6hDswMUBEgCVYDoQGb2Qdh+7p3OQAAAABJRU5ErkJggg==";
        private const string s_Arrow17 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPBAMAAADJ+Ih5AAAAKlBMVEUAAAAjPycjPycAAAAjPycOGQ8jPycjPyc2vEgAAAAjPycM/wAjRCgWTR3lT4RzAAAACHRSTlMA6u2X+/gf4F4rB4wAAABOSURBVAjXY8AGuGCMFQsYFoIFOrpyGkGMio6OE00gBktHRweYweDR0dEIZuTARE4AGRBdN2+ApRhFZ08VBEtx7t4JMZl59mSoHZYGQAIAU6MWUEkus9IAAAAASUVORK5CYII =";

        // Transforms
        private const string s_MirrorX = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwQAADsEBuJFr7QAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC41ZYUyZQAAAG1JREFUOE+lj9ENwCAIRB2IFdyRfRiuDSaXAF4MrR9P5eRhHGb2Gxp2oaEjIovTXSrAnPNx6hlgyCZ7o6omOdYOldGIZhAziEmOTSfigLV0RYAB9y9f/7kO8L3WUaQyhCgz0dmCL9CwCw172HgBeyG6oloC8fAAAAAASUVORK5CYII=";
		private const string s_MirrorY = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC41ZYUyZQAAAG9JREFUOE+djckNACEMAykoLdAjHbPyw1IOJ0L7mAejjFlm9hspyd77Kk+kBAjPOXcakJIh6QaKyOE0EB5dSPJAiUmOiL8PMVGxugsP/0OOib8vsY8yYwy6gRyC8CB5QIWgCMKBLgRSkikEUr5h6wOPWfMoCYILdgAAAABJRU5ErkJggg==";
		private const string s_Rotated = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwQAADsEBuJFr7QAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC41ZYUyZQAAAHdJREFUOE+djssNwCAMQxmIFdgx+2S4Vj4YxWlQgcOT8nuG5u5C732Sd3lfLlmPMR4QhXgrTQaimUlA3EtD+CJlBuQ7aUAUMjEAv9gWCQNEPhHJUkYfZ1kEpcxDzioRzGIlr0Qwi0r+Q5rTgM+AAVcygHgt7+HtBZs/2QVWP8ahAAAAAElFTkSuQmCC";

		private static Texture2D[] s_Arrows;
		public static Texture2D[] arrows
		{
			get
			{
				if (s_Arrows == null)
				{
					s_Arrows = new Texture2D[19];
					s_Arrows[0] = Base64ToTexture(s_Arrow0);
					s_Arrows[1] = Base64ToTexture(s_Arrow1);
					s_Arrows[2] = Base64ToTexture(s_Arrow2);
					s_Arrows[3] = Base64ToTexture(s_Arrow3);
					s_Arrows[5] = Base64ToTexture(s_Arrow5);
					s_Arrows[6] = Base64ToTexture(s_Arrow6);
					s_Arrows[7] = Base64ToTexture(s_Arrow7);
					s_Arrows[8] = Base64ToTexture(s_Arrow8);
                    s_Arrows[9] = Base64ToTexture(s_XIconString);

                    s_Arrows[10] = Base64ToTexture(s_Arrow9);
                    s_Arrows[11] = Base64ToTexture(s_Arrow10);
                    s_Arrows[12] = Base64ToTexture(s_Arrow11);
                    s_Arrows[13] = Base64ToTexture(s_Arrow12);
                    s_Arrows[15] = Base64ToTexture(s_Arrow14);
                    s_Arrows[16] = Base64ToTexture(s_Arrow15);
                    s_Arrows[17] = Base64ToTexture(s_Arrow16);
                    s_Arrows[18] = Base64ToTexture(s_Arrow17);
				}
				return s_Arrows;
			}
		}

		private static Texture2D[] s_AutoTransforms;
		public static Texture2D[] autoTransforms
		{
			get
			{
				if (s_AutoTransforms == null)
				{
					s_AutoTransforms = new Texture2D[3];
					s_AutoTransforms[0] = Base64ToTexture(s_Rotated);
					s_AutoTransforms[1] = Base64ToTexture(s_MirrorX);
					s_AutoTransforms[2] = Base64ToTexture(s_MirrorY);
				}
				return s_AutoTransforms;
			}
		}

		private ReorderableList m_ReorderableList;
		public RuleTileAND tile { get { return (target as RuleTileAND); } }
		private Rect m_ListRect;
        public static Rect m_SpriteToCheckRect;
        public Rect m_MatrixRect;
        public static SerializedObject m_serializeObject;


        const float k_DefaultElementHeight = 48f;
		const float k_PaddingBetweenRules = 13f;
		const float k_SingleLineHeight = 16f;
		const float k_LabelWidth = 53f;

		public void OnEnable()
		{
			if (tile.m_TilingRules == null)
				tile.m_TilingRules = new List<RuleTileAND.TilingRule>();

			m_ReorderableList = new ReorderableList(tile.m_TilingRules, typeof(RuleTileAND.TilingRule), true, true, true, true);
			m_ReorderableList.drawHeaderCallback = OnDrawHeader;
			m_ReorderableList.drawElementCallback = OnDrawElement;
			m_ReorderableList.elementHeightCallback = GetElementHeight;
			m_ReorderableList.onReorderCallback = ListUpdated;
		}

		private void ListUpdated(ReorderableList list)
		{
			SaveTile();
		}

		private float GetElementHeight(int index)
		{
            RuleTileAND.TilingRule rule = tile.m_TilingRules[index];

            float elementHeight = k_DefaultElementHeight + k_PaddingBetweenRules;

			if (tile.m_TilingRules != null && tile.m_TilingRules.Count > 0)
			{

                if (rule.m_showSpriteToCheck) {
                    elementHeight += k_SingleLineHeight * (rule.m_Sprites.Length + 3) + k_PaddingBetweenRules;
                }

                switch (rule.m_Output)
				{
					case RuleTileAND.TilingRule.OutputSprite.Random:
						return elementHeight + k_SingleLineHeight*(rule.m_Sprites.Length + 3) + k_PaddingBetweenRules;
					case RuleTileAND.TilingRule.OutputSprite.Animation:
						return elementHeight + k_SingleLineHeight*(rule.m_Sprites.Length + 2) + k_PaddingBetweenRules;
				}

			}
			return elementHeight;
		}

		private void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
		{
			RuleTileAND.TilingRule rule = tile.m_TilingRules[index];

			float yPos = rect.yMin + 2f;
			float height = rect.height - k_PaddingBetweenRules;
			float matrixWidth = k_DefaultElementHeight;


            Rect inspectorRect = new Rect(rect.xMin, yPos, rect.width - matrixWidth * 2f - 20f, height);
			Rect matrixRect = new Rect(rect.xMax - matrixWidth * 2f - 10f, yPos, matrixWidth, k_DefaultElementHeight);
			Rect spriteRect = new Rect(rect.xMax - matrixWidth - 5f, yPos, matrixWidth, k_DefaultElementHeight);

            m_MatrixRect = matrixRect;
            m_SpriteToCheckRect = new Rect(rect.xMin, yPos+k_DefaultElementHeight+k_PaddingBetweenRules, rect.width - matrixWidth * 2f - 20f, height);

            EditorGUI.BeginChangeCheck();
			RuleInspectorOnGUI(inspectorRect, rule);
			RuleMatrixOnGUI(inspectorRect, matrixRect, rule);
			SpriteOnGUI(spriteRect, rule);

            m_serializeObject = serializedObject;

			if (EditorGUI.EndChangeCheck())
				SaveTile();
		}

        public static void SpriteToCheckOnGUI(Rect rect, Rect matrixRect, RuleTileAND.TilingRule tilingRule) {
            float y = rect.yMin;
            EditorGUI.BeginChangeCheck();
            GUI.Label(new Rect(rect.xMin, y, rect.width, k_SingleLineHeight * 1.5f), "Check Neighboring Sprites");
            y += k_SingleLineHeight * 1.5f;
            GUI.DrawTexture(new Rect(rect.xMin + (k_LabelWidth / 2f), y, matrixRect.width / 3f, matrixRect.height / 3f), arrows[11]);
            tilingRule.m_neighborSpriteToCheck = (Sprite)EditorGUI.ObjectField(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tilingRule.m_neighborSpriteToCheck, typeof(Sprite), false) as Sprite;

            Debug.Log(tilingRule.m_neighborSpriteToCheck);

            if (tilingRule.m_neighborSpriteToCheck != null) {
                Debug.Log(tilingRule.m_neighborSpriteToCheck.GetType());
            }
        }

        private void SaveTile()
		{
			EditorUtility.SetDirty(target);
			SceneView.RepaintAll();
		}

		private void OnDrawHeader(Rect rect)
		{
			GUI.Label(rect, "Tiling Rules");
		}

		public override void OnInspectorGUI()
		{
			tile.m_DefaultSprite = EditorGUILayout.ObjectField("Default Sprite", tile.m_DefaultSprite, typeof(Sprite), false) as Sprite;
            tile.m_DefaultColliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup("Default Collider", tile.m_DefaultColliderType);
			EditorGUILayout.Space();

			if (m_ReorderableList != null && tile.m_TilingRules != null)
				m_ReorderableList.DoLayoutList();
		}

		private static void RuleMatrixOnGUI(Rect inspectorRect, Rect rect, RuleTileAND.TilingRule tilingRule)
		{
			Handles.color = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.2f) : new Color(0f, 0f, 0f, 0.2f);
			int index = 0;
			float w = rect.width / 3f;
			float h = rect.height / 3f;



			for (int y = 0; y <= 3; y++)
			{
				float top = rect.yMin + y * h;
				Handles.DrawLine(new Vector3(rect.xMin, top), new Vector3(rect.xMax, top));
			}
			for (int x = 0; x <= 3; x++)
			{
				float left = rect.xMin + x * w;
				Handles.DrawLine(new Vector3(left, rect.yMin), new Vector3(left, rect.yMax));
			}
			Handles.color = Color.white;

			for (int y = 0; y <= 2; y++)
			{
				for (int x = 0; x <= 2; x++)
				{
					Rect r = new Rect(rect.xMin + x * w, rect.yMin + y * h, w - 1, h - 1);
					if (x != 1 || y != 1)
					{
						switch (tilingRule.m_Neighbors[index])
						{
							case RuleTileAND.TilingRule.Neighbor.This:
								GUI.DrawTexture(r, arrows[y*3 + x]);
								break;
                            case RuleTileAND.TilingRule.Neighbor.NotThis:
								GUI.DrawTexture(r, arrows[9]);
								break;
                            case RuleTileAND.TilingRule.Neighbor.ThisPlus:
                                GUI.DrawTexture(r, arrows[y * 3 + x + 10]);
                                m_serializeObject.Update();
                                tilingRule.m_showSpriteToCheck = true;
                                SpriteToCheckOnGUI(m_SpriteToCheckRect, rect, tilingRule);
                                m_serializeObject.ApplyModifiedProperties();
                                break;
                        }
						if (Event.current.type == EventType.MouseDown && r.Contains(Event.current.mousePosition))
						{
                            int change = 1;
						    if (Event.current.button == 1)
								change = -1;
							tilingRule.m_Neighbors[index] = (RuleTileAND.TilingRule.Neighbor) (((int)tilingRule.m_Neighbors[index] + change) % 4);

                            if(((int)tilingRule.m_Neighbors[index] + change) % 4 == 0) {
                                m_serializeObject.Update();
                                tilingRule.m_showSpriteToCheck = true;
                                SpriteToCheckOnGUI(m_SpriteToCheckRect, rect, tilingRule);
                                m_serializeObject.ApplyModifiedProperties();
                            } else {
                                m_serializeObject.Update();
                                tilingRule.m_showSpriteToCheck = false;
                                // TODO: Destroy sprite list;
                                tilingRule.m_neighborSpriteToCheck = null;
                                m_serializeObject.ApplyModifiedProperties();
                            }
							GUI.changed = true;
							Event.current.Use();
						}

						index++;
					}
					else
					{
						switch (tilingRule.m_RuleTransform)
						{
							case RuleTileAND.TilingRule.Transform.Rotated:
								GUI.DrawTexture(r, autoTransforms[0]);
								break;
							case RuleTileAND.TilingRule.Transform.MirrorX:
								GUI.DrawTexture(r, autoTransforms[1]);
								break;
							case RuleTileAND.TilingRule.Transform.MirrorY:
								GUI.DrawTexture(r, autoTransforms[2]);
								break;
						}

						if (Event.current.type == EventType.MouseDown && r.Contains(Event.current.mousePosition))
						{
							tilingRule.m_RuleTransform = (RuleTileAND.TilingRule.Transform)(((int)tilingRule.m_RuleTransform + 1) % 4);
							GUI.changed = true;
							Event.current.Use();
						}
					}
				}
			}
		}

		private static void OnSelect(object userdata)
		{
			MenuItemData data = (MenuItemData) userdata;
			data.m_Rule.m_RuleTransform = data.m_NewValue;
		}

		private class MenuItemData
		{
			public RuleTileAND.TilingRule m_Rule;
			public RuleTileAND.TilingRule.Transform m_NewValue;

			public MenuItemData(RuleTileAND.TilingRule mRule, RuleTileAND.TilingRule.Transform mNewValue)
			{
				this.m_Rule = mRule;
				this.m_NewValue = mNewValue;
			}
		}

		private void SpriteOnGUI(Rect rect, RuleTileAND.TilingRule tilingRule)
		{
			tilingRule.m_Sprites[0] = EditorGUI.ObjectField(new Rect(rect.xMax - rect.height, rect.yMin, rect.height, rect.height), tilingRule.m_Sprites[0], typeof (Sprite), false) as Sprite;

		}

		private void RuleInspectorOnGUI(Rect rect, RuleTileAND.TilingRule tilingRule)
		{
			float y = rect.yMin;
			EditorGUI.BeginChangeCheck();
			GUI.Label(new Rect(rect.xMin, y, k_LabelWidth, k_SingleLineHeight), "Rule");
			tilingRule.m_RuleTransform = (RuleTileAND.TilingRule.Transform)EditorGUI.EnumPopup(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tilingRule.m_RuleTransform);
			y += k_SingleLineHeight;
			GUI.Label(new Rect(rect.xMin, y, k_LabelWidth, k_SingleLineHeight), "Collider");
			tilingRule.m_ColliderType = (Tile.ColliderType)EditorGUI.EnumPopup(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tile.m_DefaultColliderType);
			y += k_SingleLineHeight;
			GUI.Label(new Rect(rect.xMin, y, k_LabelWidth, k_SingleLineHeight), "Output");
			tilingRule.m_Output = (RuleTileAND.TilingRule.OutputSprite)EditorGUI.EnumPopup(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tilingRule.m_Output);
			y += k_SingleLineHeight;

			if (tilingRule.m_Output == RuleTileAND.TilingRule.OutputSprite.Animation)
			{
				GUI.Label(new Rect(rect.xMin, y, k_LabelWidth, k_SingleLineHeight), "Speed");
				tilingRule.m_AnimationSpeed = EditorGUI.FloatField(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tilingRule.m_AnimationSpeed);
				y += k_SingleLineHeight;
			}
			if (tilingRule.m_Output == RuleTileAND.TilingRule.OutputSprite.Random)
			{
				GUI.Label(new Rect(rect.xMin, y, k_LabelWidth, k_SingleLineHeight), "Noise");
				tilingRule.m_PerlinScale = EditorGUI.Slider(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tilingRule.m_PerlinScale, 0.001f, 0.999f);
				y += k_SingleLineHeight;

				GUI.Label(new Rect(rect.xMin, y, k_LabelWidth, k_SingleLineHeight), "Shuffle");
				tilingRule.m_RandomTransform = (RuleTileAND.TilingRule.Transform)EditorGUI.EnumPopup(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tilingRule.m_RandomTransform);
				y += k_SingleLineHeight;
			}

			if (tilingRule.m_Output != RuleTileAND.TilingRule.OutputSprite.Single)
			{
				GUI.Label(new Rect(rect.xMin, y, k_LabelWidth, k_SingleLineHeight), "Size");
				EditorGUI.BeginChangeCheck();
				int newLength = EditorGUI.DelayedIntField(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tilingRule.m_Sprites.Length);
				if (EditorGUI.EndChangeCheck())
					Array.Resize(ref tilingRule.m_Sprites, Math.Max(newLength, 1));
				y += k_SingleLineHeight;

				for (int i = 0; i < tilingRule.m_Sprites.Length; i++)
				{
					tilingRule.m_Sprites[i] = EditorGUI.ObjectField(new Rect(rect.xMin + k_LabelWidth, y, rect.width - k_LabelWidth, k_SingleLineHeight), tilingRule.m_Sprites[i], typeof(Sprite), false) as Sprite;
					y += k_SingleLineHeight;
				}
			}
		}

		public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
		{
			if (tile.m_DefaultSprite != null)
			{
				Type t = GetType("UnityEditor.SpriteUtility");
				if (t != null)
				{
					MethodInfo method = t.GetMethod("RenderStaticPreview", new Type[] {typeof (Sprite), typeof (Color), typeof (int), typeof (int)});
					if (method != null)
					{
						object ret = method.Invoke("RenderStaticPreview", new object[] {tile.m_DefaultSprite, Color.white, width, height});
						if (ret is Texture2D)
							return ret as Texture2D;
					}
				}
			}
			return base.RenderStaticPreview(assetPath, subAssets, width, height);
		}

		private static Type GetType(string TypeName)
		{
			var type = Type.GetType(TypeName);
			if (type != null)
				return type;

			if (TypeName.Contains("."))
			{
				var assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));
				var assembly = Assembly.Load(assemblyName);
				if (assembly == null)
					return null;
				type = assembly.GetType(TypeName);
				if (type != null)
					return type;
			}

			var currentAssembly = Assembly.GetExecutingAssembly();
			var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
			foreach (var assemblyName in referencedAssemblies)
			{
				var assembly = Assembly.Load(assemblyName);
				if (assembly != null)
				{
					type = assembly.GetType(TypeName);
					if (type != null)
						return type;
				}
			}
			return null;
		}

		private static Texture2D Base64ToTexture(string base64)
		{
			Texture2D t = new Texture2D(1, 1);
			t.hideFlags = HideFlags.HideAndDontSave;
			t.LoadImage(System.Convert.FromBase64String(base64));
			return t;
		}
	}
}
