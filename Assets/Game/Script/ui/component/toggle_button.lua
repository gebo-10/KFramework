ToggleButton = ToggleButton or BaseClass(Control)

function ToggleButton:__init(go,init,cb1,cb2)
	self.go=go
	self.type = "ToggleButton"
	self.component=go:GetComponent("Button")
	self.state=true
	
	self.button=Button.New(go)
	self.init_fun=init or function( btn ) --init
		btn.img1=btn.button:Find("Image1")
		btn.img2=btn.button:Find("Image2")

		btn.img1:SetActive(true)
		btn.img2:SetActive(false)
	end
		
	self.cb1=cb1 or function ( btn ) --cb1
		btn.img1:SetActive(true)
		btn.img2:SetActive(false)
	end

	self.cb2=cb2 or function ( btn ) --cb2
		btn.img1:SetActive(false)
		btn.img2:SetActive(true)
	end

	self:Bind(self.init_fun,self.cb1,self.cb2)
	self.on_change=function (state)end
end

function ToggleButton:Bind(init,cb1,cb2)
	self.init_fun=init
	self.cb1=cb1
	self.cb2=cb2
	self.init_fun(self)
	self:OnClick(function ( )
		if not self.state then
			self.cb1(self)
		else
			self.cb2(self)
		end
		self.state= not self.state
		self.on_change(self.state)
	end)
end

function ToggleButton:Interactable(bool)
	self.component.interactable=bool
end

function ToggleButton:OnChange(fun)
	self.on_change=fun
end

function ToggleButton:ChangeState(bool)
	self.state=bool
	if bool then
		self.cb1(self)
	else
		self.cb2(self)
	end
end

function ToggleButton:__delete()
	self.component.onClick:RemoveAllListeners()
end

function ToggleButton:OnClick(cb)
	self.component.onClick:RemoveAllListeners()
	self.component.onClick:AddListener(cb)
end

function ToggleButton:AddClickCallback(cb)
	self.component.onClick:AddListener(cb)
end

function ToggleButton:GetValue()
	return self.state
end
function ToggleButton:SetValue()
	self.state=not self.state
end