TimerQuest = TimerQuest  or Singleton("TimerQuest")

function TimerQuest:__init()
	
end

function TimerQuest:ResetData()
	
end

--[[延时执行一个任务]]
function TimerQuest:AddDelayQuest(quest_func, delay_time)

	return quest.id
end

--[[周期性执行一个任务]]
function TimerQuest:AddPeriodQuest(quest_func, period, last_time)

	return quest.id
end

--[[周期性执行任务指定次数]]
function TimerQuest:AddRunTimesQuest( quest_func, period, run_count)

	return quest.id
end

--[[周期性任务 周期起点 周期增量 持续时间 ]]
-- !!! 周期为负数将导致任务一直执行
function TimerQuest:AddPeriodGrowthQuest( quest_func, period_start, period_growth, last_time )

end

function  TimerQuest:CancelQuest( quest_id )

end

