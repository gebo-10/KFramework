FrameImage = FrameImage or BaseClass(Control)

function FrameImage:__init(go)
	self.go=go
	self.type = "FrameImage"
	self.comp_Animator=go:GetComponent("Animator")
	self.speed=12
end

function FrameImage:SetAnimator(animator)
	self.comp_Animator.runtimeAnimatorController=animator
end

function FrameImage:Play()
	
end

function FrameImage:Pause()
	
end

function FrameImage:Stop()
	
end

function FrameImage:SetSpeed(s)
	self.speed=s
end

