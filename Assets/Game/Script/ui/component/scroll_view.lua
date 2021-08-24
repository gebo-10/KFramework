ScrollView = ScrollView or BaseClass(Control)

function ScrollView:__init(go)
	self.go=go
	self.type = "ScrollView"

	self.event_listener=LuaFramework.EventTriggerListener.Get(go)
	self.comp_scrollview=go:GetComponent("ScrollRect")

end

function ScrollView:__delete()
	self.event_listener:ClearListener("onDrag")
	self.event_listener=nil
end

function ScrollView:OnDrag(cb)
	self.event_listener:ClearListener("onDrag")
	self.event_listener.onDrag =self.event_listener.onDrag+cb
end

