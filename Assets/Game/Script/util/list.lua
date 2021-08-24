List = {}
function List.New()
	return {first = 0, last = -1}
end

function List.Size(list)
	if list.first > list.last then
		return 0
	else
		return list.last + 1 - list.first
	end
end

function List.Empty(list)
	return list.first > list.last
end

function List.Get(list, index)
	local index = list.first + index
	return list[index]	
end

function List.PushFront(list, value)
	local first = list.first - 1
	list.first = first
	list[first] = value
end

function List.PushBack(list, value)
	local last = list.last + 1
	list.last = last
	list[last] = value
end

function List.PopFront(list)
	local first = list.first
	if first > list.last then error("list is empty") end
	local value = list[first]
	list[first] = nil		-- to allow garbage collection
	list.first = first + 1
	return value
end

function List.PopBack(list)
	local last = list.last
	if list.first > last then error("list is empty") end
	local value = list[last]
	list[last] = nil		-- to allow garbage collection
	list.last = last - 1
	return value
end

function List.GetBack(list)
	local last = list.last
	return list[last]	
end

function List.GetFront(list)
	local first = list.first
	return list[first]
end

function List.Clear(list)
	for i=list.first, list.last + 1 do
		list[i] = nil
	end
	
	list.first = 0
	list.last = -1
end

function List.Map(list,fun)
	if List.Empty(list) then return end
	for i=list.first, list.last  do
		fun(list[i])
	end
end

function List.GetAllItem(list)
	local items={}
	if List.Empty(list) then return items end
	for i=list.first, list.last  do
		table.insert(items,list[i])
	end
	return items
end