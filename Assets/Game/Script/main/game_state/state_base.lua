StateBase = StateBase or BaseClass()

function StateBase:__init()
	self.name="StateBase"
end

function StateBase:Enter( )
	
end

function StateBase:Exit()
	
end

function StateBase:Update()
	
end

function StateBase:OnConnect()
    --logw("OnConnect------->>>>"..self.name);
end

function StateBase:OnException()
    --logw("OnException------->>>>"..self.name);
end

--当连接不上时候--
function StateBase:OnConnectFail() 
    --logw("Game Server connect fail!! "..self.name);
end


--连接中断，或者被踢掉--
function StateBase:OnDisconnect()
    --logw("OnDisconnect------->>>>"..self.name);
end


--连接中断，或者被踢掉--
function StateBase:OnApplicationPause(bool)
    --logw("OnApplicationPause------->>>>"..self.name);
end

--连接中断，或者被踢掉--
function StateBase:OnApplicationFocus(bool)
    --logw("OnApplicationFocus------->>>>"..self.name);
end