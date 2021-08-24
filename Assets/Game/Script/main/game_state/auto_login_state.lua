AutoLoginState = AutoLoginState or BaseClass(StateBase)

function AutoLoginState:__init()
	self.name="AutoLoginState"
end

function AutoLoginState:Enter()
	TimerQuest.Instance():AddDelayQuest(function()
		LoginCtrl.Instance():AutoLogin()
  	end,1)
end

function AutoLoginState:Exit()
	
end

function AutoLoginState:Update()
	
end