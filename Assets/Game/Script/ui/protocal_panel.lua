ProtocalPanel = ProtocalPanel or BaseClass(UiPanel)

function ProtocalPanel:__init()

	self.msg=nil
end

-- function ProtocalPanel:LayoutCallback() --虚函数
-- end

function ProtocalPanel:NetworkCallback() --虚函数

end

function ProtocalPanel:AllCallback() --虚函数

end
---------------------------------------------------------
function ProtocalPanel:OpenView()

	UiPanel.OpenView(self)
end

function ProtocalPanel:__CreateCallback(go,...) --private
	--
	UiPanel.__CreateCallback(self,go,...)
end

function ProtocalPanel:__NetworkCallback() --private

	self:NetworkCallback()
end

function ProtocalPanel:__AllCallback() --private

	self:AllCallback()
end

-----------------------------------------------
ProcessHub = ProcessHub or BaseClass()
function ProcessHub:__init(cb)

end

function ProcessHub:AddProcess()

end

function ProcessHub:OnEvent(event)

end