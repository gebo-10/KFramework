LoginState = LoginState or BaseClass(StateBase)

function LoginState:__init()
	self.name="LoginState"
end

function LoginState:Enter( )
	Game.ResetData()
	Game.Cleanup()
	PanelManager.Instance():HideLoadAnimation()
	PanelManager.Instance():CloseAllPanel()

	LoginCtrl.Instance():OpenPlatformView()
	ProcessManager.Instance():BrokenAll()
end

function LoginState:Exit()
	LoginCtrl.Instance():CloseView()
	LoginCtrl.Instance():ClosePlatformView()
end

function LoginState:Update()
	
end

--当连接不上时候--
function LoginState:OnConnectFail() 
    loge("Game Server connect fail!! "..self.name);
end

function LoginState:OnException()

end

--当连接上时候--
function LoginState:OnConnect() 
    
end

--当连接上时候--
function LoginState:OnDisconnect() 
    
end
