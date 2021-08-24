SoundManager = SoundManager  or Singleton("SoundManager")
SoundManager.SoundType={
	Background=0,
	Effect=1,
}

function SoundManager:__init()
	self.manager=LuaHelper.GetSoundManager()
end

function SoundManager:AddSound(clip)
	self.manager:AddAudioClip(clip)
end

function SoundManager:PlayBackground(name)
	if Util.HasKey("set_data") then 
     	if Util.Split(Util.GetString("set_data"),",")[1]=="0" then return end	
    end 

	if self.manager:HasSound(name) then 
        self.manager:Play(SoundManager.SoundType.Background,name,true)
        return
	end
	ResourceManager.Instance():LoadAudio(name,function(sound)
		self:AddSound(sound)
		self.manager:Play(SoundManager.SoundType.Background,sound.name,true)
	end)
 -- 	DG.Tweening.DOTween.To(DG.Tweening.Core.DOSetter_float(function(value)
	--    		self.manager:Volume(SoundManager.SoundType.Background,value)
	-- 	end),
 -- 		0,v,1):SetEase(DG.Tweening.Ease.InCubic)
end

function SoundManager:StopBackground()
 -- 	DG.Tweening.DOTween.To(DG.Tweening.Core.DOSetter_float(function(value)
	--    		self.manager:Volume(SoundManager.SoundType.Background,value)
	-- 	end),
 -- 		self:GetVolume(0),0,1):SetEase(DG.Tweening.Ease.OutCubic):OnComplete(function()
 		self.manager:Stop(SoundManager.SoundType.Background)
	-- end)
end

function SoundManager:PauseBackground()
	self.manager:Pause(SoundManager.SoundType.Background)
end

function SoundManager:SetBackgroundVolume(value)
	self.manager:Volume(SoundManager.SoundType.Background,value)
end

function SoundManager:PlayEffect(name)
	if Util.HasKey("set_data") then 
	   if Util.Split(Util.GetString("set_data"),",")[2]=="0" then return end 
	end    

	if self.manager:HasSound(name) then 
        self.manager:Play(SoundManager.SoundType.Effect,name,false)
        return
	end
	ResourceManager.Instance():LoadAudio(name,function(sound)
		self:AddSound(sound)
		self.manager:Play(SoundManager.SoundType.Effect,sound.name,false)
	end)
end
function SoundManager:StopEffect()
	self.manager:Stop(SoundManager.SoundType.Effect)
end

function SoundManager:PauseEffect()
	self.manager:Pause(SoundManager.SoundType.Effect)
end

function SoundManager:EffectOn()
	-- local v = SetCtrl.Instance():GetEffectVolume()
	-- self.manager:Volume(SoundManager.SoundType.Effect,v)
end

function SoundManager:EffectOff()
	self.manager:Volume(SoundManager.SoundType.Effect,0)
end

function SoundManager:SetEffectVolume(value)
	self.manager:Volume(SoundManager.SoundType.Effect,value)
end

function SoundManager:Silence()
	DG.Tweening.DOTween.To(DG.Tweening.Core.DOSetter_float(function(value)
	   		self.manager:Volume(SoundManager.SoundType.Background,value)
		end),
 		self:GetVolume(0),0,1):SetEase(DG.Tweening.Ease.OutCubic):OnComplete(function()
 		self.manager:Silence()
	end)
end

function SoundManager:Resume()
	-- local v = SetCtrl.Instance():GetBgVolume()
	-- self.manager:Resume()
	-- DG.Tweening.DOTween.To(DG.Tweening.Core.DOSetter_float(function(value)
	--    		self.manager:Volume(SoundManager.SoundType.Background,value)
	-- 	end),
 -- 		0,v,2.5):SetEase(DG.Tweening.Ease.InCubic)
end

function SoundManager:GetVolume(index)
	return self.manager:GetVolume(index)
end