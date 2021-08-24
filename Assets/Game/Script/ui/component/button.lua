Button = Button or BaseClass(Control)

function Button:__init(go,comp)
	self.go=go
	self.type = "Button"
	self.component=comp or go:GetComponent("Button")
	self.last_time=0
	-- Util.Debug(function ( ... )
	-- 	self:OnClick(function ()  --release的时候关闭
	-- 		loge("button have no action")
	-- 	end)
	-- end)

end

function Button:Interactable(bool)
	self.component.interactable=bool
end

function Button:__delete()
	self.component.onClick:RemoveAllListeners()
end

function Button:OnClick(cb)
	self.component.onClick:RemoveAllListeners()
	self.component.onClick:AddListener(function ()
		local now=Game.Time.now
		
		if now - self.last_time < 0.3 then
			return
		end
		self.last_time=now
		SoundManager.Instance():PlayEffect("button1")
		cb()
	end)
end

function Button:SetEnable(isTrue)
	self.component.enabled=isTrue
end

function Button:AddClickCallback(cb)
	self.component.onClick:AddListener(cb)
end