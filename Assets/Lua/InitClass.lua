--常用别名定位
require("Object")
--字符串拆分
require("SplitTools")
--Json解析
Json = require("JsonUtility")
--Unity相关
GameObject = CS.UnityEngine.GameObject
Resources = CS.UnityEngine.Resources
Transform = CS.UnityEngine.Transform
RectTransform = CS.UnityEngine.RectTransform
TextAsset = CS.UnityEngine.TextAsset
SpriteAtlas = CS.UnityEngine.U2D.SpriteAtlas
Vector2 = CS.UnityEngine.Vector2
Vector3 = CS.UnityEngine.Vector3
Random = CS.UnityEngine.Random
UI = CS.UnityEngine.UI
Image = UI.Image
Text = UI.Text
Button = UI.Button
Toggle = UI.Toggle
ScrollRect = UI.ScrollRect
UIBehavior = CS.UnityEngine.EventSystems.UIBehaviour
Grid = CS.UnityEngine.UI.GridLayoutGroup
--Canvas对于这个项目只要找一次就行了
Canvas = GameObject.Find("Canvas").transform
--直接得到AB包资源管理器单例对象
ABMgr = CS.ABMgr.GetInstance()
Canvas = GameObject.Find("Canvas").transform
--Scene相关
SceneManager = CS.UnityEngine.SceneManagement.SceneManager
GameManager = CS.GameManager()
WeaponLayout = CS.WeaponLayout()
