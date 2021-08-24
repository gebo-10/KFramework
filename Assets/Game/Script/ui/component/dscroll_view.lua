DScrollView = DScrollView or BaseClass(Control)

function DScrollView:__init(go,comp)
	self.go=go
	self.type = "DScrollView"

	self.event_listener=LuaFramework.EventTriggerListener.Get(go)
	self.comp_scrollview=comp or go:GetComponent("DynamicScrollView")

	
end

function DScrollView:__delete()
	self.event_listener=nil
end

--fun(index,go)  index0~n
function DScrollView:Bind(cell_num,fun )
	self.comp_scrollview.luaCallback=function (index,go)
		fun(index,Control.New(go))
	end
	self.comp_scrollview.totalItemCount=cell_num
	self.comp_scrollview:refresh ()
end

function DScrollView:SetNum(num)
	self.comp_scrollview.totalItemCount=num
end

function DScrollView:Refresh( )
	self.comp_scrollview:refresh ()
end

--scrollview移动到最上面
function DScrollView:ScrollByTop()
    TimerQuest.Instance():AddDelayQuest(function()
            self.comp_scrollview:scrollByItemIndex(0)
    end,0.05)
end
-- DScrollView = DScrollView or BaseClass()

-- function DScrollView:__init(go)
-- 	self.go=go
-- 	self.type = "DScrollView"

-- 	self.event_listener=LuaFramework.EventTriggerListener.Get(go)
-- 	self.comp_scrollview=go:GetComponent("ScrollPanel")

-- 	self:Gen(20,function (index, go )
-- 		log("SSSSCROLL",index)
-- 		local t=go.transform:Find("Text")
-- 		local text=Text.New(t)
-- 		text:SetText(index)
-- 	end)
-- end

-- function DScrollView:__delete()
-- 	--self.event_listener:ClearListener("onPointerClick")
-- 	self.event_listener=nil
-- end


-- function DScrollView:Gen(cell_num,fun )
-- 	self.comp_scrollview:Init(cell_num,fun)
-- end

-- -- function DScrollView:OnDrag(cb)
-- -- 	self.event_listener:ClearListener("onDrag")
-- -- 	self.event_listener.onDrag =self.event_listener.onDrag+cb
-- -- end
