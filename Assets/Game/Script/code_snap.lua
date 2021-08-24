require "system.game_entry"
function Main()
	CS.UnityEngine.Debug.Log('hello world')
	-- local x=CS.GameFramework.Utility.Text.Format("xxx! This is an empty project based on Game Framework {0}.", CS.GameFramework.Version.GameFrameworkVersion)
	-- CS.UnityEngine.Debug.Log(x)
	--local foo_generic = xlua.get_generic_method(CS.GetGenericMethodTest, 'Foo')


	-- GameEntry.event:Subscribe(CS.UnityGameFramework.DownloadSuccessEventArgs.EventId, function (sender,e )
	-- 	print("yyyyyyyyyyyyyyyyyyyy")
	-- end,1)
	local path=CS.UnityEngine.Application.dataPath .."/../RuntimeData/chat.unity3d"
	local url= "http://192.168.0.192/window/20201010/chat.unity3d"
	--GameEntry.download:AddDownload(path, url)
	-- GameEntry.download_ext:AddDownload(path, url, function (info)
	-- 	print(info.id)
	-- 	print(info.state)
	-- 	print(info.url)
	-- 	print(info.path)
	-- 	print(info.current)
	-- 	print(info.delta)

	-- end)
	-- local txt=CS.System.IO.File.ReadAllText(CS.UnityEngine.Application.dataPath .."/../RuntimeData/test.txt")
	-- print(txt)
	Test()
end

function Update( )
	--print("Update")
end

GameObject=CS.UnityEngine.GameObject
Assets=CS.kassets.Assets
function Test()
	
	Assets.Initialize(function ()
		-- Assets.LoadModule("Common")
  --       Assets.LoadModule("Main")
  --       Assets.LoadModule("BlackJack")
        local req=Assets.LoadAssetAsync("Assets/Game/Main/Panel/PanelMain.prefab", typeof(GameObject))
        req.completed=function (request )
        	--print("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOk"..request.name)

        	local go = CS.UnityEngine.Object.Instantiate(request.asset  );
        	--go=cast(go, typeof(GameObject) )
        	go.name = request.asset.name

        	local canvas = GameObject.Find("Canvas")
            go.transform:SetParent(canvas.transform)


            local req2=Assets.LoadAssetAsync("Assets/Game/Common/Atlas/Image.spriteatlas", typeof(CS.UnityEngine.U2D.SpriteAtlas))
            req2.completed=function(req)
            	--print("OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOk"..req.asset.name)
            	local img = GameObject.Find("Image");
                img:GetComponent(typeof(CS.UnityEngine.UI.Image)).sprite = req.asset:GetSprite("button-text-clear");
                req:Release()
                request:Release()

                req=nil
                request=nil
                -- Assets.UnLoadModule("Common");
                -- Assets.UnLoadModule("Main");
                -- Assets.UnLoadModule("BlackJack");
            end
        end
	end)
end