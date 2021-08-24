PanelManager = PanelManager  or Singleton("PanelManager")

function PanelManager:__init()
	self.panelMgr = LuaHelper.GetPanelManager()

	if AppConst.Channel=="PrisonBreak" then
		local reporter=UnityEngine.GameObject.Find("Reporter")
		if reporter ~= nil then
			UnityEngine.GameObject.Destroy(reporter.gameObject)
		end
	end
	self.ui_width=UnityEngine.Screen.width
	self.ui_heght=UnityEngine.Screen.height

	self.uiroot=self.panelMgr.uiroot

	self.mask=Image.New(self.uiroot:Find("Mask").gameObject)
	self.mask.animation=self.mask.go:GetComponent("Animation")
	self.mask.ref=0

	self.extract=self.uiroot:Find("Extract").gameObject

	self.load_animation=self.uiroot:Find("LoadAnimation").gameObject

	self.canvas=UnityEngine.GameObject.Find("Canvas")
	self.screen_mask=self.canvas.transform:Find("ScreenMask").gameObject
	self.screen_mask_image=self.screen_mask:GetComponent("Image")
    --self.win_mask=self.canvas.transform:Find("WinMask").gameObject

	self.ui_left=0
	self.ui_right=0
	if LuaHelper.GetPlatform()=="IOS" and self.ui_width/self.ui_heght > 1334/750 then
		self.ui_left=88
		self.ui_right=68
	end

	--SysInfo.Instance():PrintInfo()

	self.view_list={}

	self.uid_ins=1000

	self.opening_num=0

	self:StartJudeMainui()
end

function PanelManager:ResetData() --resetdata 只清数据
	-- self:CloseAllPanel()
	-- self.view_list={}
end

function PanelManager:GetUID()
	self.uid_ins=self.uid_ins+1
	return self.uid_ins
end

function PanelManager:CreateView(view,panel,abname,cb)
	logw("PanelManager:CreateView ",panel)
	self.opening_num=self.opening_num+1	--用于判断当前正在打开的view数目
	self.panelMgr:CreatePanel(panel,abname,view._uid,function (is_success,go, uimap)
		self.opening_num=self.opening_num-1
		if is_success then
			local real_abname = abname or ""
			self.view_list[view._uid]=view
		end
		cb(is_success,go, uimap)
	end)
end

function PanelManager:DeleteView(view,name)
	logw("PanelManager:DeleteView  ",name)
	self.panelMgr:ClosePanel(view.go, view._uid)
	view.go=nil
	view.is_open=false
end

function PanelManager:CloseAllPanel()
	for k,v in pairs(self.view_list) do
		v:CloseView()
	end
	self.view_list={}
end

function PanelManager:GetUiRoot()
	return self.panelMgr.uiroot
end

--使用引用计数
function PanelManager:ShowMask(time)--todo 超过多少 秒 弹出重连 
	self.mask.ref=self.mask.ref+1
	if self.mask.ref >0 then
		self.mask:SetActive(true)
		self.mask.animation:Play()
	end
end

function PanelManager:HideMask()
	self.mask.ref=self.mask.ref-1
	if self.mask.ref <1 then
		self.mask.ref=0
		self.mask:SetActive(false)
	end
end

function PanelManager:ShowWinMask()
	--self.win_mask:SetActive(true)
end

function PanelManager:HideWinMask()
	--self.win_mask:SetActive(false)
end


function PanelManager:GetUiType(go)
	return self.panelMgr:GetUiType(go)
end


function PanelManager:SetAdapter(control)
	control:SetLeft(self.ui_left)
	control:SetRight(self.ui_right)
end

function PanelManager:ShowLoadAnimation()
	self.load_animation:SetActive(true)
end


function PanelManager:HideLoadAnimation()
	self.load_animation:SetActive(false)
end

function PanelManager:ShowScreenMask()
	log("[KEYPATH]: ShowScreenMask is Show")
	local os=LuaHelper.GetPlatform()
    if os=="pc" then return end
	self.screen_mask:SetActive(true)
end

function PanelManager:HideScreenMask()
	log("[KEYPATH]: ShowScreenMask is Hide")
	self.screen_mask:SetActive(false)
end



-- 全屏窗口黑影渐变
function PanelManager:DoFadeScreenMask(wintype,cb)
	if wintype~=UiPanel.Normal then return end
	-- self.screen_mask_image:DOKill()
	self.screen_mask:SetActive(true)
	self.screen_mask_image.color=Color(0,0,0,1)
    self.screen_mask_image:DOFade(0,0.4):OnComplete(function()
	    self.screen_mask_image.color=Color(0,0,0,1)
		self.screen_mask:SetActive(false)
	end)
end

-- 窗口弹出
function PanelManager:DoPopTopWin(is_open,control,fun,dic)
	if control==nil then return end
	dic=dic==nil and 1 or dic
	local height=control.rect_transform.rect.height
    local start_val= is_open and height or 0
    local end_val = is_open and 0 or height
	control.transform.localPosition=Vector3.New(0,dic*start_val,0)	
    control.transform:DOLocalMoveY(dic*end_val,0.2):OnComplete(function()fun()end)
end

function PanelManager:DoPopLeftWin(is_open,control,fun,dic)
	  if control==nil then return end
	  dic=dic==nil and -1 or dic
	  local width=control.rect_transform.rect.width/2
	  local start_val= is_open and width or 0
      local end_val = is_open and 0 or width 
   	  control.transform.localPosition=Vector3.New(dic*start_val,0,0)
   	  control.transform:DOKill()
      control.transform:DOLocalMoveX(dic*end_val,0.2):OnComplete(function()fun()end)	
end

function PanelManager:DoPopRightWin(is_open,control,fun)
	self:DoPopLeftWin(is_open,control,fun,1)
end

function PanelManager:SetI2LocalizeItem(go,item)
	self.panelMgr:SetI2LocalizeItem(go,item)
end

function PanelManager:GetI2LocalizeItem(name,idx)
	return self.panelMgr:GetI2LocalizeItem(name,idx)
end

--切换语言
function PanelManager:ChangeLanguage(language)
	self.panelMgr:ChangeLanguage(language)
end

function PanelManager:GetLanguage()
	return self.panelMgr:GetLanguage()
end

function PanelManager:UseLocalizationCSV(CSVfile)
	self.panelMgr:UseLocalizationCSV(CSVfile)
end

function PanelManager:DOCanvasGroupAlpha(go,start_val,end_val,fun)
    local component =go:GetComponent("CanvasGroup")
    component.alpha=start_val
    component:DOKill()
    component:DOFade(end_val,0.3):OnComplete(function()fun()end)	
end

function PanelManager:StartJudeMainui()
	self.mainui_show=false
	Event.Bind(EventEnum.EVENT_TIME_CLOCK,function()
		local is_top=self:JudgeMainuiShow()
		if self.mainui_show ~= is_top then
			logw("Mainui state change ",is_top)
			self.mainui_show=is_top
			Event.Fire(EventEnum.EVENT_MAINUI_SHOW_HIDE,is_top)
		end
	end)
end

function PanelManager:GetTop()
	local root=self:GetUiRoot()
	local num=root.transform.childCount
	return root.transform:GetChild(num-1).gameObject
end

function PanelManager:JudgeMainuiShow( )
	--logw("PanelManager:JudgeMainuiShow1")
	if GameState.Instance():GetCurrentStateName() ~= "MainState" then return false end
	--logw("PanelManager:JudgeMainuiShow2")
	if self.opening_num >0 then return false end
	--logw("PanelManager:JudgeMainuiShow3")
	if self.mask:IsActive() then return false end
	--logw("PanelManager:JudgeMainuiShow4")
	local top = self:GetTop()
	if top.name~="MainuiPanel" then return false end
	return true
end

function PanelManager:IsMainuiTop()
	return self.mainui_show
end

--是否是模拟器
function PanelManager:IsSimulator()
	local is_simulator=LuaHelper.GetSDKManager():IsSimulator()
	if not is_simulator then return false end
    local param={
		title=Language.Tip,
		content="The game does not support simulator running.",
		show_close=false,
		btns={
			{
				Language.Ok,function ()
					LuaHelper.QuitGame()
				end
			},
		}
	}
	DialogCtrl.Instance():OpenView(param)
	return true
end