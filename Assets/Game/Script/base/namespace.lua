--_namespace={}
function namespace(namespace)
	if namespace == nil or namespace == _G then
		setmetatable(_G,nil)
		return
	end
	setmetatable(_G, {__newindex =
		function(t,k,v)
			namespace[k] = v
		end
		, 
		__index = namespace, --For call parent method
	})
end

function local_require(namespace,file)
	namespace(namespace)
	require(file)
	namespace()
end

function LocalClass(namespace, name,father)
	local class = BaseClass(father)
	namespace[name]=class
	return class
end
