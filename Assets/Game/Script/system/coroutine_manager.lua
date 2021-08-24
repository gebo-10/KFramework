local create = coroutine.create
local running = coroutine.running
local resume = coroutine.resume
local yield = coroutine.yield
local error = error
local unpack = unpack
local debug = debug

CoroutineManager = CoroutineManager  or Singleton("CoroutineManager")
function CoroutineManager:__init()
	self.co_list={}
end

function CoroutineManager:Update()
	for co,state in pairs(self.co_list) do
		if state~= nil then
			local flag, msg =resume(co)
			if not flag then						
					error(debug.traceback(co, msg))			
				return
			end
		end
	end
end

function CoroutineManager:Start(f, ...)
	local co = create(f)
	local flag, msg = resume(co, ...)
	
	if not flag then					
		error(debug.traceback(co, msg))
	end
	self.co_list[co]=true
	return co
end

function CoroutineManager:Stop(co )
	self.co_list[co]=nil
end

function CoroutineManager:Step()
	yield()
end

function CoroutineManager:Wait(t)
	local flag=false
	GameEntry.timer:Delay(t, function()
		flag=true
	end)
	while not flag do
		yield()
	end
end