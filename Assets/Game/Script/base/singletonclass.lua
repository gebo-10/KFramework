require("base.baseclass")

-- SingletonClass
-- 所有单例的基类

SingletonClass = SingletonClass or BaseClass()

function SingletonClass:__init(class_name)
    if class_name == nil then
        error("singleton.New() need class_name parameter.")
    end
    self.class_name = class_name

    if self:HasCreate() then
        error("attempt to create singleton twice! " .. self.class_name)
    end
end

function SingletonClass:__delete()
end

function SingletonClass:Update(delta_time)
    --error("should implement Update method")
end

function SingletonClass:ResetData()
    --error("should implement ResetData method")
end

function SingletonClass:Cleanup()
   
end
