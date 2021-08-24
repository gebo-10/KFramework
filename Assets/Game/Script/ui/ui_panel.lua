UiPanel = UiPanel or BaseClass()

UiPanel.Normal=0
UiPanel.TopPopup=1
UiPanel.LeftPopup=2

Control.ControlType={
	Control=Control,
	Button=Button,
	Text=Text,
	Image=Image,
	DScrollView=DScrollView,
	ScrollView=ScrollView,
	InputField=InputField,
	Slider=Slider,
	Toggle=Toggle,
	ToggleGroup=ToggleGroup,
	Dropdown=Dropdown,
	Tab=Tab,
}

function UiPanel:__init(ctrl, data)
	self.asset_bundle=nil
	self.panel_name = nil
	self.ctrl = ctrl
	self.data = data
	self.view_type=UiPanel.Normal
	self._uid=PanelManager.Instance():GetUID()
	self._binds={}
	self._events={}
	self._timer={}
	self._controls={}
	self.go=nil
	self.is_open=false
	self.is_alpha=false
end
------------------------------------------------虚函数
function UiPanel:ResetData()
end

function UiPanel:LayoutCallback(go,...)
end

function UiPanel:OnOpen()
end

function UiPanel:OnClose(go)
end

function UiPanel:OnShow()
end

function UiPanel:OnHide()
end

function UiPanel:Update(deltaTime,now)--暂时没有接入
	
end
----------------------------------------------------
function UiPanel:__CreateCallback(go,...)
	if self.control_adapter ~= nil then
		PanelManager.Instance():SetAdapter(self.control_adapter)
	end
	self:LayoutCallback(go,...)
end

function UiPanel:OpenView(...)
	local panel_manager=PanelManager.Instance()
	if self.is_alpha then 
       panel_manager:DeleteView(self,self.panel_name.."Panel") 
       self.is_alpha=false
	end
	if self.is_open then 
		logw("Panel had opened ",self.panel_name)
		return
	end
	self:ResetData()
	self:OnOpen()
	local panel=self.panel_name.."Panel"
	local abname=""
	if self.asset_bundle == nil then
		abname=self.panel_name..".unity3d"
	else
		abname=self.asset_bundle..".unity3d"
	end
	local args = {...}

	PanelManager.Instance():ShowMask()
    -- PanelManager.Instance():DoFadeScreenMask(self.view_type) 	
	PanelManager.Instance():CreateView(self,panel,abname,function (is_success,go, uimap)
		PanelManager.Instance():HideMask()	
		if not is_success then
			loge("UiPanel:OpenView Load panel failed ",self.panel_name)
			return
		end
		self.go=go
		self:InitUiMap(uimap)
		PanelManager.Instance():DOCanvasGroupAlpha(go,0,1)
        
		self.is_open=true
		self:__CreateCallback(go,unpack(args))
		self:OnShow()
        self.pop_map ={
			 [0]=function()end,
			 [1]=panel_manager.DoPopTopWin,
			 [2]=panel_manager.DoPopLeftWin,
        }
        self.pop_map[self.view_type](panel_manager,self.is_open,self.control_popwin)
       
	end)
end

function UiPanel:InitUiMap(uimap)
	self._controls={}
	if uimap==nil then return end
	for i=1,uimap.Length do
		local item=uimap[i-1]
		--log("UiPanel:InitUiMap",item.name,item.type_name)
		local control= Control.ControlType[item.type_name].New(item.obj,item.component,item.rect)
		self[item.name]=control
		table.insert(self._controls,control)
	end
end

function UiPanel:CloseView()
	log("CloseView: ",self.panel_name)
	self:UnBindAllDataSource()--unbind data source
	self:UnBindAllEvent() --unbind events
	self:DestoryAllTimer() --destory timer

	if not self.is_open then
		self:DestoryAllControl() --destory controls
		PanelManager.Instance():DeleteView(self,self.panel_name.."Panel")
		return
	end
	self.is_alpha=true
	local  panel_manager= PanelManager.Instance()
    if self.view_type==UiPanel.Normal then
        panel_manager:DOCanvasGroupAlpha(self.go,1,0,function()
        	self:OnHide()
			self:OnClose()
		    self.is_open=false
        	self:DestoryAllControl() --destory controls
        	panel_manager:DeleteView(self,self.panel_name.."Panel") 
        	self.is_alpha=false
        end)
        return
    end    
    self.is_open=false
    self.pop_map[self.view_type](panel_manager,self.is_open,self.control_popwin,function()
    	self:OnHide()
		self:OnClose()
    	self:DestoryAllControl() --destory controls	
    	panel_manager:DeleteView(self,self.panel_name.."Panel") 
    	self.is_alpha=false
    end)
end

function UiPanel:IsOpen()
	return self.is_open
end

function UiPanel:SetActive(bool)
	if not self.is_open then return end
	if self.go.activeInHierarchy == bool then return end
	if bool then
		self:OnShow()
	else
		self:OnHide()
	end
	self.go:SetActive(bool)
end

function UiPanel:GetControl(name) --性能问题
	local transform=self.go.transform:Find(name)
	if transform ==nil then
		logw("UiPanel:GetControl get null name="..name)
		return
	end
	local ui_name=PanelManager.Instance():GetUiType(transform.gameObject)
	return Control.ControlType[ui_name].New(transform.gameObject)
end

------------------------------------------------------------
--gc 解除unity对象与lua对象的循环依赖
function UiPanel:DestoryAllControl()
	for i,v in ipairs(self._controls) do
		v:DeleteMe()
	end
	self._controls={}
end
------------------------------------------------------------
--绑定DataSource 接口
--key 是字符串
function UiPanel:BindControl(data_source, key,control)
	local process={
		Text=function (new,old)
			control:SetText(new)
		end,
		Slider=function (new,old)
			control:SetValue(new)
		end,
		Toggle=function (new,old)
			control:SetValue(new)
		end,
	}
	local proc=process[control.type]
	if proc ==nil then
		loge("can't bind control  type=",control.type)
	end
	local tag=data_source:Bind(key,proc)
    table.insert(self._binds,{data_source, key,tag})
end

--可以绑定别的DataSource
function UiPanel:BindFunction(data_source, key,fun)
	local tag=data_source:Bind(key,fun)
    table.insert(self._binds,{data_source, key,tag})
end

--绑定带单位的文本
function UiPanel:BindFotmatText(data_source,key,control)
	local tag=data_source:Bind(key,function(new,old)
		control:SetText(Util.FormatNum(new))
	end)
    table.insert(self._binds,{data_source, key,tag})
end

function UiPanel:UnBindAllDataSource()
	for k,v in pairs(self._binds) do
		v[1]:UnBind(v[2],v[3])
	end
	self._binds={}
end
------------------------------------------------------------
--绑定事件接口
function UiPanel:BindEvent(event_enum,fun)
	Event.Bind(event_enum,fun)
	table.insert(self._events,{event_enum,fun})
end

function UiPanel:UnBindAllEvent()
	for i,v in ipairs(self._events) do
		Event.Unbind(v[1],v[2])
	end
	self._events={}
end
------------------------------------------------------------
--定时器接口
function UiPanel:AddDelayQuest(quest_func, delay_time)
	local quest_id=TimerQuest.Instance():AddDelayQuest(quest_func, delay_time)
	table.insert(self._timer,quest_id)
	return quest_id
end

--period 是间隔 单位秒
--run_count 为-1时 就会一直跑
function UiPanel:AddTimesQuest(period, run_count, quest_func)
	local quest_id=TimerQuest.Instance():AddRunTimesQuest( quest_func, period, run_count)
	table.insert(self._timer,quest_id)
	return quest_id
end

--period 是间隔 单位秒
--last_time 为总时间 为-1时 就会一直跑
function UiPanel:AddPeriodQuest(period, last_time, quest_func)
	local quest_id=TimerQuest.Instance():AddPeriodQuest(quest_func, period, last_time)
	table.insert(self._timer,quest_id)
	return quest_id
end

function UiPanel:CancelQuest(id)
    TimerQuest.Instance():CancelQuest(id)
end

function UiPanel:DestoryAllTimer()
	for i,v in ipairs(self._timer) do
		self:CancelQuest( v )
	end
	self._timer={}
end
------------------------------------------------------------