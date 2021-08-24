--保存类类型的虚表
--_class的结构如下：_class = { Class_A = vtbl_A, Class_B = vtbl_B}
local _class = {}
 
 --BaseClass(super)
 --函数功能：创建一个super类的子类类型
 --子类类型实现了New方法、设置了当前类的元表（包括__index函数和__newindex函数，用于索引操作，该操作实现了对虚表的索引）
 --如果是继承自父类，那么虚表的元表的索引方法将会在父类super对应的vtbl中去查找键值
function BaseClass(super)
	-- 生成一个类类型,	实际上存放类信息
	local class_type = {}
	
	-- 在创建对象的时候自动调用
	-- 默认的两个属性
	class_type.__init = false
	class_type.__delete = false
	class_type.super = super
	--!!!看此方法时，先跳过，看完BaseClass的其他定义后，再来看此"成员函数
	-- 创建接口
	-- 子类型class_type创建实例对象的方法
	class_type.New = function(...)
		-- 生成一个类对象
		local obj = {}
		obj._class_type = class_type
		-- 在初始化之前注册基类方法
		-- 即引入基类class_type的虚表vtbl
		setmetatable(obj, { __index = _class[class_type] })

		--初始化
		do
			local create
			create = function(c, ...)
				if c.super then
					create(c.super, ...)
				end
				if c.__init then
					c.__init(obj, ...)
				end
			end
			create(class_type, ...)
		end

		-- 注册一个delete方法
		obj.DeleteMe = function(self)
			local now_super = self._class_type 
			while now_super ~= nil do	
				if now_super.__delete then
					now_super.__delete(self)
				end
				now_super = now_super.super
			end
		end
		
		return obj
	end

	--class_type类型的虚表
	local vtbl = {}
	--在类类型的虚表中，为当前的类类型添加虚表，比如使用Layer = Layer or BaseClass() 创建了一个子类Layer，那么，此时类类型虚表中，会添加这么一项：_class = { Class_A = vtbl_A, Class_B = vtbl_B, Layer = {}}
	_class[class_type] = vtbl

	-- 设置class_type的元表，给出访问索引函数和赋值索引函数
	-- 从赋值索引函数__newindex上可以看出，如果要往class_type类型的中添加属性，会在其虚表vtbl中实现这种键值的添加
	-- 比如Layer = BaseClass()后，通过Layer.position = {x = 0.0, y = 0.0} 往Layer中添加一个键值，此时该键值会添加在Layer类型所对应的虚表中，此时虚表变为：_class = { Class_A = vtbl_A, Class_B = vtbl_B, Layer = { position = {x = 0.0, y = 0.0} }}
	-- 于是这么做的好处就现象出来了，类型Layer自身的键值不会发生改变，因此可以做为一个“稳定”的基类去派生子类，而所有的变化，都体现在Layer所对应的虚表vtbl_Layer中，这样的设计，封装了变化，也便于继承的实现
	setmetatable(class_type, {__newindex =
		function(t,k,v)
			vtbl[k] = v
		end
		, 
		__index = vtbl, --For call parent method
	})

	--如果是通过super派生子类，那么vtbl的访问索引将在super的vtbl中查找
	if super then
		setmetatable(vtbl, {__index =
			function(t,k)
				local ret = _class[super][k]
				--do not do accept, make hot update work right!
				vtbl[k] = ret
				return ret
			end
		})
	end
 
	return class_type
end

function StaticClass( )
	return {}
end