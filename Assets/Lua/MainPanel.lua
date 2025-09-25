BasePanel:subClass("MainPanel")

function MainPanel:Init(name)
    self.base.Init(self,name)
    if self.isInitEvent==false then
        self:GetControl("btnBegin","Button").onClick:AddListener(function()
            self:BtnBeginClick()
        end)
        self.isInitEvent=true
    end        
end
function MainPanel:HideMe()
    self.panelObj:SetActive(false)
end

function MainPanel:BtnBeginClick()
    SceneManager.LoadScene(1)
end