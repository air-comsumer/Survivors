BasePanel:subClass("ItemWeapon")

function ItemWeapon:Init(father,name,shopObj)
    self.base.Init(self,name)
    self.panelObj.transform:SetParent(father,false) --设置父对象
    if self.isInitEvent==false then
        self:GetControl("btnShop","Button").onClick:AddListener(function()
            self:BtnShopClick(shopObj)
        end)
        self.isInitEvent = true
    end
end

function ItemWeapon:BtnShopClick(shopObj)
    print("购买武器")
    GameManager:ReleaseScore()
    WeaponLayout:AddWeapon()
    shopObj:SetActive(false)
    CS.UnityEngine.Time.timeScale=1
end
function ItemWeapon:InitData(id,name)
    --通过道具ID读取道具配置表
    local itemData = ItemData[id]
    --根据名字先加载图集再加载图集中的图标信息
    local strs = string.split(itemData.icon,"_")
    local spriteAtlas = ABMgr:LoadRes("ui",strs[1],typeof(SpriteAtlas))--加载图集
    self:GetControl("imgIcon","Image").sprite = spriteAtlas:GetSprite(strs[2])
    self:GetControl("txtItem","Text").text = name
end

function ItemWeapon:Destroy()
    GameObject.Destroy(self.panelObj)
    self.panelObj = nil
end
