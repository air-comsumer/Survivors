local txt = ABMgr:LoadRes("json","ItemDataForSurvivor",typeof(TextAsset))
local itemList = Json.decode(txt.text)
ItemData = {}
for _,value in pairs(itemList) do
    ItemData[value.id] = value
end
for key,value in pairs(ItemData) do
    print(key,value.name)
end