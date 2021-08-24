Tab = Tab or BaseClass(Control)

function Tab:__init(go)
	self.go=go
	self.type = "Tab"
	
	local tab_buttons_root=self:Find("Head/TabButtons")
	self.tab_buttons=tab_buttons_root:GetChildren()
	self.selected=self:Find("Head/bg/Selected")
	local view_root=self:Find("View")
	self.views=view_root:GetChildren()

	for i,v in ipairs(self.tab_buttons) do
		v:OnClick(function ()
			self:Show(i)
		end)
	end
	self.on_tab_fun=function ()	end
	self:Show(1)
end


function Tab:Show(index)
	self.index=index
	local size=self.selected:GetSize()
	local len=size.x
	local total_len=len* #self.tab_buttons

	for i,view in ipairs(self.views) do
		view:SetActive(false)
	end
	self.views[index]:SetActive(true)
	self.selected:MoveTo(Vector2.New(( (index-1)*len+len/2) - (total_len/2),0))
	self.on_tab_fun(index)
end

function Tab:GetIndex()
	return self.index
end

function Tab:OnTab(fun)
	self.on_tab_fun=fun
end

function Tab:__delete()

end

