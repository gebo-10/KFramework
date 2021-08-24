require "base.namespace"
require "base.baseclass"
require "base.singleton"
require "base.datasource"

require "system.game_entry"
require "system.playerprefs"
local CJson = require('cjson')
local pb = require "pb"
local protoc = require "protocal.pb.protoc"

function Main()
	Test()
	-- GameEntry.timer:Delay(3, function()
	-- 	CS.UnityEngine.Debug.LogWarning('Delay')
	-- end)

	-- GameEntry.timer:AddTimer(3,2, function()
	-- 	CS.UnityEngine.Debug.LogWarning('AddTimer')
	-- end)

	local ani = GameObject.Find("ani")
	local ctr=ani:GetComponent(typeof(CS.UnityEngine.Animator))
	
	local state=0
	GameEntry.timer:Loop(3, function()
		state=(state+1)%3
		ctr:SetInteger("state", state);
	end)

	PlayerPrefs.Instance():Set("a",11)
	
	--TestProto()
	TestSocket()
end

function Update( )
	--print("Update")
end

function OnMessage(msg_type,param)
	print("XXXXXXXXXXXXXXXXXX",msg_type)
	print("ccccccccccccc", msg_type:ToString())--CS.System.Convert.ToInt32(msg_type)
	for k,v in pairs(param) do
		print(k,v)
	end
	if msg_type == CS.MessageType.Proto then
		local data2 = assert(pb.decode("pb.UserLoginRSP", param.buffer))
		print(require "protocal.pb.serpent".block(data2))
		print(CJson.encode(data2))
	end
end

GameObject=CS.UnityEngine.GameObject
Assets=CS.kassets.Assets
function Test()
	local req=Assets.LoadAssetAsync("Assets/Game/Main/Panel/PanelMain.prefab", typeof(GameObject))
    req.completed=function (request )
    	local go = CS.UnityEngine.Object.Instantiate(request.asset  );
    	go.name = request.asset.name

    	local canvas = GameObject.Find("Canvas")
        go.transform:SetParent(canvas.transform)

        local req2=Assets.LoadAssetAsync("Assets/Game/Common/Atlas/Image.spriteatlas", typeof(CS.UnityEngine.U2D.SpriteAtlas))
        req2.completed=function(req)
        	local img = GameObject.Find("Image");
            img:GetComponent(typeof(CS.UnityEngine.UI.Image)).sprite = req.asset:GetSprite("button-text-clear");
            --req:Release()
            --request:Release()
        end
    end
end

function TestProto()
	assert(protoc:load [[
		syntax = "proto2";
		package pb;

		message UserLoginREQ
		{
		    optional uint64 uid = 1;
		    optional string rdkey = 2;
		    optional string version = 3;
		}

		message UserLoginRSP
		{
		    optional int32  code = 1;   // -1 rdkey error | -2 version error | -3 server stop | -4 其他错误
		    optional uint64 uid = 2;
		}

	]])


local req={
	uid=10000028,
	rdkey="abb7dcc51e9d4eaa7b006e31eac08b60",
	version="1.4.0"
}
local bytes = assert(pb.encode("pb.UserLoginREQ", req))

GameEntry.network:SendConnect("47.115.171.11", 5000)
GameEntry.timer:Delay(3, function()
	CS.UnityEngine.Debug.LogWarning('proto')
	GameEntry.network:SendProto(bytes, "pb.UserLoginREQ")
end)

end

function TestSocket()
	local socket = require("socket")
	local host = "www.baidu.com"
	local file = "/"
	local sock = assert(socket.connect(host, 80))  -- 创建一个 TCP 连接，连接到 HTTP 连接的标准 80 端口上
	sock:send("GET " .. file .. " HTTP/1.0\r\n\r\n")
	repeat
	    local chunk, status, partial = sock:receive(1024) -- 以 1K 的字节块来接收数据，并把接收到字节块输出来
	    print(chunk or partial)
	until status ~= "closed"
	sock:close()  -- 关闭 TCP 连接
end