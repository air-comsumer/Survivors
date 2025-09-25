BasePanel:subClass("ShopPanel")
gridTransform = {}
ShopPanel.items = {}
function  ShopPanel:Init(name)
    self.base.Init(self,name)

    if self.isInitEvent==false then
        -- self:GetControl("btnShop1","Button").onClick:AddListener(function()
        --     self:BtnShopClick()            
        -- end)
        gridTransform = self.panelObj:GetComponentInChildren(typeof(Grid)).gameObject.transform
        self.isInitEvent=true
        self.panelObj:SetActive(false)
    end

end

-- function ShopPanel:BtnShopClick()
--     print("购买武器")
--     GameManager:ReleaseScore()
--     self:HideMe()
-- end
function ShopPanel:ShowMe()
    if self.isInitEvent==false then
        Canvas = GameObject.Find("Canvas").transform
    end
    ShopPanel:Init("ShopPanel")
    if self.panelObj.activeSelf~=true then
        for i=1,#self.items do
            self.items[i]:Destroy()
        end
        self.items = {}
        for i=1,3 do
            local grid = ItemWeapon:new()
            grid:Init(gridTransform,"ItemShop",self.panelObj)
            local random = Random.Range(1,3)
            print("随机数"..random)
            grid:InitData(ItemData[math.floor(random)].id,ItemData[math.floor(random)].name)
            table.insert(self.items,grid)
        end
        self.panelObj:SetActive(true)
    end

end


