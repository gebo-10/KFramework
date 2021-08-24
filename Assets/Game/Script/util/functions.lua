local function concat(t)
	local t1={}
	for i=1,#t do
		local str=tostring(t[i]).." "
		if t[i]==nil then
			str="nil "
		end
		table.insert(t1,str)
	end
	return table.concat(t1)
end
function loge(...)
	local strs={"<b><color=#7C45D6>LUA:</color></b> ",...}
	local str=concat(strs)
	str="<color=#FF0000>"..str.."</color>"
	LuaFramework.Util.LogError(str.."\n"..debug.traceback())
end

--输出日志--
function log(...)
    local strs={"<b><color=#7C45D6>LUA:</color></b> ",...}
	local str=concat(strs)
	LuaFramework.Util.Log(str)
end

function logw(...)
	local strs={"<b><color=#7C45D6>LUA:</color></b> ",...}
	local str=concat(strs)
	str="<color=#FF9200>"..str.."</color>"
	LuaFramework.Util.LogWarning(str)
end

function logw2(...)
	local strs={"<b><color=#7C45D6>LUA:</color></b> ",...}
	local str=concat(strs)
	LuaFramework.Util.LogWarning(str)
end

function logt(t,str)
	if not Config.DebugMode then return end
	local json = require "cjson"
	local json_str = json.encode(t)
	if str ~= nil then
		logw(str,json_str)
	else
		logw(json_str)
	end
end

_G.print=log
if not Config.DebugMode then
	_G.print=function ()end
	_G.log=function ()end
	_G.logw=function ()end
	_G.logt=function ()end
end