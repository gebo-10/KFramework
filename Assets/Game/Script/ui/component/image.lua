Image = Image or BaseClass(Control)

function Image:__init(go,comp)
	self.go=go
	self.type = "Image"
	self.comp_image=comp or go:GetComponent("Image")
end

function Image:SetTexture(texture)
	assert(texture ~= nil,"Image:SetTexture texture==nil")
	self.comp_image.sprite=texture
end

function Image:SetSprite(texture)
	self.comp_image.sprite=texture
end

function Image:SetColor(color)
	self.comp_image.color=color
end

function Image:SetAmount( amount )
	self.comp_image.fillAmount=amount
end

function Image:GetAmount()
	return self.comp_image.fillAmount
end

function Image:DoColor(color,time)
	return self.comp_image:DOColor(color,time)
end

function Image:DoFade(value,time)
	return self.comp_image:DOFade(value,time)
end

function Image:SetEnable(isTrue)
	self.comp_image.enabled=isTrue
end

function Image:SetNativeSize()
	self.comp_image:SetNativeSize()
end
