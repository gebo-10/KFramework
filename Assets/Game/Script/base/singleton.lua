require("base.singletonclass")
require("base.singletonmanager")

-- Singleton
-- 使用Singleton方法创建出来的对象，即为一个单例，并且会在对应的时机调用Update、ResetData和Destroy
-- example:
-- SingletonExample = SingletonExample or Singleton("SingletonExample")
-- SingletonExample:Instance():FOO()

GlobalSingletonManager = GlobalSingletonManager or SingletonManager.New()

function Singleton(name)
    local class = BaseClass(SingletonClass)
    function class:Instance()
        if class.instance == nil then
            class.instance = class.New(name)
            GlobalSingletonManager:RegisterSingleton(name, class.instance)
        end
        return class.instance
    end

    function class:Destroy()
        if class.instance == nil then
            return
        end
        class.instance:DeleteMe()
        class.instance = nil
        GlobalSingletonManager:RemoveSingleton(name)
    end

    function class:HasCreate()
        return class.instance ~= nil
    end

    return class
end

