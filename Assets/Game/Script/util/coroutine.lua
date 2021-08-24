Coroutine={
	Start=function(fun, ... )
		return coroutine.start(fun, ...)
	end,
	Stop=function(co )
		coroutine.stop(co)
	end,
	Step=function()
		coroutine.step()
	end,
	WaitTime=function(t)
		coroutine.wait(t)
	end,

	WaitTween=function(tween)
		Yield(tween:WaitForCompletion())
	end,

	WaitSign=function(fun)
		while not fun() do
			coroutine.step()
		end
	end,

	WaitView=function (view)
		while not view:IsOpen() do
			coroutine.step()
		end
	end,
	WaitProto=function (name)
		return Network.WaitProto(name)
	end
}

--[[
Coroutine.Start(function ()
	Coroutine.WaitTime(3)
	log("xxxx")
	local  tween=self.btn_gold.transform:DOMove(Vector3.one, 3)
	Coroutine.WaitTween(tween)
	log("xxxx2")
end)
]]
