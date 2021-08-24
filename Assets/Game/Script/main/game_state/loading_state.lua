LoadingState = LoadingState or BaseClass(StateBase)

function LoadingState:__init()
	self.name="LoadingState"
end

function LoadingState:Enter()
	PanelManager.Instance():ShowLoadAnimation()
	ResourceManager.Instance():PreLoad()
end

function LoadingState:Exit()

end

function LoadingState:Update()
	if ResourceManager.Instance():PreLoadFinish() then
		self:Login()
	end
end

function LoadingState:Login()
	Event.Fire(EventEnum.EVENT_LOADING_FINISH)
	if not Util.HasKey("rdkey") or Util.GetString("rdkey")=="" then--如果没有rdkey就不自动登录
		GameState.Instance():Translate("LoginState")
		return
	end

	if AppConst.Channel=="Dev" then--测试不自动登录
		GameState.Instance():Translate("LoginState")
	else--自动登录
		GameState.Instance():Translate("AutoLoginState")
	end
end