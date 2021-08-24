local panelMgr = LuaHelper.GetPanelManager()

Control = Control or BaseClass()

function Control:__init(go,comp,rect)
	self.type="Control"
	self.go=go
	self.component=comp
	assert(go ~= nil ,"Control gameObject == nil")
	self.transform=go.transform
	self.rect_transform=rect or go:GetComponent("RectTransform")
	self:InitUiMap( )


	-- local mt=getmetatable(go)
	-- local old_gc=mt.__gc
	-- mt.__gc=function ()
	-- 	self:__gc()
	-- 	old_gc()
	-- end
end

--虚函数
function Control:__gc()
	--loge("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"..self.type)
end

function Control:__delete()
	--析构牌
	-- if self.has_delete then return end
	--logw("Control:__delete name="..self.go.name)
	-- self.has_delete=true

	self:DestoryAllControl()
	self.go.transform:SetParent(nil)
	UnityEngine.GameObject.Destroy(self.go)
end

------------------------------------------------------------
--gc 解除unity对象与lua对象的循环依赖
function Control:DestoryAllControl()
	for i,v in ipairs(self._controls) do
		v:DeleteMe()
	end
	self._controls={}
end
------------------------------------------------------------

function Control:InitUiMap( )
	self._controls={}
	local types=Control.ControlType
	local comp_uimap=self.go:GetComponent("LuaUiMap")
	if comp_uimap==nil then return end
	local uimap=comp_uimap:GetUiList()
	for i=1,uimap.Length do
		local item=uimap[i-1]
		--log("Control:InitUiMap",item.name,item.type:ToInt(),ControlType)
		assert(item.obj ~= nil,"item.name="..item.name)
		local control = types[item.type_name].New(item.obj,item.component,item.rect)
		self[item.name]=control
		table.insert(self._controls,control)
	end
end

function Control:SetActive(bool)
	self.go:SetActive(bool)
end

function Control:SetName(name)
	self.go.name=name
end

function Control:IsActive()
	return self.go.activeSelf
end

function Control:GetActiveSelf()
	return self.go.activeSelf
end

function Control:SetParent(parent,b)
	if b==nil then b=false end
	self.transform:SetParent(parent,b)
end

function Control:AddChild(control,b)
	if b==nil then b=false end
	control.transform:SetParent(self.transform,b)
end

function Control:RemoveAllChildren()
	local children = self:GetChildren()
	for i,v in ipairs(children) do
		v:SetParent(nil)
		UnityEngine.GameObject.Destroy(v.gameObject)
	end
end


function Control:SetScale(vec3)
	self.transform.localScale(vec3)
end

function Control:GetSize()
	return self.rect_transform.sizeDelta
end

function Control:SetSize(width,height)
	self.rect_transform.sizeDelta=Vector2.New(width,height)
end

function Control:SetLeft(v)
	self.rect_transform.offsetMin=Vector2.New(v,self.rect_transform.offsetMin.y);
end

function Control:SetBottom(v)
	self.rect_transform.offsetMin=Vector2.New(self.rect_transform.offsetMax.x,v);
end

function Control:SetRight(v)
	self.rect_transform.offsetMax=Vector2.New(-v,self.rect_transform.offsetMax.y);
end

function Control:SetTop(v)
	self.rect_transform.offsetMax=Vector2.New(self.rect_transform.offsetMax.x,v);
end

function Control:SetPosition(x,y)
	self.rect_transform.anchoredPosition=Vector2.New(x,y)
end

function Control:GetPositionVector()
	return self.rect_transform.anchoredPosition
end

function Control:SetGlobalPosition(pos)
	self.transform.position=pos
end

function Control:GetGlobalPosition()
	return self.transform.position
end

function Control:SetPositionVector(pos)
	self.rect_transform.anchoredPosition=pos
end

function Control:MoveTo(vec2,time,cb)
	self.go.transform:SetAsLastSibling()
	local ani=self.rect_transform:DOAnchorPos(vec2, time or 0.1, false)
	if cb ~= nil then
		ani:OnComplete(cb)
	end
end

function Control:Switch(index) --choose child  0-n
	local children= self:GetChildren()
	for i,v in ipairs(children) do
		if i==index+1 then
			v:SetActive(true)
		else
			v:SetActive(false)
		end
	end
end

function Control:ChangeState(index)
	if self.state_manager==nil then
		self.state_manager=self.go:GetComponent("StateManager")
	end
	self.state_manager:Translate(index)
end

function Control:NextState()
	if self.state_manager==nil then
		self.state_manager=self.go:GetComponent("StateManager")
	end
	self.state_manager:Next()
end

function Control:GetComponent(name)
	return self.go:GetComponent(name)
end


function Control:PreState()
	if self.state_manager==nil then
		self.state_manager=self.go:GetComponent("StateManager")
	end
	self.state_manager:Pre()
end

function Control:GetChildren()
	local children={}
	local num=self.go.transform.childCount
	for i=1,num do
		children[i]=self:GetChildIndex(i-1)
	end
	return children
end
function Control:Find(path )
	local t=self.go.transform:Find(path)
	if t == nil then
		logw("Control:Find get null name= "..path)
		return nil
	end
	return self:ToLuaControl( t.gameObject )
end

function Control:GetChildIndex(index)
	local go=self.go.transform:GetChild(index).gameObject
	return self:ToLuaControl( go )
end

function Control:OnClick(cb)
	if self.event_listener == nil then
		self.event_listener=LuaFramework.EventTriggerListener.Get(self.go)
	end
	self.event_listener:ClearListener("onPointerClick")
	self.event_listener.onPointerClick =self.event_listener.onPointerClick+cb
end

function Control:ToLuaControl( go )
	local type_name=panelMgr:GetUiType(go)
	return Control.ControlType[type_name].New(go)
end

function Control:As( Type )
	return Type.New(self.go)
end