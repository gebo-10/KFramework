ToggleGroup = ToggleGroup or BaseClass()

function ToggleGroup:__init(go)
	self.go=go
	
	self.control=Control.New(go)
	self.toggles=self.control:GetChildren()
	self.index=#self.toggles
	for i,v in ipairs(self.toggles) do
		v:OnClick(function()
			self.index=i
			log("QQQQQQQQQQToggleGroup",i)
		end)
	end
end
function ToggleGroup:__delete()

end

function ToggleGroup:GetIndex()
	return self.index
end
