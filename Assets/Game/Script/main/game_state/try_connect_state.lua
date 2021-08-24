TryConnectState = TryConnectState or BaseClass(StateBase)

function TryConnectState:__init()
	self.name="TryConnectState"
	self.try_times=9
end

function TryConnectState:Enter( )
	self:StartTryConnect( )
end

function TryConnectState:Exit()
	self:StopTryConnect( )
end

function TryConnectState:Update()
	
end


function TryConnectState:OnConnect()
	--logw("OnConnect------->>>>"..self.name)
	self:StopTryConnect( )
	local status={
		uid=Util.GetString("uid"),
		rdkey=Util.GetString("rdkey"),
	}
	LoginCtrl.Instance():ProtoLogin(status)
end

function TryConnectState:OnConnectFail()
	--logw("OnConnectFail------->>>>"..self.name)
end

function TryConnectState:OnException()
   --logw("OnException------->>>>"..self.name)
   if self.reconnect_timer==nil then
   		GameState.Instance():Translate("ReconnectState")
   end
end

function TryConnectState:StartTryConnect( )
	log("TryConnectState:StartTryConnect( )")
	PanelManager.Instance():ShowMask(100000)
	Game.ResetData()
	local reconnect_times=0
	self.reconnect_timer=TimerQuest.Instance():AddPeriodQuest(function ( ) 
		reconnect_times=reconnect_times+1
		--log("TryConnectState:StartTryConnect 1")
		if reconnect_times == self.try_times then
			GameState.Instance():Translate("ReconnectState")
			return
		end
		--log("TryConnectState:StartTryConnect ",reconnect_times)
		local ip= Util.GetString("ip")
		local port= Util.GetString("port")
		Network.Connect(ip  ,port ,function ()end)
	end, 1,self.try_times+1)
end

function TryConnectState:StopTryConnect( )
	--log("TryConnectState:StopTryConnect( )")
	PanelManager.Instance():HideMask()
	if self.reconnect_timer ~= nil  then
		TimerQuest.Instance():CancelQuest( self.reconnect_timer )
		self.reconnect_timer=nil
	end
end