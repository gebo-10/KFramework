local File=CS.System.IO.File
local CJson = require('cjson')
PlayerPrefs = PlayerPrefs  or Singleton("PlayerPrefs")
function PlayerPrefs:__init()
	self.path=CS.LuaFramework.Util.DataPath.."playerprefs"
	if File.Exists(self.path) then
		local json=File.ReadAllBytes(self.path)
		self.data=CJson.decode(json)
	else
		self.data={}
	end
end

function PlayerPrefs:Save()
	File.WriteAllBytes(self.path, CJson.encode(self.data));
end

function PlayerPrefs:Get(key)
	return self.data[key]
end

function PlayerPrefs:Set(key,value)
	self.data[key]=value
	self:Save()
end