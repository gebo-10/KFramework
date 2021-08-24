GameState = GameState  or Singleton("GameState")

function GameState:__init()
	log("GameState init")
	self.state_list={}
	self.current_state=nil

	namespace(GameState)
	require "logic.game_state.state_base"
	require "logic.game_state.auto_login_state"
	require "logic.game_state.login_state"
	require "logic.game_state.loading_state"
	require "logic.game_state.reconnect_state"
	require "logic.game_state.main_state"
	require "logic.game_state.try_connect_state"

	self:AddState(LoginState.New())
	self:AddState(LoadingState.New())
	self:AddState(ReconnectState.New())
	self:AddState(MainState.New())
	self:AddState(AutoLoginState.New())
	self:AddState(TryConnectState.New())

	namespace()
end

function GameState:AddState(state)
	self.state_list[state.name]=state
end

function GameState:InitState(name)
	self.current_state=self.state_list[name]
	self.current_state:Enter()
end

function GameState:Translate(name)
	log("[KEYPATH] GameState:Translate ",name)
	if self.current_state.name==name then
		logw("[KEYPATH] GameState:Translate Error ,In-current",name)
		return
	end
	self.current_state:Exit()
	self.current_state=self.state_list[name]
	self.current_state:Enter()
end

function GameState:Update(deltaTime)
	self.current_state:Update(deltaTime)
end

function GameState:GetCurrentStateName()
	return self.current_state.name
end
--------------------------------------------------------------------------------------
function GameState:OnConnect()
    log("GameState OnConnect------->>>>",self.current_state.name);
    self.current_state:OnConnect()
end

function GameState:OnException()
    logw("GameState OnException------->>>>",self.current_state.name);
    self.current_state:OnException()
end

--当连接不上时候--
function GameState:OnConnectFail() 
    logw("GameState Game Server connect fail!! ",self.current_state.name);
    self.current_state:OnConnectFail()
end

--连接中断，或者被踢掉--
function GameState:OnDisconnect()
    logw("GameState OnDisconnect------->>>>",self.current_state.name);
    self.current_state:OnDisconnect()
end
--------------------------------------------------------------------------------------
function GameState:OnApplicationPause(bool)
    logw("GameState OnApplicationPause------->>>>",self.current_state.name);
    --self.current_state:OnApplicationPause(bool)
    if not is_pause then
		self:DelayCheck()
	end
end

function GameState:OnApplicationFocus(bool)
    logw("GameState OnApplicationFocus------->>>>",self.current_state.name);
    --self.current_state:OnApplicationFocus(bool)
    if is_focus then
		self:DelayCheck()
	end
end

function GameState:DelayCheck()
	if self.timer ~= nil then
		TimerQuest.Instance():CancelQuest( self.timer )
		self.timer=nil
	end
	self.timer=TimerQuest.Instance():AddDelayQuest(function()
		if GameState.Instance():GetCurrentStateName() == "ReconnectState" then
			LoginCtrl.Instance():CloseReconnectView()
			LoginCtrl.Instance():Reconnect()
		end
	end, 0.5)
	
end