Toggle = Toggle or BaseClass()

function Toggle:__init(go,comp)
	self.go=go
	self.type = "Toggle"
	self.comp_toggle=comp or go:GetComponent("Toggle")
	self.event_listener=LuaFramework.EventTriggerListener.Get(go)
end
function Toggle:__delete()
	self.event_listener:ClearListener("onPointerClick")
	self.event_listener=nil
end

function Toggle:Interactable(bool)
	self.comp_toggle.interactable=bool
end

function Toggle:IsOn()
	return self.comp_toggle.isOn
end

function Toggle:On()
	self.comp_toggle.isOn = true
end

function Toggle:Off()
	self.comp_toggle.isOn = false
end

function Toggle:SetValue(value)
	self.comp_toggle.isOn=value
end

function Toggle:OnClick(cb)
	self.event_listener:ClearListener("onPointerClick")
	self.event_listener.onPointerClick =self.event_listener.onPointerClick+cb
end

function Toggle:SetIsOnWithoutNotify(bool)
	self.comp_toggle:SetIsOnWithoutNotify(bool)
end