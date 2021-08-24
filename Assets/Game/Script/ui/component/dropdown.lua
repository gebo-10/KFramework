Dropdown = Dropdown or BaseClass()

function Dropdown:__init(go,comp)
	self.go=go
	self.type = "Dropdown"

	self.comp_dropdown=comp or go:GetComponent("Dropdown")
end

function Dropdown:__delete()
	LuaHelper.ClearDropdownCallback(self.comp_dropdown)
end

function Dropdown:OnValueChange(cb)
	LuaHelper.SetDropdownCallback(self.comp_dropdown, cb)
end

function Dropdown:GetValue()
	return self.comp_dropdown.value
end
function Dropdown:SetValue(value)
	self.comp_dropdown.value=value
end