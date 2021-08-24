Slider = Slider or BaseClass()

function Slider:__init(go,comp)
	self.go=go
	self.type = "Slider"
	self.comp_slider=comp or go:GetComponent("Slider")
	self.event_listener=LuaFramework.EventTriggerListener.Get(go)
end

function Slider:__delete()
	self.event_listener:ClearListener("onDrag")
	self.event_listener=nil
	LuaHelper.ClearSliderCallback(self.comp_slider)
end

function Slider:OnDrag(cb)
	self.event_listener:ClearListener("onDrag")
	self.event_listener.onDrag =self.event_listener.onDrag+cb
end

function Slider:OnValueChange(cb)
	LuaHelper.SetSliderCallback(self.comp_slider,cb)
end

function Slider:SetValue(value)
	self.comp_slider.value=value
end

function Slider:SetMinValue(value)
	self.comp_slider.minValue=value
end

function Slider:SetMaxValue(value)
	self.comp_slider.maxValue=value
end

function Slider:GetValue()
	return self.comp_slider.value
end