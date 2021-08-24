MainState = MainState or BaseClass(StateBase)

function MainState:__init()
	self.name="MainState"
	self.last_heart_beat=0
end

function MainState:Enter( )
	self.last_heart_beat=Game.Time.now
	
	self:StartHeartBeat()

	PanelManager.Instance():HideMask()
	ProcessManager.Instance():BrokenAll()

	CommonProtocal.Instance():GetServerTimeStampREQ()

	require("logic.userinfo.user_info_ctrl")
	--UserInfoCtrl.Instance().set_view:Init()

	ProcessManager.Instance():Start("login_room_process")
	LoginProtocal.Instance():UserRoomREQ()--请求继续上一把
	
	--ClubProtocal.Instance():ClubListREQ()
	UserInfoCtrl.Instance():RequestUserInfo()
	HallProtocal.Instance():GetUserSignInfoREQ(UserInfoCtrl.Instance():GetUid())--获取签到信息

	require "logic.mainui.main_ui_ctrl"
	MainuiCtrl.Instance():OpenView()
end

function MainState:Exit()
	self:StopHeartBeat()
end

function MainState:Update()
	if Game.Time.now-self.last_heart_beat > 12 then
		loge("MainState heart beat timeout")
		GameState.Instance():Translate("ReconnectState")
	end
end

function MainState:OnConnect()

end

function MainState:OnConnectFail()

end

function MainState:OnException()
	GameState.Instance():Translate("TryConnectState")
end

--连接中断，或者被踢掉--
function MainState:OnDisconnect()
	Event.Fire(EventEnum.EVENT_NETWORK_DISCONNECT)
	GameState.Instance():Translate("ReconnectState")
end


function MainState:StartHeartBeat()
	logw(" MainState:StartHeartBeat()")

	Event.Bind(EventEnum.EVENT_HEARTBEAT_RSP,function ()
		--logw("On heart beat")
		self.last_heart_beat=Game.Time.now
	end)

	if self.heart_beat_timer ~= nil  then
		TimerQuest.Instance():CancelQuest( self.heart_beat_timer )
	end
	require "Protol/login_pb"
	local beat = Protol.login_pb.HeartBeatREQ();
	self.heart_beat_timer=TimerQuest.Instance():AddPeriodQuest(function ( ) --3秒心跳
		Network.SendProto(beat)
	end, 3,-1)
end

function MainState:StopHeartBeat()
	logw(" MainState:StopHeartBeat()")
	Event.Unbind(EventEnum.EVENT_HEARTBEAT_RSP)
	if self.heart_beat_timer ~= nil  then
		TimerQuest.Instance():CancelQuest( self.heart_beat_timer )
		self.heart_beat_timer=nil
	end
end

