ReconnectState = ReconnectState or BaseClass(StateBase)

function ReconnectState:__init()
	self.name="ReconnectState"
end

function ReconnectState:Enter( )
	LoginCtrl.Instance():OpenReconnectView()

end

function ReconnectState:Exit()
	ProcessManager.Instance():Step("reconnect_process","reconnect_over")
end

function ReconnectState:Update()
	
end

function ReconnectState:OnException()
   GameState.Instance():Translate("LoginState")
end


-- function ReconnectState:OnApplicationPause(is_pause)
-- 	if not is_pause then
-- 		self:DelayCheck()
-- 	end
-- end

-- function ReconnectState:OnApplicationFocus(is_focus)
-- 	if is_focus then
-- 		self:DelayCheck()
-- 	end
-- end
-- ----------------------------------------------------
-- function ReconnectState:DelayCheck()
-- 	if self.timer ~= nil then
-- 		TimerQuest.Instance():CancelQuest( self.timer )
-- 		self.timer=nil
-- 	end
-- 	self.timer=TimerQuest.Instance():AddDelayQuest(function()
-- 		if GameState.Instance():GetCurrentStateName() == self.name then
-- 			LoginCtrl.Instance():CloseReconnectView()
-- 			LoginCtrl.Instance():Reconnect()
-- 		end
-- 	end, 0.2)
	
-- end