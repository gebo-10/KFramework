ResourceManager = ResourceManager  or Singleton("ResourceManager")

function ResourceManager:__init()
	self.manager=LuaHelper.GetResManager()
	self.photo_canche={}--头像缓存
end

function ResourceManager:PreLoad()
	self.manager:LoadImage("headimages.unity3d","Head",{"0","1","2","3","4","5","6","7","8","9","10","11","12","13"},function(images)
		--log("resMgr:LoadImage",images[1])
		self.photos=images
	end)
	self.manager:LoadImage("vipimages.unity3d","Vip",{"b0","b1","b2","b3","b4","b5","b6","b7","b8","b9"},function(images)
		self.iconbox=images
	end)

	self.manager:LoadImage("signinimages.unity3d","SignIn",{"shop1","shop2","shop3","shop4","shop5","shop6","shop7","shop8"},function(images)
		self.golds=images
	end)

	self.manager:LoadImage("blackjackchipimages.unity3d","BlackJackChip",{"chip1","chip2","chip3","chip4","chip5"},function(images)
		self.bet_img=images
	end)

	self.manager:LoadImage("vipimages.unity3d","Vip",{"v0","v1","v2","v3","v4","v5","v6"},function(images)
		self.vip_img=images
	end)

	self.manager:LoadImage("itemimages.unity3d","Item",{"r1","r2","r3"},function(images)
		self.rank_img=images
	end)

	self.manager:LoadImage("pokerimages.unity3d","Poker",{"bg-spade","bg-heart","bg-club","bg-diamond"},function(images)
		self.suit_big=images
	end)

	self.manager:LoadImage("blackjackpokerimages.unity3d","BlackJackPoker",{"poker-spades","poker-hearts","poker-clubs","poker-diamonds"},function(images)
		self.suit_smart=images
	end)

	self.manager:LoadImage("blackjackpokerimages.unity3d","BlackJackPoker",{"red2","red3","red4","red5","red6","red7","red8","red9","red10","redJ","redQ","redK","redA"},function(images)
		self.rank_smart_red=images
	end)

	self.manager:LoadImage("blackjackpokerimages.unity3d","BlackJackPoker",{"black2","black3","black4","black5","black6","black7","black8","black9","black10","blackJ","blackQ","blackK","blackA"},function(images)
		self.rank_smart_black=images
	end)
    
    self.manager:LoadImage("pokerimages.unity3d","Poker",{"black2","black3","black4","black5","black6","black7","black8","black9","black10","blackJ","blackQ","blackK","blackA"},function(images)
		self.rank_big_black=images
	end)

    self.manager:LoadImage("pokerimages.unity3d","Poker",{"red2","red3","red4","red5","red6","red7","red8","red9","red10","redJ","redQ","redK","redA"},function(images)
		self.rank_big_red=images
	end)

    self.manager:LoadImage("pokerimages.unity3d","Poker",{"blue2","blue3","blue4","blue5","blue6","blue7","blue8","blue9","blue10","blueJ","blueQ","blueK","blueA"},function(images)
		self.rank_big_blue=images
	end)

	self.manager:LoadImage("pokerimages.unity3d","Poker",{"green2","green3","green4","green5","green6","green7","green8","green9","green10","greenJ","greenQ","greenK","greenA"},function(images)
		self.rank_big_green=images
	end)

	self.manager:LoadPrefab("emoji.unity3d",{"1","2","3","4","5","6","7","8","9","10","11","12","13","14","15","16","17","18","19","20"},function(prefabs)
		self.emojis=prefabs
	end)

	--加载mata筹码
	self:LoadAllImageAsync("chipimages.unity3d","Chip",function(images)
		self.mata_chip=images
	end)

	self:LoadAllImageAsync("backagegroundimages.unity3d","Backageground",function(images)
		self.background=images
	end)

	self:LoadAllImageAsync("goodsimages.unity3d","Goods",function(images)
		self.goods=images
	end)

	self.manager:LoadTextAsset("config.unity3d",{"LocalizationCSVForm"},function(texts)
		self.configs=texts
	end)

	self:LoadAllImageAsync("buttoncommonimages.unity3d","ButtonCommon",function(images)
		self.buttoncommon=images
	end)
	
	-- resMgr:LoadImage("cardimages.unity3d",{"joker","1","2","3","4","5","6","7","8","9","10","11","12","13","beimian"},function(images)
	-- 	self.cards=images
	-- end)

	-- resMgr:LoadAudio("sounds.unity3d",{"card","hallBg"},function(sounds)
	-- 	self.sounds=sounds
	-- 	for i=1,sounds.Length do
	-- 		SoundManager.Instance():AddSound(sounds[i-1])
	-- 	end
	-- end)
end

function ResourceManager:PreLoadFinish()
	if self.photos == nil then return false end
	if self.iconbox == nil then return false end
	if self.golds == nil then return false end
	if self.bet_img == nil then return false end
	if self.vip_img == nil then return false end
	if self.rank_img == nil then return false end
	if self.suit_big == nil then return false end
	if self.suit_smart == nil then return false end
	if self.rank_smart_red == nil then return false end
	if self.rank_smart_black == nil then return false end
	if self.rank_big_black == nil then return false end
	if self.rank_big_red == nil then return false end
	if self.rank_big_blue == nil then return false end
	if self.rank_big_green == nil then return false end
	if self.emojis == nil then return false end
	if self.goods == nil then return false end
	if self.configs==nil then return false end
	if self.buttoncommon==nil then return false end
	-- if self.cards == nil then return false end
	if self.background == nil then return false end
	return true
end

function ResourceManager:LoadImageAsync(abname,altas_name,res_names,cb)
	self.manager:LoadImage(abname,altas_name,res_names,function(images)
		cb(images)
	end)
end

function ResourceManager:LoadAllImageAsync(abname,altas_name,cb)
	self.manager:LoadAllImage(abname,altas_name,function (images )
		local image_map={}
		for i = 0, images.Length - 1 do
			local image=images[i]
			image.name=string.sub(image.name,1,-8)--去掉(Clone)
			image_map[image.name]=image
		end
		cb(image_map)
	end)
end

function ResourceManager:SetCardSprite(img,index)
	img:SetTexture(self.cards[index])
end

function ResourceManager:SetEmojiSprite(img,index)
	img:SetTexture(self.emoji[index])
end

function ResourceManager:SetGoodsSprite(img,name)
	img:SetTexture(self.goods[name])
end

function ResourceManager:SetEmojiAnimator(animator,index)
	animator:SetAnimator(self.emojiani[index])
end

function ResourceManager:SetDefaultPhoto(img, index)
	img:SetTexture(self.photos[index])
end

function ResourceManager:SetPhotoBox(img, index)
	img:SetTexture(self.iconbox[index])
end

function ResourceManager:SetGold(img, index)
	img:SetTexture(self.golds[index])
end

function ResourceManager:SetBetChip(img, index)
	img:SetTexture(self.bet_img[index])
end

function ResourceManager:SetVip(img, index)
	img:SetTexture(self.vip_img[index])
end

function ResourceManager:SetRank(img, index)
	img:SetTexture(self.rank_img[index])
end

function ResourceManager:GetEmoji(index)
	return self.emojis[index]
end

function ResourceManager:GetConfig(index)
	return self.configs[index]
end

--card_type,0小牌花色,1大牌花色
function ResourceManager:SetSuitCard(img,index,card_type)
	if card_type==0 then
       img:SetTexture(self.suit_smart[index-1])
	else
       img:SetTexture(self.suit_big[index-1])
	end 
end

--card_type,1,4小牌黑色数字,2,3小牌红色数字
function ResourceManager:SetRankSmartCard(img,index,card_type)
	if card_type==1 or card_type==3 then 
       img:SetTexture(self.rank_smart_black[index])
	else
	   img:SetTexture(self.rank_smart_red[index])
	end
end

--card_type,1大牌黑桃数字,2大牌红桃数字,3大牌梅花数字,4大牌方块数字
function ResourceManager:SetRankBigCard(img,index,card_type)
	if card_type==1 then 
		img:SetTexture(self.rank_big_black[index])		
	elseif card_type==2 then
		img:SetTexture(self.rank_big_red[index]) 	    
	elseif card_type==3 then
		img:SetTexture(self.rank_big_green[index])		
	elseif card_type==4 then
	    img:SetTexture(self.rank_big_blue[index])
	end
end

function ResourceManager:LoadAudio(name,fun)
	self.manager:LoadAudio("sounds.unity3d",{name},function(sounds)
		log("resMgr:LoadAudio",sounds[0])
	 	fun(sounds[0])
	end)
end

--mata筹码
function ResourceManager:SetMataChip(img,name)
	img:SetTexture(self.mata_chip[tostring(name)])
end