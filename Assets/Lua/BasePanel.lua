Object:subClass("BasePanel")
BasePanel.panelObj = nil
BasePanel.controls = {}
BasePanel.isInitEvent = false

function BasePanel:Init(name)
    if(self.panelObj ==nil) then
        self.panelObj = ABMgr:LoadRes("ui",name,typeof(GameObject))
        self.panelObj.transform:SetParent(Canvas,false)
        local allControls = self.panelObj:GetComponentsInChildren(typeof(UIBehavior))
        for i =0,allControls.Length-1 do
            local controlName = allControls[i].name
            if  string.find(controlName,"btn")~=nil or
                string.find(controlName,"txt")~=nil or
                string.find(controlName,"img")~=nil then
                local typeName = allControls[i]:GetType().Name
                if(self.controls[allControls[i].name]~=nil) then
                    self.controls[controlName][typeName]=allControls[i]
                else
                    self.controls[controlName]={[typeName]=allControls[i]}
                end
            end
        end
    end
end

--通过名字和类型得到控件
function BasePanel:GetControl(name,typeName)
    if self.controls[name]~=nil then
        local sameNameControls = self.controls[name]
        if sameNameControls[typeName]~=nil then
            return sameNameControls[typeName]
        end
    end
    return nil
    
end
function BasePanel:ShowMe(name)
    self:Init(name)
    self.panelObj:SetActive(true)
end
function BasePanel:HideMe()
    self.panelObj:SetActive(false)
end