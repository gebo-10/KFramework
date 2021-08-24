--[[
Auth:Chiuan
like Unity Brocast Event System in lua.
]]
require "event.event_enum"
local EventLib = require "event/eventlib"

Event = {}
local events = {}

function Event.Bind(event,handler)
	--if not event or type(event) ~= "string" then
	if not event then
		error("event parameter in addlistener function has to be string, " .. type(event) .. " not right.")
	end
	if not handler or type(handler) ~= "function" then
		error("handler parameter in addlistener function has to be function, " .. type(handler) .. " not right")
	end

	if not events[event] then
		--create the Event with name
		events[event] = EventLib:new(event)
	end

	--conn this handler
	events[event]:connect(handler)
end

function Event.Fire(event,...)
	if events[event] then
		events[event]:fire(...)
	end
end

function Event.Unbind(event,handler)
	if not events[event] then
		error("remove " .. event .. " has no event.")
	else
		events[event]:disconnect(handler)
	end
end
