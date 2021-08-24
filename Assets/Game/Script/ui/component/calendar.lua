Calendar = Calendar or BaseClass(Control)

function Calendar:__init(go)
	self.go=go
	self.type = "Calendar"
	self.component=go:GetComponent("DatePicker")
end

function Calendar:__delete()
	LuaHelper.SetDataPickerCallback(self.component,nil)
end

function Calendar:OnSelect(cb) --cb(时间戳,时间字符串)
	LuaHelper.SetDataPickerCallback(self.component,cb)
end

function Calendar:SetRange(str_from,str_to)
	LuaHelper.SetDataPickerRange(self.component,str_from,str_to)
end