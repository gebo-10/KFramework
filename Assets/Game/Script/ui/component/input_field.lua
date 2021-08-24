InputField = InputField or BaseClass(Control)

function InputField:__init(go,comp)
	self.go=go
	self.type = "InputField"
	self.comp_input_field=comp or go:GetComponent("InputField")
	if self.comp_input_field == nil then
		self.comp_input_field=go:GetComponent("TMP_InputField")
	end
	self.event_listener=LuaFramework.EventTriggerListener.Get(go)
end

function InputField:__delete()
	self.event_listener=nil
	LuaHelper.ClearInputCallback(self.comp_input_field)
end

function InputField:GetValue()
	return self.comp_input_field.text
end

function InputField:SetValue(txt)
	self.comp_input_field.text = txt
end

function InputField:OnValueChange(cb)
	LuaHelper.SetInputCallback(self.comp_input_field,cb)
end

--输入框结束编辑时调用
function InputField:OnEndEdit(cb)
	LuaHelper.OnEndEdit(self.comp_input_field,cb)
end

--光标是否移动到最后
function InputField:MoveTextEnd(b)
	self.comp_input_field:MoveTextEnd(b)
end

--激活焦点
function InputField:ActivateInputField()
	self.comp_input_field:ActivateInputField()
end

--获取是否在焦点上
function InputField:IsFocused()
	return self.comp_input_field.isFocused
end