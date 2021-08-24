Text = Text or BaseClass(Control)

function Text:__init(go,comp)
	self.go=go
	self.type = "Text"
	self.comp_text=comp or go:GetComponent("TextMeshProUGUI")
	if self.comp_text==nil then 
       self.comp_text=go:GetComponent("Text")
	end
end

function Text:SetText(str)
	self.comp_text.text=str
end

function Text:GetText(str)
	return self.comp_text.text
end

function Text:SetSize(size)
	self.comp_text.fontSize=size
end

function Text:SetColor(color)
	self.comp_text.color=color
end

function Text:SetRGBA(r,g,b,a) --#445566

end
function Text:SetColorCode(color) --#445566

end

