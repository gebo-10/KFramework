Tip = Tip  or Singleton("Tip")

Tip.Error=0
Tip.Warning=1
Tip.Normal=2

function Tip:__init()
	local uiroot=PanelManager.Instance():GetUiRoot()
	self.tip_root=Image.New(uiroot:Find("UiUp/Tip").gameObject)
	self.tip_txt=Text.New(uiroot:Find("UiUp/Tip/Text").gameObject)
	self.bg=Image.New(uiroot:Find("UiUp/Tip").gameObject)
	self.animation=self.tip_root:GetComponent("Animation")
end
--todo 添加type 添加队列
function Tip:PushTip(str,type)
	if type==nil or type==Tip.Error then 
		self.bg:SetColor(Color.red)
		self.tip_txt:SetColor(Color.red)
	elseif type==Tip.Warning then
		self.bg:SetColor(Color.yellow)
		self.tip_txt:SetColor(Color.yellow)
	elseif type==Tip.Normal then
		self.bg:SetColor(Color.green)
		self.tip_txt:SetColor(Color.green)
	end
	self.tip_txt:SetText(str)
	self.animation:Play()
end