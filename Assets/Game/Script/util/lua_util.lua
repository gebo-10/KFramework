Util={}
local CJson = require('cjson')
function Util.FromJson(json)
	return CJson.decode(json)
end

function Util.ToJson(data) 
	return CJson.encode(data)
end

function Util.HttpGet(url,cb) --cb(error,data)
	Network.HttpGet(url,cb)
end
function Util.HttpPost(url,data,cb) --cb(error,data)
	Network.HttpPost(url,data,cb)
end

function Util.HttpGetImage(url,cb)
	Network.HttpGetImage(url,cb)
end


function Util.Debug(fn)
	if Config.DebugMode then
		fn()
	end
end

function Util.ServerTimestamp( )
	return os.time()-Game.Time.diff
end

function Util.ErrorCode(code)
	if code==0 then return end
	local e=ConfigError[code]
	if e==nil then
		Tip.Instance():PushTip("Error code "..tostring(code) )
		return
	end
	local content = e[Language.current_type]
	local t=e.type
	if t== ErrorType.Tip then
		Tip.Instance():PushTip(content)
	end
end

function Util.FormatGold(value)
	if tonumber(value) >= 10000000 then
		local result = (math.floor(value / 100000) / 100) .. "M"
		return result
	end
	return tostring(value)
end

function Util.Split(szFullString, szSeparator)  
	local nFindStartIndex = 1  
	local nSplitIndex = 1  
	local nSplitArray = {}  
	while true do  
	   local nFindLastIndex = string.find(szFullString, szSeparator, nFindStartIndex)  
	   if not nFindLastIndex then  
		nSplitArray[nSplitIndex] = string.sub(szFullString, nFindStartIndex, string.len(szFullString))  
		break  
	   end  
	   nSplitArray[nSplitIndex] = string.sub(szFullString, nFindStartIndex, nFindLastIndex - 1)  
	   nFindStartIndex = nFindLastIndex + string.len(szSeparator)  
	   nSplitIndex = nSplitIndex + 1  
	end  
	return nSplitArray  
end 

function Util.SetAvatar(image,url)
	local tag=image.tag
	ResourceManager.Instance():SetDefaultPhoto(image, 0)
	if url == nil or url=="" then return  end
	local r = Util.Split( url, ":" )
	if r[1]== "default"  then
		local index=tonumber(r[2])
		if index ==nil then
			loge("SetAvatar error",url)
			return
		end
		ResourceManager.Instance():SetDefaultPhoto(image, index)
	else--if r[1]== "http" or r[1]== "https" then
		if ResourceManager.Instance().photo_canche[url]~=nil then
			image:SetTexture(ResourceManager.Instance().photo_canche[url])
		else
			Util.HttpGetImage(url,function(error,texture)
				if error == 1 then
					loge("Download avatar error",url)
					return
				end
				if tag and tag ~= image.tag then
					return
				end
				image:SetTexture(texture)
				ResourceManager.Instance().photo_canche[url]=texture
			end)
		end
	end
end

function Util.SetAvatarFrame(image,head_frame)
	if head_frame<0 then return end
	if head_frame==1000 then head_frame=9 end
	--print("头像：________________________________ ",head_frame)
	ResourceManager.Instance():SetPhotoBox(image,head_frame)
end

function Util.Trim(str)
   return (string.gsub(str, "^%s*(.-)%s*$", "%1"))
end

function Util.Rate(num)
   return math.floor(num/100)
end

function Util.DeRate(num)
   return math.floor(num*100)
end

function Util:ShowTip(obj,time)
	time=time or 2
	if obj.go.activeSelf then return end
	obj:SetActive(true)
 	DG.Tweening.DOTween.To(DG.Tweening.Core.DOSetter_float(function(value)
	end),0,1,time):OnComplete(function()
		obj:SetActive(false)
	end)
end

function Util.Instantiate(go)
	return UnityEngine.GameObject.Instantiate(go)
end

--将数字转为k(千),m(百万),b(十亿)格式
function Util.FormatNum(num)
	num=tonumber(num)
	if num>=1000000000 then
		num=num/1000000000
		num=math.floor(num*100)
		return string.format("%.2f",num/100).."b"
	elseif num>=1000000 then
		num=num/1000000
		num=math.floor(num*100)
		return string.format("%.2f",num/100).."m"
	elseif num>=1000 then
		num=num/1000
		num=math.floor(num*100)
		return string.format("%.2f",num/100).."k"
	elseif num<1000 then
		return num
	end
end

--返回未来时间-当前时间的秒数
function Util.TimeSpan(time)
	return time-os.time()
end

function Util.Digit(num)
	if num <1 then
		return 0
	end
	return math.floor(math.log10(num))+1
end

--截取名字长度
function Util.NameSub(name,len)
	return string.len(name)>=len and string.sub(name, 1, len).."..." or name 
end

--截取数字(超过3位逗号隔开如:12,456,123)
function Util.NumSub(num)
	if num<1000 then return num end
	local new_num=""
	local str_num=tostring(num)
	local t1,t2=math.modf(#str_num/3)
	if t2==0 then t1=t1-1 end
	local c=string.sub(str_num,1,#str_num-t1*3)
    for i=1,t1 do
    	local b=string.sub(str_num,-3*i)
    	b=string.sub(b,1,3)
    	new_num=","..b..new_num
    end
    return c..new_num
end

function Util.DeepCopy(ori_tab) --不处理死循环
    if (type(ori_tab) ~= "table") then
       return nil;
    end
    local new_tab = {};
    for i,v in pairs(ori_tab) do
       local vtyp = type(v);
       if (vtyp == "table") then
           new_tab[i] = Util.DeepCopy(v);
       else
           new_tab[i] = v;
       end
    end
    return new_tab;
end

local bit = require("bit")
function Util.IsBitSet(num, position) --position 序号是1-32 低位开始算
	log(bit.tohex(num))
	local test=bit.lshift(1, position-1)
	log(bit.tohex(test))
	local res=bit.band(num,test)
	log(bit.tohex(res))
	return res >0
end

--校验邮箱格式
function Util.IsRightEmail(str)
  if string.len(str or "") < 6 then return false end
  local b,e = string.find(str or "", '@')
  local bstr = ""
  local estr = ""
  if b then
   bstr = string.sub(str, 1, b-1)
   estr = string.sub(str, e+1, -1)
  else
   return false
  end

  --检查@前字符串
  local p1,p2 = string.find(bstr, "[%w_.]+")--任意字符、数字或者.都可以匹配
  if (p1 ~= 1) or (p2 ~= string.len(bstr)) then return false end

  --检查@后字符串
  if string.find(estr, "^[%.]+") then return false end
  if string.find(estr, "%.[%.]+") then return false end
  if string.find(estr, "@") then return false end
  if string.find(estr, "[%.]+$") then return false end

  _,count = string.gsub(estr, "%.", "")
  if (count < 1 ) or (count > 3) then
   return false
  end

  return true
end

--验证密码是否包含数字加字母
function Util.MatchPwd(pwd)
	local num=string.match(pwd,"%d+")
	local letter=string.match(pwd,"%a+")
	if num==nil or letter==nil then
		return false
	else
		return true
	end
end

function Util.SubstringNickName(name)
	if string.len(name)<8 then return name end
	f1=string.find(name,"Guest")
	f2=string.find(name,"Mata")
	if f1==1 then
		return "Guest"..string.sub(name,-5)
	elseif f2==1 then
		return "Mata"..string.sub(name,-5)
	else
		return name
	end
end

--字符串转成时间戳
function Util.String2time(timestr)
    local Y=string.sub(timestr,1,4)
    local M=string.sub(timestr,6,7)
    local D=string.sub(timestr,9,10)
    return os.time({year=Y, month=M,day=D, hour=0,min=0,sec=0})
end

--local t={name="stu",name2="haha"}
--string.gsub([[hello {name},i am {name2}]], [[{(%w+)}]], t)
function Util.ParamString(pattern,json_str)
	if json_str==nil or json_str=="" then
		return pattern
	end
	local json=require "cjson"
	local t=json.decode(json_str)
	return string.gsub(pattern,[[{([%w_]+)}]],t)
end
--"unknown" "Mexico"
function Util.GetLocation()
	return Config.Region
end